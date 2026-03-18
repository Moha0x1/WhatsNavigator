using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using WhatsNavigator.Core.Interfaces;
using WhatsNavigator.Core.Models;

namespace WhatsNavigator.Infrastructure.Client;

public class WhatsAppClient : IWhatsAppClient
{
    private readonly HttpClient _httpClient;
    private readonly WhatsAppOptions _options;

    public WhatsAppClient(HttpClient httpClient, IOptions<WhatsAppOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
        
        _httpClient.BaseAddress = new Uri($"https://graph.facebook.com/{_options.ApiVersion}/{_options.PhoneNumberId}/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.ApiToken);
    }

    public async Task SendTextAsync(string to, string text, CancellationToken ct = default)
    {
        var payload = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = to,
            type = "text",
            text = new { body = text }
        };

        var response = await _httpClient.PostAsJsonAsync("messages", payload, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task SendTemplateAsync(string to, string templateName, string languageCode, object[] components = null, CancellationToken ct = default)
    {
        var payload = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = to,
            type = "template",
            template = new
            {
                name = templateName,
                language = new { code = languageCode },
                components = components
            }
        };

        var response = await _httpClient.PostAsJsonAsync("messages", payload, ct);
        response.EnsureSuccessStatusCode();
    }
}
