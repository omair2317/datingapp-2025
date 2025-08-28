using System;
using System.Collections.Concurrent;

namespace API.SignalR;

public class PresenceTracker
{
    private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, byte>> OnlineUSers = new();

    public Task UserConnected(string userId, string connectionId)
    {
        var connections = OnlineUSers.GetOrAdd(userId, _ =>
            new ConcurrentDictionary<string, byte>());
        connections.TryAdd(connectionId, 0);
        return Task.CompletedTask;
    }

    public Task UserDisconnected(string userId, string connectionId)
    {
        if (OnlineUSers.TryGetValue(userId, out var connections))
        {
            connections.TryRemove(connectionId, out _);
            if (connections.IsEmpty)
            {
                OnlineUSers.TryRemove(userId, out _);
            }
        }

        return Task.CompletedTask;
    }

    public Task<string[]> GetOnlineUsers()
    {
        return Task.FromResult(OnlineUSers.Keys.OrderBy(k => k).ToArray());
    }

    public static Task<List<string>> GetConnectionsForUser(string userId)
    {
        if (OnlineUSers.TryGetValue(userId, out var connections))
        {
            return Task.FromResult(connections.Keys.ToList());
        }

        return Task.FromResult(new List<string>());
    }
}
