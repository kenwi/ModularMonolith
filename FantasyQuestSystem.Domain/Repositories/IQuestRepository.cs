using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;

namespace FantasyQuestSystem.Domain.Repositories;

public interface IQuestRepository
{
    Task<Quest?> GetByIdAsync(Guid id);
    Task<IEnumerable<Quest>> GetAllAsync();
    Task<IEnumerable<Quest>> GetByStatusAsync(QuestStatus status);
    Task<IEnumerable<Quest>> GetByDifficultyAsync(QuestDifficulty difficulty);
    Task<IEnumerable<Quest>> GetByAssignedCharacterAsync(Guid characterId);
    Task<IEnumerable<Quest>> GetByAssignedGuildAsync(Guid guildId);
    Task<IEnumerable<Quest>> GetAvailableQuestsAsync();
    Task<Quest> AddAsync(Quest quest);
    Task<Quest> UpdateAsync(Quest quest);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
} 