using CalastoneTextFilter.App.FilterPipeline.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CalastoneTextFilter.App.FilterPipeline;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFilterPipeline(this IServiceCollection services)
    {
        services.AddSingleton<IFilterPipeline, FilterPipeline>();
        return services;
    }

    public static IServiceCollection AddMinLengthFilter(this IServiceCollection services, int length)
    {
        return services.AddSingleton<IWordFilter, MinLengthFilter>(provider => new MinLengthFilter(length));
    }

    public static IServiceCollection AddCharacterFilter(this IServiceCollection services, char character)
    {
        return services.AddSingleton<IWordFilter, ContainsCharacterFilter>(provider => new ContainsCharacterFilter(character));
    }

    public static IServiceCollection AddMiddleVowelFilter(this IServiceCollection services)
    {
        return services.AddSingleton<IWordFilter, MiddleVowelFilter>();
    }
}
