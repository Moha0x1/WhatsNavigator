using System.Collections.Concurrent;
using System.Text.Json;
using WhatsNavigator.Core.Models;
using WhatsNavigator.Core.Interfaces;

namespace WhatsNavigator.Infrastructure.State;

public class InMemoryWhatsAppState : IWhatsAppState
{
    private static readonly ConcurrentDictionary<string, string> _state = new();

    public Task SetAsync<T>(string key, T value)
    {
        _state[key] = JsonSerializer.Serialize(value);
        return Task.CompletedTask;
    }

    public Task<T?> GetAsync<T>(string key)
    {
        if (_state.TryGetValue(key, out var value))
        {
            return Task.FromResult(JsonSerializer.Deserialize<T>(value));
        }
        return Task.FromResult(default(T));
    }

    public Task RemoveAsync(string key)
    {
        _state.TryRemove(key, out _);
        return Task.CompletedTask;
    }
}
