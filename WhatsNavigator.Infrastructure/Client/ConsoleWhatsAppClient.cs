using Microsoft.Extensions.Logging;
using WhatsNavigator.Core.Interfaces;

namespace WhatsNavigator.Infrastructure.Client;

public class ConsoleWhatsAppClient : IWhatsAppClient
{
    private readonly ILogger<ConsoleWhatsAppClient> _logger;

    public ConsoleWhatsAppClient(ILogger<ConsoleWhatsAppClient> logger)
    {
        _logger = logger;
    }

    public Task SendTextAsync(string to, string text, CancellationToken ct = default)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n[WHATSAPP-CONSOLE] Enviando a {to}:");
        Console.ResetColor();
        Console.WriteLine($"{text}\n");
        return Task.CompletedTask;
    }

    public Task SendTemplateAsync(string to, string templateName, string languageCode, object[] components = null, CancellationToken ct = default)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n[WHATSAPP-CONSOLE] Enviando plantilla '{templateName}' a {to} ({languageCode})");
        Console.ResetColor();
        return Task.CompletedTask;
    }
}
