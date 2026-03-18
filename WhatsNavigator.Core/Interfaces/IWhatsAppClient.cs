namespace WhatsNavigator.Core.Interfaces;

public interface IWhatsAppClient
{
    Task SendTextAsync(string to, string text, CancellationToken ct = default);
    Task SendTemplateAsync(string to, string templateName, string languageCode, object[] components = null, CancellationToken ct = default);
}
