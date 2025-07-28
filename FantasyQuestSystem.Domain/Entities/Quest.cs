using FantasyQuestSystem.Domain.Enums;
using FantasyQuestSystem.Domain.Exceptions;

namespace FantasyQuestSystem.Domain.Entities;

public class Quest : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public QuestDifficulty Difficulty { get; private set; }
    public QuestStatus Status { get; private set; }
    public int ExperienceReward { get; private set; }
    public int GoldReward { get; private set; }
    public Guid? AssignedCharacterId { get; private set; }
    public Guid? AssignedGuildId { get; private set; }
    public DateTime? AssignedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public List<string> RequiredItems { get; private set; }
    public List<string> RewardedItems { get; private set; }

    private Quest() { } // For EF Core

    public Quest(string title, string description, QuestDifficulty difficulty, int experienceReward, int goldReward)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Quest title cannot be empty.");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Quest description cannot be empty.");
        
        if (experienceReward < 0)
            throw new DomainException("Experience reward cannot be negative.");
        
        if (goldReward < 0)
            throw new DomainException("Gold reward cannot be negative.");

        Title = title;
        Description = description;
        Difficulty = difficulty;
        Status = QuestStatus.Available;
        ExperienceReward = experienceReward;
        GoldReward = goldReward;
        RequiredItems = new List<string>();
        RewardedItems = new List<string>();
    }

    public void AssignToCharacter(Guid characterId)
    {
        if (Status != QuestStatus.Available)
            throw new DomainException("Quest is not available for assignment.");
        
        if (AssignedGuildId.HasValue)
            throw new DomainException("Quest is already assigned to a guild.");

        AssignedCharacterId = characterId;
        AssignedGuildId = null;
        AssignedAt = DateTime.UtcNow;
        Status = QuestStatus.InProgress;
        UpdateTimestamp();
    }

    public void AssignToGuild(Guid guildId)
    {
        if (Status != QuestStatus.Available)
            throw new DomainException("Quest is not available for assignment.");
        
        if (AssignedCharacterId.HasValue)
            throw new DomainException("Quest is already assigned to a character.");

        AssignedGuildId = guildId;
        AssignedCharacterId = null;
        AssignedAt = DateTime.UtcNow;
        Status = QuestStatus.InProgress;
        UpdateTimestamp();
    }

    public void Complete()
    {
        if (Status != QuestStatus.InProgress)
            throw new DomainException("Quest is not in progress.");

        Status = QuestStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }

    public void Cancel()
    {
        if (Status != QuestStatus.InProgress)
            throw new DomainException("Only in-progress quests can be cancelled.");

        Status = QuestStatus.Cancelled;
        UpdateTimestamp();
    }

    public void AddRequiredItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            throw new DomainException("Item name cannot be empty.");

        if (!RequiredItems.Contains(itemName))
            RequiredItems.Add(itemName);
    }

    public void AddRewardedItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
            throw new DomainException("Item name cannot be empty.");

        if (!RewardedItems.Contains(itemName))
            RewardedItems.Add(itemName);
    }

    public void UpdateRewards(int experienceReward, int goldReward)
    {
        if (experienceReward < 0)
            throw new DomainException("Experience reward cannot be negative.");
        
        if (goldReward < 0)
            throw new DomainException("Gold reward cannot be negative.");

        ExperienceReward = experienceReward;
        GoldReward = goldReward;
        UpdateTimestamp();
    }
} 