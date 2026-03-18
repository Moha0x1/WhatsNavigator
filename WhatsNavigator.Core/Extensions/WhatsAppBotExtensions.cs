using WhatsNavigator.Core.Interfaces;
using WhatsNavigator.Core.Models;
using WhatsNavigator.Core.Routing;

namespace WhatsNavigator.Core.Extensions;

public static class WhatsAppBotExtensions
{
    public static WhatsAppBot OnMessage(this WhatsAppBot bot, Func<WhatsAppMessage, bool> predicate, WhatsAppHandlerDelegate handler)
    {
        return bot.OnMessage(predicate, handler);
    }

    public static WhatsAppBot OnCommand(this WhatsAppBot bot, string command, WhatsAppHandlerDelegate handler)
    {
        return bot.OnCommand(command, handler);
    }

    public static WhatsAppBot OnMessage(this WhatsAppBot bot, string text, WhatsAppHandlerDelegate handler)
    {
        return bot.OnMessage(m => m.Text is not null && m.Text.Equals(text, StringComparison.OrdinalIgnoreCase), handler);
    }
}
