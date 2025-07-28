using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;

namespace FantasyQuestSystem.Domain.Repositories;

public interface ICharacterRepository
{
    Task<Character?> GetByIdAsync(Guid id);
    Task<IEnumerable<Character>> GetAllAsync();
    Task<IEnumerable<Character>> GetByClassAsync(CharacterClass characterClass);
    Task<IEnumerable<Character>> GetByGuildAsync(Guid guildId);
    Task<IEnumerable<Character>> GetByLevelRangeAsync(int minLevel, int maxLevel);
    Task<Character> AddAsync(Character character);
    Task<Character> UpdateAsync(Character character);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> NameExistsAsync(string name);
} 