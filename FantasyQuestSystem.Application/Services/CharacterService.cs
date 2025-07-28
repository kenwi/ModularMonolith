using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;
using FantasyQuestSystem.Domain.Repositories;

namespace FantasyQuestSystem.Application.Services;

public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IGuildRepository _guildRepository;

    public CharacterService(ICharacterRepository characterRepository, IGuildRepository guildRepository)
    {
        _characterRepository = characterRepository;
        _guildRepository = guildRepository;
    }

    public async Task<Character> CreateCharacterAsync(string name, CharacterClass characterClass)
    {
        if (await _characterRepository.NameExistsAsync(name))
            throw new InvalidOperationException($"Character name '{name}' already exists.");

        var character = new Character(name, characterClass);
        return await _characterRepository.AddAsync(character);
    }

    public async Task<Character> GetCharacterByIdAsync(Guid id)
    {
        var character = await _characterRepository.GetByIdAsync(id);
        if (character == null)
            throw new InvalidOperationException($"Character with ID {id} not found.");
        
        return character;
    }

    public async Task<IEnumerable<Character>> GetAllCharactersAsync()
    {
        return await _characterRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Character>> GetCharactersByClassAsync(CharacterClass characterClass)
    {
        return await _characterRepository.GetByClassAsync(characterClass);
    }

    public async Task<IEnumerable<Character>> GetCharactersByGuildAsync(Guid guildId)
    {
        return await _characterRepository.GetByGuildAsync(guildId);
    }

    public async Task<Character> UpdateCharacterStatsAsync(Guid id, int health, int mana)
    {
        var character = await GetCharacterByIdAsync(id);
        character.RestoreHealth(health);
        character.RestoreMana(mana);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> GainExperienceAsync(Guid id, int experience)
    {
        var character = await GetCharacterByIdAsync(id);
        character.GainExperience(experience);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> GainGoldAsync(Guid id, int gold)
    {
        var character = await GetCharacterByIdAsync(id);
        character.GainGold(gold);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> SpendGoldAsync(Guid id, int gold)
    {
        var character = await GetCharacterByIdAsync(id);
        character.SpendGold(gold);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> AddItemToInventoryAsync(Guid id, string item)
    {
        var character = await GetCharacterByIdAsync(id);
        character.AddToInventory(item);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> RemoveItemFromInventoryAsync(Guid id, string item)
    {
        var character = await GetCharacterByIdAsync(id);
        character.RemoveFromInventory(item);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> LearnSkillAsync(Guid id, string skillName)
    {
        var character = await GetCharacterByIdAsync(id);
        character.LearnSkill(skillName);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> JoinGuildAsync(Guid characterId, Guid guildId)
    {
        var character = await GetCharacterByIdAsync(characterId);
        var guild = await _guildRepository.GetByIdAsync(guildId);
        
        if (guild == null)
            throw new InvalidOperationException($"Guild with ID {guildId} not found.");

        if (!guild.HasSpace)
            throw new InvalidOperationException("Guild is at maximum capacity.");

        character.JoinGuild(guildId);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> LeaveGuildAsync(Guid characterId)
    {
        var character = await GetCharacterByIdAsync(characterId);
        character.LeaveGuild();
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> CompleteQuestAsync(Guid characterId, Guid questId)
    {
        var character = await GetCharacterByIdAsync(characterId);
        character.CompleteQuest(questId);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> RestoreHealthAsync(Guid id, int amount)
    {
        var character = await GetCharacterByIdAsync(id);
        character.RestoreHealth(amount);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> RestoreManaAsync(Guid id, int amount)
    {
        var character = await GetCharacterByIdAsync(id);
        character.RestoreMana(amount);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> TakeDamageAsync(Guid id, int damage)
    {
        var character = await GetCharacterByIdAsync(id);
        character.TakeDamage(damage);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task<Character> UseManaAsync(Guid id, int amount)
    {
        var character = await GetCharacterByIdAsync(id);
        character.UseMana(amount);
        return await _characterRepository.UpdateAsync(character);
    }

    public async Task DeleteCharacterAsync(Guid id)
    {
        var character = await GetCharacterByIdAsync(id);
        await _characterRepository.DeleteAsync(id);
    }
} 