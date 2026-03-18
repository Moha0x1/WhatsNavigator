using Microsoft.Extensions.DependencyInjection;
using WhatsNavigator.Core.Interfaces;
using WhatsNavigator.Core.Models;
using WhatsNavigator.Core.Routing;
using WhatsNavigator.Infrastructure.Models;

namespace WhatsNavigator.Infrastructure.Services;

public class WhatsAppWebhookProcessor
{
    private readonly WhatsAppBot _bot;
    private readonly IWhatsAppClient _client;
    private readonly IWhatsAppState _state;
    private readonly IServiceProvider _serviceProvider;

    public WhatsAppWebhookProcessor(
        WhatsAppBot bot,
        IWhatsAppClient client,
        IWhatsAppState state,
        IServiceProvider serviceProvider)
    {
        _bot = bot;
        _client = client;
        _state = state;
        _serviceProvider = serviceProvider;
    }

    public async Task ProcessAsync(WhatsAppWebhookPayload payload)
    {
        foreach (var entry in payload.Entry)
        {
            foreach (var change in entry.Changes)
            {
                if (change.Value.Messages == null) continue;

                foreach (var message in change.Value.Messages)
                {
                    var waMessage = new WhatsAppMessage(
                        SenderNumber: message.From,
                        Text: message.Text?.Body ?? string.Empty,
                        MessageId: message.Id,
                        RawPayload: System.Text.Json.JsonSerializer.Serialize(message)
                    );

                    var context = new WhatsAppContext(
                        Message: waMessage,
                        Client: _client,
                        Services: _serviceProvider,
                        State: _state
                    );

                    await _bot.HandleAsync(context);
                }
            }
        }
    }
}
