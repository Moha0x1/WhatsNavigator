using WhatsNavigator.Core.Models;

namespace WhatsNavigator.Core.Interfaces;

public delegate Task WhatsAppHandlerDelegate(WhatsAppContext context);

public interface IWhatsAppMiddleware
{
    Task InvokeAsync(WhatsAppContext context, WhatsAppHandlerDelegate next);
}
