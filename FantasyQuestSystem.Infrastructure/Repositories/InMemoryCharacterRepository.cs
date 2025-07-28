using FantasyQuestSystem.Domain.Entities;
using FantasyQuestSystem.Domain.Enums;
using FantasyQuestSystem.Domain.Repositories;

namespace FantasyQuestSystem.Infrastructure.Repositories;

public class InMemoryCharacterRepository : ICharacterRepository
{
    private readonly Dictionary<Guid, Character> _characters = new();
    private readonly HashSet<string> _characterNames = new();

    public Task<Character?> GetByIdAsync(Guid id)
    {
        _characters.TryGetValue(id, out var character);
        return Task.FromResult(character);
    }

    public Task<IEnumerable<Character>> GetAllAsync()
    {
        return Task.FromResult(_characters.Values.AsEnumerable());
    }

    public Task<IEnumerable<Character>> GetByClassAsync(CharacterClass characterClass)
    {
        var characters = _characters.Values.Where(c => c.Class == characterClass);
        return Task.FromResult(characters);
    }

    public Task<IEnumerable<Character>> GetByGuildAsync(Guid guildId)
    {
        var characters = _characters.Values.Where(c => c.GuildId == guildId);
        return Task.FromResult(characters);
    }

    public Task<IEnumerable<Character>> GetByLevelRangeAsync(int minLevel, int maxLevel)
    {
        var characters = _characters.Values.Where(c => c.Level >= minLevel && c.Level <= maxLevel);
        return Task.FromResult(characters);
    }

    public Task<Character> AddAsync(Character character)
    {
        if (_characterNames.Contains(character.Name))
            throw new InvalidOperationException($"Character name '{character.Name}' already exists.");

        _characters[character.Id] = character;
        _characterNames.Add(character.Name);
        return Task.FromResult(character);
    }

    public Task<Character> UpdateAsync(Character character)
    {
        if (!_characters.ContainsKey(character.Id))
            throw new InvalidOperationException($"Character with ID {character.Id} not found.");

        var existingCharacter = _characters[character.Id];
        if (existingCharacter.Name != character.Name && _characterNames.Contains(character.Name))
            throw new InvalidOperationException($"Character name '{character.Name}' already exists.");

        _characterNames.Remove(existingCharacter.Name);
        _characters[character.Id] = character;
        _characterNames.Add(character.Name);
        return Task.FromResult(character);
    }

    public Task DeleteAsync(Guid id)
    {
        if (!_characters.ContainsKey(id))
            throw new InvalidOperationException($"Character with ID {id} not found.");

        var character = _characters[id];
        _characters.Remove(id);
        _characterNames.Remove(character.Name);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(_characters.ContainsKey(id));
    }

    public Task<bool> NameExistsAsync(string name)
    {
        return Task.FromResult(_characterNames.Contains(name));
    }
} 