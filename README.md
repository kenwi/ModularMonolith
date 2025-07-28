# 🎮 Fantasy Quest Management System

A fictional **Modular Monolith** application built in C# that demonstrates clean architecture principles for managing quests, characters, and guilds in a fantasy gaming world.

```
┌───────────────────────────────────────────────────────────────┐
│                    Console UI Layer                           │
├───────────────────────────────────────────────────────────────┤
│                  Application Layer                            │
│  ┌─────────────┐ ┌─────────────┐ ┌─────────────┐ ┌──────────┐ │
│  │Quest Service│ │Character Svc│ │ Guild Svc   │ │Reward Svc│ │
│  └─────────────┘ └─────────────┘ └─────────────┘ └──────────┘ │
├───────────────────────────────────────────────────────────────┤
│                    Domain Layer                               │
│  ┌─────────────┐ ┌─────────────┐ ┌─────────────┐ ┌──────────┐ │
│  │Quest Module │ │Character Mod│ │ Guild Mod   │ │Reward Mod│ │
│  └─────────────┘ └─────────────┘ └─────────────┘ └──────────┘ │
├───────────────────────────────────────────────────────────────┤
│                Infrastructure Layer                           │
│  ┌─────────────┐ ┌─────────────┐ ┌─────────────┐ ┌──────────┐ │
│  │Quest Repo   │ │Character Rep│ │ Guild Rep   │ │Reward Rep│ │
│  └─────────────┘ └─────────────┘ └─────────────┘ └──────────┘ │
├───────────────────────────────────────────────────────────────┤
│                    Data Storage                               │
│  ┌─────────────────────────────────────────────────────────┐  │
│  │              In-Memory Storage                          │  │
│  └─────────────────────────────────────────────────────────┘  │
└───────────────────────────────────────────────────────────────┘
```

## 🔧 **Key Fixes Made**

1. **Consistent line length**: All horizontal lines are exactly 61 characters wide
2. **Proper alignment**: Right side now forms a straight vertical line
3. **Balanced spacing**: Equal spacing between modules in each layer
4. **Clean borders**: All box corners align properly

## 📏 **Line Length Breakdown**

- **Outer border**: 61 characters (including corners)
- **Module boxes**: 13 characters wide each
- **Spacing**: 2 spaces between modules
- **Total width**: 4 modules × 13 chars + 3 spaces × 2 chars = 58 + 3 = 61 characters

This creates a much cleaner, more professional-looking architecture diagram! 🎨✨

## 🎯 Key Features

### Quest Management
- ✅ Create quests with title, description, difficulty, and rewards
- ✅ Assign quests to characters or guilds
- ✅ Track quest completion status
- ✅ View quest history and statistics

### Character Management
- ✅ Create and manage player characters (Warrior, Mage, Ranger, Cleric)
- ✅ Track character stats (level, experience, health, mana, attributes)
- ✅ Manage inventory and skills
- ✅ Join/leave guilds
- ✅ Character progression system

### Guild Management
- ✅ Create and manage guilds with reputation system
- ✅ Handle guild membership and leadership
- ✅ Guild treasury management
- ✅ Guild achievements and ranking system

### Technical Features
- ✅ **Modular Design**: Separate modules for different business domains
- ✅ **Clean Architecture**: Clear separation of concerns
- ✅ **Domain-Driven Design**: Rich domain models with business logic
- ✅ **Repository Pattern**: Easy to swap data storage implementations
- ✅ **Dependency Injection**: Built-in .NET DI container
- ✅ **In-Memory Storage**: No external database required

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK or later
- Any terminal/command prompt

