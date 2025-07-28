using FantasyQuestSystem.Domain.Enums;
using FantasyQuestSystem.Domain.Exceptions;

namespace FantasyQuestSystem.Domain.Entities;

public class Character : BaseEntity
{
    public string Name { get; private set; }
    public CharacterClass Class { get; private set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int Gold { get; private set; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Mana { get; private set; }
    public int MaxMana { get; private set; }
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Intelligence { get; private set; }
    public int Constitution { get; private set; }
    public Guid? GuildId { get; private set; }
    public List<string> Inventory { get; private set; }
    public List<string> Skills { get; private set; }
    public List<Guid> CompletedQuestIds { get; private set; }

    private Character() { } // For EF Core

    public Character(string name, CharacterClass characterClass)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Character name cannot be empty.");

        Name = name;
        Class = characterClass;
        Level = 1;
        Experience = 0;
        Gold = 100; // Starting gold
        Inventory = new List<string>();
        Skills = new List<string>();
        CompletedQuestIds = new List<Guid>();

        // Set base stats based on class
        SetBaseStats();
    }

    private void SetBaseStats()
    {
        switch (Class)
        {
            case CharacterClass.Warrior:
                MaxHealth = 120;
                MaxMana = 50;
                Strength = 18;
                Dexterity = 12;
                Intelligence = 8;
                Constitution = 16;
                break;
            case CharacterClass.Mage:
                MaxHealth = 80;
                MaxMana = 120;
                Strength = 8;
                Dexterity = 10;
                Intelligence = 18;
                Constitution = 12;
                break;
            case CharacterClass.Ranger:
                MaxHealth = 100;
                MaxMana = 80;
                Strength = 14;
                Dexterity = 18;
                Intelligence = 12;
                Constitution = 14;
                break;
            case CharacterClass.Cleric:
                MaxHealth = 110;
                MaxMana = 100;
                Strength = 12;
                Dexterity = 10;
                Intelligence = 16;
                Constitution = 15;
                break;
        }

        Health = MaxHealth;
        Mana = MaxMana;
    }

    public void GainExperience(int experience)
    {
        if (experience <= 0)
            throw new DomainException("Experience gain must be positive.");

        Experience += experience;
        CheckLevelUp();
        UpdateTimestamp();
    }

    public void GainGold(int gold)
    {
        if (gold <= 0)
            throw new DomainException("Gold gain must be positive.");

        Gold += gold;
        UpdateTimestamp();
    }

    public void SpendGold(int gold)
    {
        if (gold <= 0)
            throw new DomainException("Gold spend must be positive.");

        if (Gold < gold)
            throw new DomainException("Insufficient gold.");

        Gold -= gold;
        UpdateTimestamp();
    }

    public void AddToInventory(string item)
    {
        if (string.IsNullOrWhiteSpace(item))
            throw new DomainException("Item name cannot be empty.");

        if (!Inventory.Contains(item))
            Inventory.Add(item);
    }

    public void RemoveFromInventory(string item)
    {
        if (!Inventory.Contains(item))
            throw new DomainException("Item not found in inventory.");

        Inventory.Remove(item);
    }

    public void LearnSkill(string skillName)
    {
        if (string.IsNullOrWhiteSpace(skillName))
            throw new DomainException("Skill name cannot be empty.");

        if (!Skills.Contains(skillName))
            Skills.Add(skillName);
    }

    public void JoinGuild(Guid guildId)
    {
        if (GuildId.HasValue)
            throw new DomainException("Character is already in a guild.");

        GuildId = guildId;
        UpdateTimestamp();
    }

    public void LeaveGuild()
    {
        if (!GuildId.HasValue)
            throw new DomainException("Character is not in a guild.");

        GuildId = null;
        UpdateTimestamp();
    }

    public void CompleteQuest(Guid questId)
    {
        if (CompletedQuestIds.Contains(questId))
            throw new DomainException("Quest already completed.");

        CompletedQuestIds.Add(questId);
        UpdateTimestamp();
    }

    public void RestoreHealth(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Health restoration must be positive.");

        Health = Math.Min(Health + amount, MaxHealth);
        UpdateTimestamp();
    }

    public void RestoreMana(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Mana restoration must be positive.");

        Mana = Math.Min(Mana + amount, MaxMana);
        UpdateTimestamp();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new DomainException("Damage must be positive.");

        Health = Math.Max(Health - damage, 0);
        UpdateTimestamp();
    }

    public void UseMana(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Mana usage must be positive.");

        if (Mana < amount)
            throw new DomainException("Insufficient mana.");

        Mana -= amount;
        UpdateTimestamp();
    }

    private void CheckLevelUp()
    {
        int experienceNeeded = Level * 100; // Simple leveling formula
        
        while (Experience >= experienceNeeded)
        {
            Experience -= experienceNeeded;
            Level++;
            
            // Increase stats on level up
            MaxHealth += 10;
            MaxMana += 5;
            Strength += 1;
            Dexterity += 1;
            Intelligence += 1;
            Constitution += 1;
            
            // Restore health and mana to full
            Health = MaxHealth;
            Mana = MaxMana;
            
            experienceNeeded = Level * 100;
        }
    }

    public bool IsAlive => Health > 0;
    public bool HasMana => Mana > 0;
    public int ExperienceToNextLevel => (Level * 100) - Experience;
} 