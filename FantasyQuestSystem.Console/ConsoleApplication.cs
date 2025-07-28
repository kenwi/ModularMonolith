using FantasyQuestSystem.Application.Services;
using FantasyQuestSystem.Domain.Enums;

namespace FantasyQuestSystem.Console;

public class ConsoleApplication
{
    private readonly IQuestService _questService;
    private readonly ICharacterService _characterService;
    private readonly IGuildService _guildService;

    public ConsoleApplication(IQuestService questService, ICharacterService characterService, IGuildService guildService)
    {
        _questService = questService;
        _characterService = characterService;
        _guildService = guildService;
    }

    public async Task RunAsync()
    {
        System.Console.WriteLine("🎮 Welcome to the Fantasy Quest Management System! 🎮");
        System.Console.WriteLine("==================================================");
        
        // Seed some initial data
        await SeedInitialDataAsync();

        while (true)
        {
            try
            {
                await ShowMainMenuAsync();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"❌ Error: {ex.Message}");
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
        }
    }

    private async Task ShowMainMenuAsync()
    {
        System.Console.WriteLine("\n📋 Main Menu:");
        System.Console.WriteLine("1. Quest Management");
        System.Console.WriteLine("2. Character Management");
        System.Console.WriteLine("3. Guild Management");
        System.Console.WriteLine("4. View Statistics");
        System.Console.WriteLine("5. Exit");
        System.Console.Write("\nSelect an option: ");

        var choice = System.Console.ReadLine();

        switch (choice)
        {
            case "1":
                await ShowQuestMenuAsync();
                break;
            case "2":
                await ShowCharacterMenuAsync();
                break;
            case "3":
                await ShowGuildMenuAsync();
                break;
            case "4":
                await ShowStatisticsAsync();
                break;
            case "5":
                System.Console.WriteLine("👋 Thanks for playing! Goodbye!");
                Environment.Exit(0);
                break;
            default:
                System.Console.WriteLine("❌ Invalid option. Please try again.");
                break;
        }
    }

