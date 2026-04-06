using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class ChatHistoryService(IDistributedCache cache)
{
    public async Task SaveHistoryAsync(string sessionId, List<ChatMessage> history)
    {
        var json = JsonSerializer.Serialize(history);
        await cache.SetStringAsync(sessionId, json);
    }

    public async Task<List<ChatMessage>> GetHistoryAsync(string sessionId)
    {
        var json = await cache.GetStringAsync(sessionId);
        if (string.IsNullOrEmpty(json)) return new List<ChatMessage>();
        return JsonSerializer.Deserialize<List<ChatMessage>>(json) ?? new List<ChatMessage>();
    }
}