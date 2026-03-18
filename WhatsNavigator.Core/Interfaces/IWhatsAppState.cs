namespace WhatsNavigator.Core.Interfaces;

public interface IWhatsAppState
{
    Task SetAsync<T>(string key, T value);
    Task<T?> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}
