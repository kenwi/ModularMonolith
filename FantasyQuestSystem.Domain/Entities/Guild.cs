using FantasyQuestSystem.Domain.Enums;
using FantasyQuestSystem.Domain.Exceptions;

namespace FantasyQuestSystem.Domain.Entities;

public class Guild : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public GuildRank Rank { get; private set; }
    public int Reputation { get; private set; }
    public int Treasury { get; private set; }
    public int MaxMembers { get; private set; }
    public List<Guid> MemberIds { get; private set; }
    public List<Guid> CompletedQuestIds { get; private set; }
    public List<string> Achievements { get; private set; }
    public Guid LeaderId { get; private set; }

    private Guild() { } // For EF Core

    public Guild(string name, string description, Guid leaderId, int maxMembers = 50)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Guild name cannot be empty.");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Guild description cannot be empty.");
        
        if (maxMembers <= 0)
            throw new DomainException("Max members must be positive.");

        Name = name;
        Description = description;
        LeaderId = leaderId;
        MaxMembers = maxMembers;
        Rank = GuildRank.Bronze;
        Reputation = 0;
        Treasury = 1000; // Starting treasury
        MemberIds = new List<Guid> { leaderId };
        CompletedQuestIds = new List<Guid>();
        Achievements = new List<string>();
    }

    public void AddMember(Guid characterId)
    {
        if (MemberIds.Contains(characterId))
            throw new DomainException("Character is already a member of this guild.");

        if (MemberIds.Count >= MaxMembers)
            throw new DomainException("Guild is at maximum capacity.");

        MemberIds.Add(characterId);
        UpdateTimestamp();
    }

    public void RemoveMember(Guid characterId)
    {
        if (characterId == LeaderId)
            throw new DomainException("Cannot remove the guild leader.");

        if (!MemberIds.Contains(characterId))
            throw new DomainException("Character is not a member of this guild.");

        MemberIds.Remove(characterId);
        UpdateTimestamp();
    }

    public void ChangeLeader(Guid newLeaderId)
    {
        if (!MemberIds.Contains(newLeaderId))
            throw new DomainException("New leader must be a member of the guild.");

        LeaderId = newLeaderId;
        UpdateTimestamp();
    }

    public void GainReputation(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Reputation gain must be positive.");

        Reputation += amount;
        CheckRankUp();
        UpdateTimestamp();
    }

    public void LoseReputation(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Reputation loss must be positive.");

        Reputation = Math.Max(Reputation - amount, 0);
        CheckRankDown();
        UpdateTimestamp();
    }

    public void AddToTreasury(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Treasury addition must be positive.");

        Treasury += amount;
        UpdateTimestamp();
    }

    public void SpendFromTreasury(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Treasury spend must be positive.");

        if (Treasury < amount)
            throw new DomainException("Insufficient treasury funds.");

        Treasury -= amount;
        UpdateTimestamp();
    }

    public void CompleteQuest(Guid questId)
    {
        if (CompletedQuestIds.Contains(questId))
            throw new DomainException("Quest already completed by guild.");

        CompletedQuestIds.Add(questId);
        UpdateTimestamp();
    }

    public void AddAchievement(string achievement)
    {
        if (string.IsNullOrWhiteSpace(achievement))
            throw new DomainException("Achievement name cannot be empty.");

        if (!Achievements.Contains(achievement))
            Achievements.Add(achievement);
    }

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Guild description cannot be empty.");

        Description = description;
        UpdateTimestamp();
    }

    public void IncreaseMaxMembers(int additionalMembers)
    {
        if (additionalMembers <= 0)
            throw new DomainException("Additional members must be positive.");

        MaxMembers += additionalMembers;
        UpdateTimestamp();
    }

    private void CheckRankUp()
    {
        if (Reputation >= 10000 && Rank == GuildRank.Bronze)
        {
            Rank = GuildRank.Silver;
            AddAchievement("Silver Guild");
        }
        else if (Reputation >= 25000 && Rank == GuildRank.Silver)
        {
            Rank = GuildRank.Gold;
            AddAchievement("Gold Guild");
        }
        else if (Reputation >= 50000 && Rank == GuildRank.Gold)
        {
            Rank = GuildRank.Platinum;
            AddAchievement("Platinum Guild");
        }
        else if (Reputation >= 100000 && Rank == GuildRank.Platinum)
        {
            Rank = GuildRank.Diamond;
            AddAchievement("Diamond Guild");
        }
    }

    private void CheckRankDown()
    {
        if (Reputation < 10000 && Rank == GuildRank.Silver)
        {
            Rank = GuildRank.Bronze;
        }
        else if (Reputation < 25000 && Rank == GuildRank.Gold)
        {
            Rank = GuildRank.Silver;
        }
        else if (Reputation < 50000 && Rank == GuildRank.Platinum)
        {
            Rank = GuildRank.Gold;
        }
        else if (Reputation < 100000 && Rank == GuildRank.Diamond)
        {
            Rank = GuildRank.Platinum;
        }
    }

    public bool IsMember(Guid characterId) => MemberIds.Contains(characterId);
    public bool IsLeader(Guid characterId) => characterId == LeaderId;
    public bool HasSpace => MemberIds.Count < MaxMembers;
    public int MemberCount => MemberIds.Count;
    public int AvailableSlots => MaxMembers - MemberIds.Count;
} 