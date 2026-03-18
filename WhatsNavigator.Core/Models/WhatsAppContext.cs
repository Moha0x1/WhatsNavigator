using WhatsNavigator.Core.Interfaces;

namespace WhatsNavigator.Core.Models;

public record WhatsAppContext(
    WhatsAppMessage Message,
    IWhatsAppClient Client,
    IServiceProvider Services,
    IWhatsAppState State
);
