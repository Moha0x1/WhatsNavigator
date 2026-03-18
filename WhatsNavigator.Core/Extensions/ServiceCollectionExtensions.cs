using Microsoft.Extensions.DependencyInjection;
using WhatsNavigator.Core.Interfaces;
using WhatsNavigator.Core.Models;
using WhatsNavigator.Core.Routing;

namespace WhatsNavigator.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static WhatsAppBotBuilder AddWhatsNavigator(this IServiceCollection services, Action<WhatsAppOptions> configureOptions)
    {
        services.Configure(configureOptions);
        
        // The client implementation is in Infrastructure
        // We'll register it here. If the user wants a different client, they can override it.
        
        var builder = new WhatsAppBotBuilder(services);
        services.AddSingleton(sp => builder.Build());
        
        return builder;
    }
}
