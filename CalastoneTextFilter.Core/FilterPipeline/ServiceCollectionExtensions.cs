using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CalastoneTextFilter.Core.FilterPipeline;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFilterPipeline(
        this IServiceCollection services,
        Action<FilterPipelineBuilder> configure)
    {
        var filters = BuildFilters(configure);
        services.AddSingleton<IFilterPipeline>(provider =>
            new FilterPipeline(filters, provider.GetRequiredService<ILogger<FilterPipeline>>()));
        return services;
    }

    public static IServiceCollection AddFilterPipeline(
        this IServiceCollection services,
        string key,
        Action<FilterPipelineBuilder> configure)
    {
        var filters = BuildFilters(configure);
        services.AddKeyedSingleton<IFilterPipeline>(key, (provider, _) =>
            new FilterPipeline(filters, provider.GetRequiredService<ILogger<FilterPipeline>>()));
        return services;
    }

    private static Filters.IWordFilter[] BuildFilters(Action<FilterPipelineBuilder> configure)
    {
        var builder = new FilterPipelineBuilder();
        configure(builder);
        return builder.Build();
    }
}
