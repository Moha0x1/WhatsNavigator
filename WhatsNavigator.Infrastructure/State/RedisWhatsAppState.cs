using System.Text.Json;
using StackExchange.Redis;
using WhatsNavigator.Core.Models;
using WhatsNavigator.Core.Interfaces;

namespace WhatsNavigator.Infrastructure.State;

public class RedisWhatsAppState : IWhatsAppState
{
    private readonly IDatabase _db;

    public RedisWhatsAppState(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);
        if (value.IsNullOrEmpty) return default;
        
        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }
}