    private async Task ShowQuestMenuAsync()
    {
        while (true)
        {
            System.Console.WriteLine("\n🗺️ Quest Management:");
            System.Console.WriteLine("1. Create New Quest");
            System.Console.WriteLine("2. View All Quests");
            System.Console.WriteLine("3. View Available Quests");
            System.Console.WriteLine("4. Assign Quest to Character");
            System.Console.WriteLine("5. Assign Quest to Guild");
            System.Console.WriteLine("6. Complete Quest");
            System.Console.WriteLine("7. Cancel Quest");
            System.Console.WriteLine("8. Back to Main Menu");
            System.Console.Write("\nSelect an option: ");

            var choice = System.Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateQuestAsync();
                    break;
                case "2":
                    await ViewAllQuestsAsync();
                    break;
                case "3":
                    await ViewAvailableQuestsAsync();
                    break;
                case "4":
                    await AssignQuestToCharacterAsync();
                    break;
                case "5":
                    await AssignQuestToGuildAsync();
                    break;
                case "6":
                    await CompleteQuestAsync();
                    break;
                case "7":
                    await CancelQuestAsync();
                    break;
                case "8":
                    return;
                default:
                    System.Console.WriteLine("❌ Invalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task ShowCharacterMenuAsync()
    {
        while (true)
        {
            System.Console.WriteLine("\n⚔️ Character Management:");
            System.Console.WriteLine("1. Create New Character");
            System.Console.WriteLine("2. View All Characters");
            System.Console.WriteLine("3. View Character Details");
            System.Console.WriteLine("4. Join Guild");
            System.Console.WriteLine("5. Leave Guild");
            System.Console.WriteLine("6. Gain Experience");
            System.Console.WriteLine("7. Add Item to Inventory");
            System.Console.WriteLine("8. Learn Skill");
            System.Console.WriteLine("9. Back to Main Menu");
            System.Console.Write("\nSelect an option: ");

            var choice = System.Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateCharacterAsync();
                    break;
                case "2":
                    await ViewAllCharactersAsync();
                    break;
                case "3":
                    await ViewCharacterDetailsAsync();
                    break;
                case "4":
                    await JoinGuildAsync();
                    break;
                case "5":
                    await LeaveGuildAsync();
                    break;
                case "6":
                    await GainExperienceAsync();
                    break;
                case "7":
                    await AddItemToInventoryAsync();
                    break;
                case "8":
                    await LearnSkillAsync();
                    break;
                case "9":
                    return;
                default:
                    System.Console.WriteLine("❌ Invalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task ShowGuildMenuAsync()
    {
        while (true)
        {
            System.Console.WriteLine("\n🏰 Guild Management:");
            System.Console.WriteLine("1. Create New Guild");
            System.Console.WriteLine("2. View All Guilds");
            System.Console.WriteLine("3. View Guild Details");
            System.Console.WriteLine("4. Add Member to Guild");
            System.Console.WriteLine("5. Remove Member from Guild");
            System.Console.WriteLine("6. Change Guild Leader");
            System.Console.WriteLine("7. Gain Reputation");
            System.Console.WriteLine("8. Add to Treasury");
            System.Console.WriteLine("9. Back to Main Menu");
            System.Console.Write("\nSelect an option: ");

            var choice = System.Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateGuildAsync();
                    break;
                case "2":
                    await ViewAllGuildsAsync();
                    break;
                case "3":
                    await ViewGuildDetailsAsync();
                    break;
                case "4":
                    await AddMemberToGuildAsync();
                    break;
                case "5":
                    await RemoveMemberFromGuildAsync();
                    break;
                case "6":
                    await ChangeGuildLeaderAsync();
                    break;
                case "7":
                    await GainReputationAsync();
                    break;
                case "8":
                    await AddToTreasuryAsync();
                    break;
                case "9":
                    return;
                default:
                    System.Console.WriteLine("❌ Invalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task SeedInitialDataAsync()
    {
        try
        {
            // Create some sample characters
            var warrior = await _characterService.CreateCharacterAsync("Thorin", CharacterClass.Warrior);
            var mage = await _characterService.CreateCharacterAsync("Gandalf", CharacterClass.Mage);
            var ranger = await _characterService.CreateCharacterAsync("Legolas", CharacterClass.Ranger);
            var cleric = await _characterService.CreateCharacterAsync("Aragorn", CharacterClass.Cleric);

            // Create a guild
            var guild = await _guildService.CreateGuildAsync("Fellowship of the Ring", "A legendary guild of heroes", warrior.Id);

            // Add members to guild
            await _guildService.AddMemberAsync(guild.Id, mage.Id);
            await _guildService.AddMemberAsync(guild.Id, ranger.Id);
            await _guildService.AddMemberAsync(guild.Id, cleric.Id);

            // Create some quests
            await _questService.CreateQuestAsync("Destroy the Ring", "Take the One Ring to Mount Doom and destroy it", QuestDifficulty.Legendary, 10000, 5000);
            await _questService.CreateQuestAsync("Defeat the Balrog", "Face the ancient demon in the depths of Moria", QuestDifficulty.Epic, 5000, 2000);
            await _questService.CreateQuestAsync("Rescue the Hobbits", "Save the hobbits from the Nazgul", QuestDifficulty.Hard, 2000, 1000);
            await _questService.CreateQuestAsync("Gather Supplies", "Collect food and supplies for the journey", QuestDifficulty.Easy, 500, 100);

            System.Console.WriteLine("✅ Sample data created successfully!");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"⚠️ Warning: Could not create sample data: {ex.Message}");
        }
    }

    // Quest Management Methods
    private async Task CreateQuestAsync()
    {
        System.Console.Write("Enter quest title: ");
        var title = System.Console.ReadLine() ?? "";
        
        System.Console.Write("Enter quest description: ");
        var description = System.Console.ReadLine() ?? "";
        
        System.Console.WriteLine("Select difficulty:");
        System.Console.WriteLine("1. Easy (1)");
        System.Console.WriteLine("2. Medium (2)");
        System.Console.WriteLine("3. Hard (3)");
        System.Console.WriteLine("4. Epic (4)");
        System.Console.WriteLine("5. Legendary (5)");
        System.Console.Write("Enter difficulty (1-5): ");
        
        if (!int.TryParse(System.Console.ReadLine(), out var difficultyChoice) || difficultyChoice < 1 || difficultyChoice > 5)
        {
            System.Console.WriteLine("❌ Invalid difficulty choice.");
            return;
        }

        System.Console.Write("Enter experience reward: ");
        if (!int.TryParse(System.Console.ReadLine(), out var experienceReward))
        {
            System.Console.WriteLine("❌ Invalid experience reward.");
            return;
        }

        System.Console.Write("Enter gold reward: ");
        if (!int.TryParse(System.Console.ReadLine(), out var goldReward))
        {
            System.Console.WriteLine("❌ Invalid gold reward.");
            return;
        }

        var difficulty = (QuestDifficulty)difficultyChoice;
        var quest = await _questService.CreateQuestAsync(title, description, difficulty, experienceReward, goldReward);
        
        System.Console.WriteLine($"✅ Quest '{quest.Title}' created successfully with ID: {quest.Id}");
    }

    private async Task ViewAllQuestsAsync()
    {
        var quests = await _questService.GetAllQuestsAsync();
        
        if (!quests.Any())
        {
            System.Console.WriteLine("📭 No quests found.");
            return;
        }

        System.Console.WriteLine("\n🗺️ All Quests:");
        foreach (var quest in quests)
        {
            System.Console.WriteLine($"ID: {quest.Id}");
            System.Console.WriteLine($"Title: {quest.Title}");
            System.Console.WriteLine($"Status: {quest.Status}");
            System.Console.WriteLine($"Difficulty: {quest.Difficulty}");
            System.Console.WriteLine($"Rewards: {quest.ExperienceReward} XP, {quest.GoldReward} Gold");
            System.Console.WriteLine("---");
        }
    }

    private async Task ViewAvailableQuestsAsync()
    {
        var quests = await _questService.GetAvailableQuestsAsync();
        
        if (!quests.Any())
        {
            System.Console.WriteLine("📭 No available quests found.");
            return;
        }

        System.Console.WriteLine("\n✅ Available Quests:");
        foreach (var quest in quests)
        {
            System.Console.WriteLine($"ID: {quest.Id}");
            System.Console.WriteLine($"Title: {quest.Title}");
            System.Console.WriteLine($"Difficulty: {quest.Difficulty}");
            System.Console.WriteLine($"Rewards: {quest.ExperienceReward} XP, {quest.GoldReward} Gold");
            System.Console.WriteLine("---");
        }
    }

    private async Task AssignQuestToCharacterAsync()
    {
        System.Console.Write("Enter quest ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var questId))
        {
            System.Console.WriteLine("❌ Invalid quest ID.");
            return;
        }

        System.Console.Write("Enter character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var characterId))
        {
            System.Console.WriteLine("❌ Invalid character ID.");
            return;
        }

        var quest = await _questService.AssignQuestToCharacterAsync(questId, characterId);
        System.Console.WriteLine($"✅ Quest '{quest.Title}' assigned to character successfully!");
    }

    private async Task AssignQuestToGuildAsync()
    {
        System.Console.Write("Enter quest ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var questId))
        {
            System.Console.WriteLine("❌ Invalid quest ID.");
            return;
        }

        System.Console.Write("Enter guild ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var guildId))
        {
            System.Console.WriteLine("❌ Invalid guild ID.");
            return;
        }

        var quest = await _questService.AssignQuestToGuildAsync(questId, guildId);
        System.Console.WriteLine($"✅ Quest '{quest.Title}' assigned to guild successfully!");
    }

    private async Task CompleteQuestAsync()
    {
        System.Console.Write("Enter quest ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var questId))
        {
            System.Console.WriteLine("❌ Invalid quest ID.");
            return;
        }

        var quest = await _questService.CompleteQuestAsync(questId);
        System.Console.WriteLine($"✅ Quest '{quest.Title}' completed successfully!");
    }

    private async Task CancelQuestAsync()
    {
        System.Console.Write("Enter quest ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var questId))
        {
            System.Console.WriteLine("❌ Invalid quest ID.");
            return;
        }

        var quest = await _questService.CancelQuestAsync(questId);
        System.Console.WriteLine($"❌ Quest '{quest.Title}' cancelled successfully!");
    }

    // Character Management Methods
    private async Task CreateCharacterAsync()
    {
        System.Console.Write("Enter character name: ");
        var name = System.Console.ReadLine() ?? "";
        
        System.Console.WriteLine("Select character class:");
        System.Console.WriteLine("1. Warrior");
        System.Console.WriteLine("2. Mage");
        System.Console.WriteLine("3. Ranger");
        System.Console.WriteLine("4. Cleric");
        System.Console.Write("Enter class (1-4): ");
        
        if (!int.TryParse(System.Console.ReadLine(), out var classChoice) || classChoice < 1 || classChoice > 4)
        {
            System.Console.WriteLine("❌ Invalid class choice.");
            return;
        }

        var characterClass = (CharacterClass)classChoice;
        var character = await _characterService.CreateCharacterAsync(name, characterClass);
        
        System.Console.WriteLine($"✅ Character '{character.Name}' created successfully with ID: {character.Id}");
    }

    private async Task ViewAllCharactersAsync()
    {
        var characters = await _characterService.GetAllCharactersAsync();
        
        if (!characters.Any())
        {
            System.Console.WriteLine("📭 No characters found.");
            return;
        }

        System.Console.WriteLine("\n⚔️ All Characters:");
        foreach (var character in characters)
        {
            System.Console.WriteLine($"ID: {character.Id}");
            System.Console.WriteLine($"Name: {character.Name}");
            System.Console.WriteLine($"Class: {character.Class}");
            System.Console.WriteLine($"Level: {character.Level}");
            System.Console.WriteLine($"Experience: {character.Experience}");
            System.Console.WriteLine($"Gold: {character.Gold}");
            System.Console.WriteLine("---");
        }
    }

    private async Task ViewCharacterDetailsAsync()
    {
        System.Console.Write("Enter character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var characterId))
        {
            System.Console.WriteLine("❌ Invalid character ID.");
            return;
        }

        var character = await _characterService.GetCharacterByIdAsync(characterId);
        
        System.Console.WriteLine($"\n⚔️ Character Details:");
        System.Console.WriteLine($"Name: {character.Name}");
        System.Console.WriteLine($"Class: {character.Class}");
        System.Console.WriteLine($"Level: {character.Level}");
        System.Console.WriteLine($"Experience: {character.Experience}/{character.Level * 100}");
        System.Console.WriteLine($"Gold: {character.Gold}");
        System.Console.WriteLine($"Health: {character.Health}/{character.MaxHealth}");
        System.Console.WriteLine($"Mana: {character.Mana}/{character.MaxMana}");
        System.Console.WriteLine($"Strength: {character.Strength}");
        System.Console.WriteLine($"Dexterity: {character.Dexterity}");
        System.Console.WriteLine($"Intelligence: {character.Intelligence}");
        System.Console.WriteLine($"Constitution: {character.Constitution}");
        System.Console.WriteLine($"Guild ID: {character.GuildId}");
        System.Console.WriteLine($"Inventory: {string.Join(", ", character.Inventory)}");
        System.Console.WriteLine($"Skills: {string.Join(", ", character.Skills)}");
    }

    private async Task JoinGuildAsync()
    {
        System.Console.Write("Enter character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var characterId))
        {
            System.Console.WriteLine("❌ Invalid character ID.");
            return;
        }

        System.Console.Write("Enter guild ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var guildId))
        {
            System.Console.WriteLine("❌ Invalid guild ID.");
            return;
        }

        var character = await _characterService.JoinGuildAsync(characterId, guildId);
        System.Console.WriteLine($"✅ Character '{character.Name}' joined guild successfully!");
    }

    private async Task LeaveGuildAsync()
    {
        System.Console.Write("Enter character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var characterId))
        {
            System.Console.WriteLine("❌ Invalid character ID.");
            return;
        }

        var character = await _characterService.LeaveGuildAsync(characterId);
        System.Console.WriteLine($"✅ Character '{character.Name}' left guild successfully!");
    }

    private async Task GainExperienceAsync()
    {
        System.Console.Write("Enter character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var characterId))
        {
            System.Console.WriteLine("❌ Invalid character ID.");
            return;
        }

        System.Console.Write("Enter experience amount: ");
        if (!int.TryParse(System.Console.ReadLine(), out var experience))
        {
            System.Console.WriteLine("❌ Invalid experience amount.");
            return;
        }

        var character = await _characterService.GainExperienceAsync(characterId, experience);
        System.Console.WriteLine($"✅ Character '{character.Name}' gained {experience} experience!");
    }

    private async Task AddItemToInventoryAsync()
    {
        System.Console.Write("Enter character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var characterId))
        {
            System.Console.WriteLine("❌ Invalid character ID.");
            return;
        }

        System.Console.Write("Enter item name: ");
        var itemName = System.Console.ReadLine() ?? "";

        var character = await _characterService.AddItemToInventoryAsync(characterId, itemName);
        System.Console.WriteLine($"✅ Item '{itemName}' added to {character.Name}'s inventory!");
    }

    private async Task LearnSkillAsync()
    {
        System.Console.Write("Enter character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var characterId))
        {
            System.Console.WriteLine("❌ Invalid character ID.");
            return;
        }

        System.Console.Write("Enter skill name: ");
        var skillName = System.Console.ReadLine() ?? "";

        var character = await _characterService.LearnSkillAsync(characterId, skillName);
        System.Console.WriteLine($"✅ Character '{character.Name}' learned skill '{skillName}'!");
    }

    // Guild Management Methods
    private async Task CreateGuildAsync()
    {
        System.Console.Write("Enter guild name: ");
        var name = System.Console.ReadLine() ?? "";
        
        System.Console.Write("Enter guild description: ");
        var description = System.Console.ReadLine() ?? "";
        
        System.Console.Write("Enter leader character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var leaderId))
        {
            System.Console.WriteLine("❌ Invalid leader ID.");
            return;
        }

        System.Console.Write("Enter max members (default 50): ");
        if (!int.TryParse(System.Console.ReadLine(), out var maxMembers))
        {
            maxMembers = 50;
        }

        var guild = await _guildService.CreateGuildAsync(name, description, leaderId, maxMembers);
        System.Console.WriteLine($"✅ Guild '{guild.Name}' created successfully with ID: {guild.Id}");
    }

    private async Task ViewAllGuildsAsync()
    {
        var guilds = await _guildService.GetAllGuildsAsync();
        
        if (!guilds.Any())
        {
            System.Console.WriteLine("📭 No guilds found.");
            return;
        }

        System.Console.WriteLine("\n🏰 All Guilds:");
        foreach (var guild in guilds)
        {
            System.Console.WriteLine($"ID: {guild.Id}");
            System.Console.WriteLine($"Name: {guild.Name}");
            System.Console.WriteLine($"Rank: {guild.Rank}");
            System.Console.WriteLine($"Reputation: {guild.Reputation}");
            System.Console.WriteLine($"Members: {guild.MemberCount}/{guild.MaxMembers}");
            System.Console.WriteLine($"Treasury: {guild.Treasury} Gold");
            System.Console.WriteLine("---");
        }
    }

    private async Task ViewGuildDetailsAsync()
    {
        System.Console.Write("Enter guild ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var guildId))
        {
            System.Console.WriteLine("❌ Invalid guild ID.");
            return;
        }

        var guild = await _guildService.GetGuildByIdAsync(guildId);
        
        System.Console.WriteLine($"\n🏰 Guild Details:");
        System.Console.WriteLine($"Name: {guild.Name}");
        System.Console.WriteLine($"Description: {guild.Description}");
        System.Console.WriteLine($"Rank: {guild.Rank}");
        System.Console.WriteLine($"Reputation: {guild.Reputation}");
        System.Console.WriteLine($"Treasury: {guild.Treasury} Gold");
        System.Console.WriteLine($"Members: {guild.MemberCount}/{guild.MaxMembers}");
        System.Console.WriteLine($"Leader ID: {guild.LeaderId}");
        System.Console.WriteLine($"Member IDs: {string.Join(", ", guild.MemberIds)}");
        System.Console.WriteLine($"Achievements: {string.Join(", ", guild.Achievements)}");
    }

    private async Task AddMemberToGuildAsync()
    {
        System.Console.Write("Enter guild ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var guildId))
        {
            System.Console.WriteLine("❌ Invalid guild ID.");
            return;
        }

        System.Console.Write("Enter character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var characterId))
        {
            System.Console.WriteLine("❌ Invalid character ID.");
            return;
        }

        var guild = await _guildService.AddMemberAsync(guildId, characterId);
        System.Console.WriteLine($"✅ Character added to guild '{guild.Name}' successfully!");
    }

    private async Task RemoveMemberFromGuildAsync()
    {
        System.Console.Write("Enter guild ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var guildId))
        {
            System.Console.WriteLine("❌ Invalid guild ID.");
            return;
        }

        System.Console.Write("Enter character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var characterId))
        {
            System.Console.WriteLine("❌ Invalid character ID.");
            return;
        }

        var guild = await _guildService.RemoveMemberAsync(guildId, characterId);
        System.Console.WriteLine($"✅ Character removed from guild '{guild.Name}' successfully!");
    }

    private async Task ChangeGuildLeaderAsync()
    {
        System.Console.Write("Enter guild ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var guildId))
        {
            System.Console.WriteLine("❌ Invalid guild ID.");
            return;
        }

        System.Console.Write("Enter new leader character ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var newLeaderId))
        {
            System.Console.WriteLine("❌ Invalid new leader ID.");
            return;
        }

        var guild = await _guildService.ChangeLeaderAsync(guildId, newLeaderId);
        System.Console.WriteLine($"✅ Guild '{guild.Name}' leader changed successfully!");
    }

    private async Task GainReputationAsync()
    {
        System.Console.Write("Enter guild ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var guildId))
        {
            System.Console.WriteLine("❌ Invalid guild ID.");
            return;
        }

        System.Console.Write("Enter reputation amount: ");
        if (!int.TryParse(System.Console.ReadLine(), out var reputation))
        {
            System.Console.WriteLine("❌ Invalid reputation amount.");
            return;
        }

        var guild = await _guildService.GainReputationAsync(guildId, reputation);
        System.Console.WriteLine($"✅ Guild '{guild.Name}' gained {reputation} reputation!");
    }

    private async Task AddToTreasuryAsync()
    {
        System.Console.Write("Enter guild ID: ");
        if (!Guid.TryParse(System.Console.ReadLine(), out var guildId))
        {
            System.Console.WriteLine("❌ Invalid guild ID.");
            return;
        }

        System.Console.Write("Enter gold amount: ");
        if (!int.TryParse(System.Console.ReadLine(), out var gold))
        {
            System.Console.WriteLine("❌ Invalid gold amount.");
            return;
        }

        var guild = await _guildService.AddToTreasuryAsync(guildId, gold);
        System.Console.WriteLine($"✅ {gold} gold added to guild '{guild.Name}' treasury!");
    }

    private async Task ShowStatisticsAsync()
    {
        var quests = await _questService.GetAllQuestsAsync();
        var characters = await _characterService.GetAllCharactersAsync();
        var guilds = await _guildService.GetAllGuildsAsync();

        System.Console.WriteLine("\n📊 System Statistics:");
        System.Console.WriteLine($"Total Quests: {quests.Count()}");
        System.Console.WriteLine($"Available Quests: {quests.Count(q => q.Status == QuestStatus.Available)}");
        System.Console.WriteLine($"In Progress Quests: {quests.Count(q => q.Status == QuestStatus.InProgress)}");
        System.Console.WriteLine($"Completed Quests: {quests.Count(q => q.Status == QuestStatus.Completed)}");
        System.Console.WriteLine($"Total Characters: {characters.Count()}");
        System.Console.WriteLine($"Total Guilds: {guilds.Count()}");
        System.Console.WriteLine($"Average Character Level: {(characters.Any() ? characters.Average(c => c.Level): 0):F1}");
        System.Console.WriteLine($"Total Gold in System: {characters.Sum(c => c.Gold) + guilds.Sum(g => g.Treasury)}");
    }
} 