using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using WhatsNavigator.Core.Interfaces;
using WhatsNavigator.Core.Models;

namespace WhatsNavigator.Core.Routing;

public class WhatsAppBot
{
    private readonly List<HandlerRegistration> _handlers = new();
    private readonly List<Type> _middlewareTypes = new();

    public WhatsAppBot OnMessage(Func<WhatsAppMessage, bool> predicate, WhatsAppHandlerDelegate handler)
    {
        _handlers.Add(new HandlerRegistration(predicate, handler));
        return this;
    }

    public WhatsAppBot OnCommand(string command, WhatsAppHandlerDelegate handler)
    {
        return OnMessage(m => m.Text is not null && m.Text.Trim().StartsWith($"/{command}", StringComparison.OrdinalIgnoreCase), handler);
    }

    public WhatsAppBot Use<TMiddleware>() where TMiddleware : IWhatsAppMiddleware
    {
        _middlewareTypes.Add(typeof(TMiddleware));
        return this;
    }

    public async Task HandleAsync(WhatsAppContext context)
    {
        var middlewarePipeline = BuildPipeline(context.Services);
        await middlewarePipeline(context);
    }

    private WhatsAppHandlerDelegate BuildPipeline(IServiceProvider sp)
    {
        WhatsAppHandlerDelegate next = async ctx =>
        {
            var handler = _handlers.FirstOrDefault(h => h.Predicate(ctx.Message));
            if (handler != null)
            {
                await handler.Handler(ctx);
            }
        };

        for (int i = _middlewareTypes.Count - 1; i >= 0; i--)
        {
            var middlewareType = _middlewareTypes[i];
            var middleware = (IWhatsAppMiddleware)sp.GetRequiredService(middlewareType);
            var currentNext = next;
            next = async ctx => await middleware.InvokeAsync(ctx, currentNext);
        }

        return next;
    }

    private record HandlerRegistration(Func<WhatsAppMessage, bool> Predicate, WhatsAppHandlerDelegate Handler);
}
