using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CalastoneTextFilter.Core.FilterPipeline;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a word filter pipeline to the service collection. The pipeline is built using the provided configuration action.
    /// Use this method when you want to register a single pipeline with a specific configuration.
    /// </summary>
    /// <param name="services">The service collection to add the pipeline to.</param>
    /// <param name="configure">The configuration action to build the pipeline.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddFilterPipeline(
        this IServiceCollection services,
        Action<FilterPipelineBuilder> configure)
    {
        var filters = BuildFilters(configure);
        services.AddSingleton<IFilterPipeline>(provider =>
            new FilterPipeline(filters, provider.GetRequiredService<ILogger<FilterPipeline>>()));
        return services;
    }

    /// <summary>
    /// Adds a word filter pipeline to the service collection which includes a keyed registration. The pipeline is built using the provided configuration action.
    /// Use this method when you want to register multiple pipelines with different configurations and retrieve them by key.
    /// </summary>
    /// <param name="services">The service collection to add the pipeline to.</param>
    /// <param name="key">The key to register the pipeline under.</param>
    /// <param name="configure">The configuration action to build the pipeline.</param>
    /// <returns>The updated service collection.</returns>
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
