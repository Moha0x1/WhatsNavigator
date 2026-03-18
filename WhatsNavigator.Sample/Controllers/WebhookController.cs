using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WhatsNavigator.Core.Models;
using WhatsNavigator.Infrastructure.Models;
using WhatsNavigator.Infrastructure.Services;

namespace WhatsNavigator.Sample.Controllers;

[ApiController]
[Route("webhook")]
public class WebhookController : ControllerBase
{
    private readonly WhatsAppWebhookProcessor _processor;
    private readonly WhatsAppOptions _options;
    private readonly ILogger<WebhookController> _logger;

    public WebhookController(
        WhatsAppWebhookProcessor processor,
        IOptions<WhatsAppOptions> options,
        ILogger<WebhookController> logger)
    {
        _processor = processor;
        _options = options.Value;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Verify([FromQuery(Name = "hub.mode")] string mode,
                                [FromQuery(Name = "hub.verify_token")] string token,
                                [FromQuery(Name = "hub.challenge")] string challenge)
    {
        if (mode == "subscribe" && token == _options.VerifyToken)
        {
            _logger.LogInformation("Webhook verified successfully.");
            return Ok(challenge);
        }

        _logger.LogWarning("Webhook verification failed. Mode: {Mode}, Token: {Token}", mode, token);
        return Forbid();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] WhatsAppWebhookPayload payload)
    {
        _logger.LogInformation("Received webhook payload.");
        try
        {
            await _processor.ProcessAsync(payload);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing webhook payload.");
            return Ok(); // Meta expects 200/Ok to stop retrying
        }
    }
}
