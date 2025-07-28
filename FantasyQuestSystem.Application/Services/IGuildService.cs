using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;

namespace FantasyQuestSystem.Application.Services;

public interface IGuildService
{
    Task<Guild> CreateGuildAsync(string name, string description, Guid leaderId, int maxMembers = 50);
    Task<Guild> GetGuildByIdAsync(Guid id);
    Task<IEnumerable<Guild>> GetAllGuildsAsync();
    Task<IEnumerable<Guild>> GetGuildsByRankAsync(GuildRank rank);
    Task<Guild> AddMemberAsync(Guid guildId, Guid characterId);
    Task<Guild> RemoveMemberAsync(Guid guildId, Guid characterId);
    Task<Guild> ChangeLeaderAsync(Guid guildId, Guid newLeaderId);
    Task<Guild> GainReputationAsync(Guid id, int amount);
    Task<Guild> LoseReputationAsync(Guid id, int amount);
    Task<Guild> AddToTreasuryAsync(Guid id, int amount);
    Task<Guild> SpendFromTreasuryAsync(Guid id, int amount);
    Task<Guild> CompleteQuestAsync(Guid guildId, Guid questId);
    Task<Guild> AddAchievementAsync(Guid id, string achievement);
    Task<Guild> UpdateDescriptionAsync(Guid id, string description);
    Task<Guild> IncreaseMaxMembersAsync(Guid id, int additionalMembers);
    Task DeleteGuildAsync(Guid id);
} 