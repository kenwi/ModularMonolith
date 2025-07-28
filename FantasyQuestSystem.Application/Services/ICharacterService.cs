using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;

namespace FantasyQuestSystem.Application.Services;

public interface ICharacterService
{
    Task<Character> CreateCharacterAsync(string name, CharacterClass characterClass);
    Task<Character> GetCharacterByIdAsync(Guid id);
    Task<IEnumerable<Character>> GetAllCharactersAsync();
    Task<IEnumerable<Character>> GetCharactersByClassAsync(CharacterClass characterClass);
    Task<IEnumerable<Character>> GetCharactersByGuildAsync(Guid guildId);
    Task<Character> UpdateCharacterStatsAsync(Guid id, int health, int mana);
    Task<Character> GainExperienceAsync(Guid id, int experience);
    Task<Character> GainGoldAsync(Guid id, int gold);
    Task<Character> SpendGoldAsync(Guid id, int gold);
    Task<Character> AddItemToInventoryAsync(Guid id, string item);
    Task<Character> RemoveItemFromInventoryAsync(Guid id, string item);
    Task<Character> LearnSkillAsync(Guid id, string skillName);
    Task<Character> JoinGuildAsync(Guid characterId, Guid guildId);
    Task<Character> LeaveGuildAsync(Guid characterId);
    Task<Character> CompleteQuestAsync(Guid characterId, Guid questId);
    Task<Character> RestoreHealthAsync(Guid id, int amount);
    Task<Character> RestoreManaAsync(Guid id, int amount);
    Task<Character> TakeDamageAsync(Guid id, int damage);
    Task<Character> UseManaAsync(Guid id, int amount);
    Task DeleteCharacterAsync(Guid id);
} 