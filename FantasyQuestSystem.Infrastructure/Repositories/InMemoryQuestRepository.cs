using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;
using FantasyQuestSystem.Domain.Repositories;

namespace FantasyQuestSystem.Infrastructure.Repositories;

public class InMemoryQuestRepository : IQuestRepository
{
    private readonly Dictionary<Guid, Quest> _quests = new();

    public Task<Quest?> GetByIdAsync(Guid id)
    {
        _quests.TryGetValue(id, out var quest);
        return Task.FromResult(quest);
    }

    public Task<IEnumerable<Quest>> GetAllAsync()
    {
        return Task.FromResult(_quests.Values.AsEnumerable());
    }

    public Task<IEnumerable<Quest>> GetByStatusAsync(QuestStatus status)
    {
        var quests = _quests.Values.Where(q => q.Status == status);
        return Task.FromResult(quests);
    }

    public Task<IEnumerable<Quest>> GetByDifficultyAsync(QuestDifficulty difficulty)
    {
        var quests = _quests.Values.Where(q => q.Difficulty == difficulty);
        return Task.FromResult(quests);
    }

    public Task<IEnumerable<Quest>> GetByAssignedCharacterAsync(Guid characterId)
    {
        var quests = _quests.Values.Where(q => q.AssignedCharacterId == characterId);
        return Task.FromResult(quests);
    }

    public Task<IEnumerable<Quest>> GetByAssignedGuildAsync(Guid guildId)
    {
        var quests = _quests.Values.Where(q => q.AssignedGuildId == guildId);
        return Task.FromResult(quests);
    }

    public Task<IEnumerable<Quest>> GetAvailableQuestsAsync()
    {
        var quests = _quests.Values.Where(q => q.Status == QuestStatus.Available);
        return Task.FromResult(quests);
    }

    public Task<Quest> AddAsync(Quest quest)
    {
        _quests[quest.Id] = quest;
        return Task.FromResult(quest);
    }

    public Task<Quest> UpdateAsync(Quest quest)
    {
        if (!_quests.ContainsKey(quest.Id))
            throw new InvalidOperationException($"Quest with ID {quest.Id} not found.");

        _quests[quest.Id] = quest;
        return Task.FromResult(quest);
    }

    public Task DeleteAsync(Guid id)
    {
        if (!_quests.ContainsKey(id))
            throw new InvalidOperationException($"Quest with ID {id} not found.");

        _quests.Remove(id);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(_quests.ContainsKey(id));
    }
} 