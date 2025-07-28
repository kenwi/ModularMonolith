using FantasyQuestSystem.Application.Services;
using FantasyQuestSystem.Domain.Repositories;
using FantasyQuestSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyQuestSystem.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFantasyQuestSystem(this IServiceCollection services)
    {
        // Register repositories
        services.AddScoped<IQuestRepository, InMemoryQuestRepository>();
        services.AddScoped<ICharacterRepository, InMemoryCharacterRepository>();
        services.AddScoped<IGuildRepository, InMemoryGuildRepository>();

        // Register application services
        services.AddScoped<IQuestService, QuestService>();
        services.AddScoped<ICharacterService, CharacterService>();
        services.AddScoped<IGuildService, GuildService>();

        return services;
    }
} 