using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;
using FantasyQuestSystem.Domain.Repositories;

namespace FantasyQuestSystem.Application.Services;

public class GuildService : IGuildService
{
    private readonly IGuildRepository _guildRepository;
    private readonly ICharacterRepository _characterRepository;

    public GuildService(IGuildRepository guildRepository, ICharacterRepository characterRepository)
    {
        _guildRepository = guildRepository;
        _characterRepository = characterRepository;
    }

    public async Task<Guild> CreateGuildAsync(string name, string description, Guid leaderId, int maxMembers = 50)
    {
        if (await _guildRepository.NameExistsAsync(name))
            throw new InvalidOperationException($"Guild name '{name}' already exists.");

        var leader = await _characterRepository.GetByIdAsync(leaderId);
        if (leader == null)
            throw new InvalidOperationException($"Character with ID {leaderId} not found.");

        var guild = new Guild(name, description, leaderId, maxMembers);
        return await _guildRepository.AddAsync(guild);
    }

    public async Task<Guild> GetGuildByIdAsync(Guid id)
    {
        var guild = await _guildRepository.GetByIdAsync(id);
        if (guild == null)
            throw new InvalidOperationException($"Guild with ID {id} not found.");
        
        return guild;
    }

    public async Task<IEnumerable<Guild>> GetAllGuildsAsync()
    {
        return await _guildRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Guild>> GetGuildsByRankAsync(GuildRank rank)
    {
        return await _guildRepository.GetByRankAsync(rank);
    }

    public async Task<Guild> AddMemberAsync(Guid guildId, Guid characterId)
    {
        var guild = await GetGuildByIdAsync(guildId);
        var character = await _characterRepository.GetByIdAsync(characterId);
        
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        guild.AddMember(characterId);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> RemoveMemberAsync(Guid guildId, Guid characterId)
    {
        var guild = await GetGuildByIdAsync(guildId);
        guild.RemoveMember(characterId);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> ChangeLeaderAsync(Guid guildId, Guid newLeaderId)
    {
        var guild = await GetGuildByIdAsync(guildId);
        var newLeader = await _characterRepository.GetByIdAsync(newLeaderId);
        
        if (newLeader == null)
            throw new InvalidOperationException($"Character with ID {newLeaderId} not found.");

        guild.ChangeLeader(newLeaderId);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> GainReputationAsync(Guid id, int amount)
    {
        var guild = await GetGuildByIdAsync(id);
        guild.GainReputation(amount);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> LoseReputationAsync(Guid id, int amount)
    {
        var guild = await GetGuildByIdAsync(id);
        guild.LoseReputation(amount);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> AddToTreasuryAsync(Guid id, int amount)
    {
        var guild = await GetGuildByIdAsync(id);
        guild.AddToTreasury(amount);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> SpendFromTreasuryAsync(Guid id, int amount)
    {
        var guild = await GetGuildByIdAsync(id);
        guild.SpendFromTreasury(amount);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> CompleteQuestAsync(Guid guildId, Guid questId)
    {
        var guild = await GetGuildByIdAsync(guildId);
        guild.CompleteQuest(questId);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> AddAchievementAsync(Guid id, string achievement)
    {
        var guild = await GetGuildByIdAsync(id);
        guild.AddAchievement(achievement);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> UpdateDescriptionAsync(Guid id, string description)
    {
        var guild = await GetGuildByIdAsync(id);
        guild.UpdateDescription(description);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task<Guild> IncreaseMaxMembersAsync(Guid id, int additionalMembers)
    {
        var guild = await GetGuildByIdAsync(id);
        guild.IncreaseMaxMembers(additionalMembers);
        return await _guildRepository.UpdateAsync(guild);
    }

    public async Task DeleteGuildAsync(Guid id)
    {
        var guild = await GetGuildByIdAsync(id);
        await _guildRepository.DeleteAsync(id);
    }
} 