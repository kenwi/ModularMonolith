using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;
using FantasyQuestSystem.Domain.Repositories;

namespace FantasyQuestSystem.Application.Services;

public class QuestService : IQuestService
{
    private readonly IQuestRepository _questRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IGuildRepository _guildRepository;

    public QuestService(IQuestRepository questRepository, ICharacterRepository characterRepository, IGuildRepository guildRepository)
    {
        _questRepository = questRepository;
        _characterRepository = characterRepository;
        _guildRepository = guildRepository;
    }

    public async Task<Quest> CreateQuestAsync(string title, string description, QuestDifficulty difficulty, int experienceReward, int goldReward)
    {
        var quest = new Quest(title, description, difficulty, experienceReward, goldReward);
        return await _questRepository.AddAsync(quest);
    }

    public async Task<Quest> GetQuestByIdAsync(Guid id)
    {
        var quest = await _questRepository.GetByIdAsync(id);
        if (quest == null)
            throw new InvalidOperationException($"Quest with ID {id} not found.");
        
        return quest;
    }

    public async Task<IEnumerable<Quest>> GetAllQuestsAsync()
    {
        return await _questRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Quest>> GetAvailableQuestsAsync()
    {
        return await _questRepository.GetAvailableQuestsAsync();
    }

    public async Task<IEnumerable<Quest>> GetQuestsByDifficultyAsync(QuestDifficulty difficulty)
    {
        return await _questRepository.GetByDifficultyAsync(difficulty);
    }

    public async Task<Quest> AssignQuestToCharacterAsync(Guid questId, Guid characterId)
    {
        var quest = await GetQuestByIdAsync(questId);
        var character = await _characterRepository.GetByIdAsync(characterId);
        
        if (character == null)
            throw new InvalidOperationException($"Character with ID {characterId} not found.");

        quest.AssignToCharacter(characterId);
        return await _questRepository.UpdateAsync(quest);
    }

    public async Task<Quest> AssignQuestToGuildAsync(Guid questId, Guid guildId)
    {
        var quest = await GetQuestByIdAsync(questId);
        var guild = await _guildRepository.GetByIdAsync(guildId);
        
        if (guild == null)
            throw new InvalidOperationException($"Guild with ID {guildId} not found.");

        quest.AssignToGuild(guildId);
        return await _questRepository.UpdateAsync(quest);
    }

    public async Task<Quest> CompleteQuestAsync(Guid questId)
    {
        var quest = await GetQuestByIdAsync(questId);
        quest.Complete();
        return await _questRepository.UpdateAsync(quest);
    }

    public async Task<Quest> CancelQuestAsync(Guid questId)
    {
        var quest = await GetQuestByIdAsync(questId);
        quest.Cancel();
        return await _questRepository.UpdateAsync(quest);
    }

    public async Task<Quest> AddRequiredItemToQuestAsync(Guid questId, string itemName)
    {
        var quest = await GetQuestByIdAsync(questId);
        quest.AddRequiredItem(itemName);
        return await _questRepository.UpdateAsync(quest);
    }

    public async Task<Quest> AddRewardedItemToQuestAsync(Guid questId, string itemName)
    {
        var quest = await GetQuestByIdAsync(questId);
        quest.AddRewardedItem(itemName);
        return await _questRepository.UpdateAsync(quest);
    }

    public async Task<Quest> UpdateQuestRewardsAsync(Guid questId, int experienceReward, int goldReward)
    {
        var quest = await GetQuestByIdAsync(questId);
        quest.UpdateRewards(experienceReward, goldReward);
        return await _questRepository.UpdateAsync(quest);
    }

    public async Task DeleteQuestAsync(Guid questId)
    {
        var quest = await GetQuestByIdAsync(questId);
        await _questRepository.DeleteAsync(questId);
    }
} 