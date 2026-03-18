using Microsoft.Extensions.DependencyInjection;
using WhatsNavigator.Core.Interfaces;
using WhatsNavigator.Core.Models;
using WhatsNavigator.Core.Routing;

namespace WhatsNavigator.Core.Extensions;

public class WhatsAppBotBuilder
{
    private readonly WhatsAppBot _bot = new();
    public IServiceCollection Services { get; }

    public WhatsAppBotBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public WhatsAppBotBuilder OnMessage(Func<WhatsAppMessage, bool> predicate, WhatsAppHandlerDelegate handler)
    {
        _bot.OnMessage(predicate, handler);
        return this;
    }

    public WhatsAppBotBuilder OnCommand(string command, WhatsAppHandlerDelegate handler)
    {
        _bot.OnCommand(command, handler);
        return this;
    }

    public WhatsAppBotBuilder Use<TMiddleware>() where TMiddleware : class, IWhatsAppMiddleware
    {
        Services.AddTransient<TMiddleware>();
        _bot.Use<TMiddleware>();
        return this;
    }

    public WhatsAppBot Build()
    {
        return _bot;
    }
}
