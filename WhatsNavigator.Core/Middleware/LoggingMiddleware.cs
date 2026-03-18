using Microsoft.Extensions.Logging;
using WhatsNavigator.Core.Interfaces;
using WhatsNavigator.Core.Models;

namespace WhatsNavigator.Core.Middleware;

public class LoggingMiddleware : IWhatsAppMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(WhatsAppContext context, WhatsAppHandlerDelegate next)
    {
        _logger.LogInformation("Received message from {Sender}: {Text}", 
            context.Message.SenderNumber, 
            context.Message.Text ?? "[Non-text message]");
            
        await next(context);
        
        _logger.LogInformation("Finished processing message from {Sender}", context.Message.SenderNumber);
    }
}
