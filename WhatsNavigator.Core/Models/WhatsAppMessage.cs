namespace WhatsNavigator.Core.Models;

public record WhatsAppMessage(
    string SenderNumber,
    string Text,
    string MessageId,
    string RawPayload,
    IDictionary<string, object>? Metadata = null
);
