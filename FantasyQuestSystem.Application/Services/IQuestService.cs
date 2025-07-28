using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;

namespace FantasyQuestSystem.Application.Services;

public interface IQuestService
{
    Task<Quest> CreateQuestAsync(string title, string description, QuestDifficulty difficulty, int experienceReward, int goldReward);
    Task<Quest> GetQuestByIdAsync(Guid id);
    Task<IEnumerable<Quest>> GetAllQuestsAsync();
    Task<IEnumerable<Quest>> GetAvailableQuestsAsync();
    Task<IEnumerable<Quest>> GetQuestsByDifficultyAsync(QuestDifficulty difficulty);
    Task<Quest> AssignQuestToCharacterAsync(Guid questId, Guid characterId);
    Task<Quest> AssignQuestToGuildAsync(Guid questId, Guid guildId);
    Task<Quest> CompleteQuestAsync(Guid questId);
    Task<Quest> CancelQuestAsync(Guid questId);
    Task<Quest> AddRequiredItemToQuestAsync(Guid questId, string itemName);
    Task<Quest> AddRewardedItemToQuestAsync(Guid questId, string itemName);
    Task<Quest> UpdateQuestRewardsAsync(Guid questId, int experienceReward, int goldReward);
    Task DeleteQuestAsync(Guid questId);
} 