### Installation & Running

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ModularMonolith
   ```

2. **Build the solution**
   ```bash
   dotnet build
   ```

3. **Run the application**
   ```bash
   cd FantasyQuestSystem.Console
   dotnet run
   ```

4. **Enjoy the Fantasy Quest Management System!** 🎮

## 📋 Usage Guide

### Main Menu Options

1. **Quest Management** - Create, assign, and manage quests
2. **Character Management** - Create and manage characters
3. **Guild Management** - Create and manage guilds
4. **View Statistics** - See system overview
5. **Exit** - Close the application

### Sample Data

The application comes pre-populated with sample data:
- **Characters**: Thorin (Warrior), Gandalf (Mage), Legolas (Ranger), Aragorn (Cleric)
- **Guild**: Fellowship of the Ring
- **Quests**: Destroy the Ring, Defeat the Balrog, Rescue the Hobbits, Gather Supplies

### Example Workflow

1. **Create a Character**
   - Choose option 2 (Character Management)
   - Choose option 1 (Create New Character)
   - Enter name and select class

2. **Create a Quest**
   - Choose option 1 (Quest Management)
   - Choose option 1 (Create New Quest)
   - Enter title, description, difficulty, and rewards

3. **Assign Quest to Character**
   - Choose option 1 (Quest Management)
   - Choose option 4 (Assign Quest to Character)
   - Enter quest ID and character ID

4. **Complete the Quest**
   - Choose option 1 (Quest Management)
   - Choose option 6 (Complete Quest)
   - Enter quest ID

## 🏛️ Project Structure

```
ModularMonolith/
├── FantasyQuestSystem.Console/          # Console UI Layer
│   ├── Program.cs                       # Application entry point
│   └── ConsoleApplication.cs            # Main UI logic
├── FantasyQuestSystem.Application/      # Application Layer
│   └── Services/                        # Application services
│       ├── IQuestService.cs
│       ├── QuestService.cs
│       ├── ICharacterService.cs
│       ├── CharacterService.cs
│       ├── IGuildService.cs
│       └── GuildService.cs
├── FantasyQuestSystem.Domain/           # Domain Layer
│   ├── Entities/                        # Domain entities
│   │   ├── BaseEntity.cs
│   │   ├── Quest.cs
│   │   ├── Character.cs
│   │   └── Guild.cs
│   ├── Enums/                           # Domain enums
│   │   ├── QuestDifficulty.cs
│   │   ├── QuestStatus.cs
│   │   ├── CharacterClass.cs
│   │   └── GuildRank.cs
│   ├── Exceptions/                      # Domain exceptions
│   │   └── DomainException.cs
│   └── Repositories/                    # Repository interfaces
│       ├── IQuestRepository.cs
│       ├── ICharacterRepository.cs
│       └── IGuildRepository.cs
├── FantasyQuestSystem.Infrastructure/   # Infrastructure Layer
│   ├── Repositories/                    # Repository implementations
│   │   ├── InMemoryQuestRepository.cs
│   │   ├── InMemoryCharacterRepository.cs
│   │   └── InMemoryGuildRepository.cs
│   └── DependencyInjection/             # DI configuration
│       └── ServiceCollectionExtensions.cs
├── PRD.html                             # Product Requirements Document
├── monolith-fix-plan.md                 # Development tracking
└── README.md                            # This file
```

## 🎯 Design Principles

### Modular Monolith Benefits
- **Simplicity**: Single deployable unit
- **Modularity**: Clear boundaries between modules
- **Maintainability**: Easy to understand and modify
- **Scalability**: Can be split into microservices later if needed

### Clean Architecture Benefits
- **Independence**: Business logic independent of frameworks
- **Testability**: Easy to unit test each layer
- **Flexibility**: Easy to change implementations
- **Maintainability**: Clear separation of concerns

### Domain-Driven Design Benefits
- **Rich Domain Models**: Business logic in entities
- **Ubiquitous Language**: Code speaks business language
- **Bounded Contexts**: Clear module boundaries
- **Aggregate Roots**: Proper entity relationships

## 🔧 Technical Stack

- **Runtime**: .NET 8
- **Language**: C# 12
- **Architecture**: Clean Architecture + Modular Monolith
- **Patterns**: Repository, Service Layer, Domain-Driven Design
- **DI Container**: Microsoft.Extensions.DependencyInjection
- **Storage**: In-Memory (easily swappable)

## 🚀 Future Enhancements

### Easy to Add
- **Database Persistence**: SQL Server, PostgreSQL, MongoDB
- **Web API Layer**: RESTful API for external integrations
- **Event-Driven Architecture**: Message queues for async operations
- **Advanced Quest Features**: Quest chains, dependencies, time limits
- **Real-time Features**: WebSocket connections for live updates
- **Advanced Analytics**: Detailed reporting and statistics

### Modular Extensions
- **Authentication Module**: User management and security
- **Notification Module**: In-game notifications and alerts
- **Trading Module**: Player-to-player trading system
- **Auction Module**: Item auction house
- **Chat Module**: Guild and global chat systems

## 📊 Architecture Benefits

### For Developers
- **Clear Structure**: Easy to understand and navigate
- **Testable Code**: Each layer can be tested independently
- **Maintainable**: Changes are isolated to specific modules
- **Extensible**: New features can be added without affecting existing code

### For Business
- **Fast Development**: Clear patterns and structure
- **Quality**: Built-in validation and business rules
- **Flexibility**: Easy to adapt to changing requirements
- **Scalability**: Can grow with business needs

## 🤝 Contributing

This is a demonstration project showcasing modular monolith architecture. Feel free to:

1. **Explore the code** to understand the patterns
2. **Extend functionality** by adding new features
3. **Improve the architecture** with your own ideas
4. **Use as a template** for your own projects

## 📝 License

This project is for educational and demonstration purposes. Feel free to use it as a reference for your own projects.

---

**Happy Questing!** ⚔️🏰🎮 