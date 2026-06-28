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
        return services.AddTransient<IWordFilter, MinLengthFilter>(provider => new MinLengthFilter(length));
    }

    public static IServiceCollection AddCharacterFilter(this IServiceCollection services, char character)
    {
        return services.AddTransient<IWordFilter, ContainsCharacterFilter>(provider => new ContainsCharacterFilter(character));
    }

    public static IServiceCollection AddMiddleVowelFilter(this IServiceCollection services)
    {
        return services.AddTransient<IWordFilter, MiddleVowelFilter>();
    }
}
