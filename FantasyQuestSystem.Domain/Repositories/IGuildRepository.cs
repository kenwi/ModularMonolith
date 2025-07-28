using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;

namespace FantasyQuestSystem.Domain.Repositories;

public interface IGuildRepository
{
    Task<Guild?> GetByIdAsync(Guid id);
    Task<IEnumerable<Guild>> GetAllAsync();
    Task<IEnumerable<Guild>> GetByRankAsync(GuildRank rank);
    Task<IEnumerable<Guild>> GetByReputationRangeAsync(int minReputation, int maxReputation);
    Task<Guild> AddAsync(Guild guild);
    Task<Guild> UpdateAsync(Guild guild);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> NameExistsAsync(string name);
} 