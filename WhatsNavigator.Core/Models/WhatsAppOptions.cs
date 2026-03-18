namespace WhatsNavigator.Core.Models;

public class WhatsAppOptions
{
    public const string SectionName = "WhatsNavigator";
    public string ApiToken { get; set; } = string.Empty;
    public string PhoneNumberId { get; set; } = string.Empty;
    public string VerifyToken { get; set; } = string.Empty;
    public string ApiVersion { get; set; } = "v21.0";
}
