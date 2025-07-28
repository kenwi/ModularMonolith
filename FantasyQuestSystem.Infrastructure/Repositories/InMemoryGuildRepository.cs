using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;
using FantasyQuestSystem.Domain.Repositories;

namespace FantasyQuestSystem.Infrastructure.Repositories;

public class InMemoryGuildRepository : IGuildRepository
{
    private readonly Dictionary<Guid, Guild> _guilds = new();
    private readonly HashSet<string> _guildNames = new();

    public Task<Guild?> GetByIdAsync(Guid id)
    {
        _guilds.TryGetValue(id, out var guild);
        return Task.FromResult(guild);
    }

    public Task<IEnumerable<Guild>> GetAllAsync()
    {
        return Task.FromResult(_guilds.Values.AsEnumerable());
    }

    public Task<IEnumerable<Guild>> GetByRankAsync(GuildRank rank)
    {
        var guilds = _guilds.Values.Where(g => g.Rank == rank);
        return Task.FromResult(guilds);
    }

    public Task<IEnumerable<Guild>> GetByReputationRangeAsync(int minReputation, int maxReputation)
    {
        var guilds = _guilds.Values.Where(g => g.Reputation >= minReputation && g.Reputation <= maxReputation);
        return Task.FromResult(guilds);
    }

    public Task<Guild> AddAsync(Guild guild)
    {
        if (_guildNames.Contains(guild.Name))
            throw new InvalidOperationException($"Guild name '{guild.Name}' already exists.");

        _guilds[guild.Id] = guild;
        _guildNames.Add(guild.Name);
        return Task.FromResult(guild);
    }

    public Task<Guild> UpdateAsync(Guild guild)
    {
        if (!_guilds.ContainsKey(guild.Id))
            throw new InvalidOperationException($"Guild with ID {guild.Id} not found.");

        var existingGuild = _guilds[guild.Id];
        if (existingGuild.Name != guild.Name && _guildNames.Contains(guild.Name))
            throw new InvalidOperationException($"Guild name '{guild.Name}' already exists.");

        _guildNames.Remove(existingGuild.Name);
        _guilds[guild.Id] = guild;
        _guildNames.Add(guild.Name);
        return Task.FromResult(guild);
    }

    public Task DeleteAsync(Guid id)
    {
        if (!_guilds.ContainsKey(id))
            throw new InvalidOperationException($"Guild with ID {id} not found.");

        var guild = _guilds[id];
        _guilds.Remove(id);
        _guildNames.Remove(guild.Name);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(_guilds.ContainsKey(id));
    }

    public Task<bool> NameExistsAsync(string name)
    {
        return Task.FromResult(_guildNames.Contains(name));
    }
} 