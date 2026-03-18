using System.Text.Json.Serialization;

namespace WhatsNavigator.Infrastructure.Models;

public record WhatsAppWebhookPayload(
    [property: JsonPropertyName("object")] string Object,
    [property: JsonPropertyName("entry")] List<WhatsAppEntry> Entry
);

public record WhatsAppEntry(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("changes")] List<WhatsAppChange> Changes
);

public record WhatsAppChange(
    [property: JsonPropertyName("value")] WhatsAppValue Value,
    [property: JsonPropertyName("field")] string Field
);

public record WhatsAppValue(
    [property: JsonPropertyName("messaging_product")] string MessagingProduct,
    [property: JsonPropertyName("metadata")] WhatsAppMetadata Metadata,
    [property: JsonPropertyName("contacts")] List<WhatsAppContact>? Contacts,
    [property: JsonPropertyName("messages")] List<WhatsAppMessagePayload>? Messages
);

public record WhatsAppMetadata(
    [property: JsonPropertyName("display_phone_number")] string DisplayPhoneNumber,
    [property: JsonPropertyName("phone_number_id")] string PhoneNumberId
);

public record WhatsAppContact(
    [property: JsonPropertyName("profile")] WhatsAppProfile Profile,
    [property: JsonPropertyName("wa_id")] string WaId
);

public record WhatsAppProfile(
    [property: JsonPropertyName("name")] string Name
);

public record WhatsAppMessagePayload(
    [property: JsonPropertyName("from")] string From,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("timestamp")] string Timestamp,
    [property: JsonPropertyName("text")] WhatsAppText? Text,
    [property: JsonPropertyName("type")] string Type
);

public record WhatsAppText(
    [property: JsonPropertyName("body")] string Body
);
