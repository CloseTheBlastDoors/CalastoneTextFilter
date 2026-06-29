using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CalastoneTextFilter.Core.FilterPipeline;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a word filter pipeline to the service collection. The pipeline is built using the provided configuration action.
    /// Use this method when you want to register a single pipeline with a specific configuration.
    /// (Same as AddStandardFilterPipeline() - included for backwards compatibity)
    /// </summary>
    /// <param name="services">The service collection to add the pipeline to.</param>
    /// <param name="configure">The configuration action to build the pipeline.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddFilterPipeline(
        this IServiceCollection services,
        Action<FilterPipelineBuilder> configure)
    {
        return AddStandardFilterPipeline(services, configure);
    }

    /// <summary>
    /// Adds a word filter pipeline to the service collection which includes a keyed registration. The pipeline is built using the provided configuration action.
    /// Use this method when you want to register multiple pipelines with different configurations and retrieve them by key.
    /// (Same as AddStandardFilterPipeline() - included for backwards compatibity)
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
        return AddStandardFilterPipeline(services, key, configure);
    }

    /// <summary>
    /// Adds a word filter pipeline to the service collection. The pipeline is built using the provided configuration action.
    /// Use this method when you want to register a single pipeline with a specific configuration.
    /// </summary>
    /// <param name="services">The service collection to add the pipeline to.</param>
    /// <param name="configure">The configuration action to build the pipeline.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddStandardFilterPipeline(
        this IServiceCollection services,
        Action<FilterPipelineBuilder> configure)
    {
        var filters = BuildFilters(configure);
        services.AddSingleton<IFilterPipeline>(provider =>
            new StandardFilterPipeline(filters, provider.GetRequiredService<ILogger<StandardFilterPipeline>>()));
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
    public static IServiceCollection AddStandardFilterPipeline(
        this IServiceCollection services,
        string key,
        Action<FilterPipelineBuilder> configure)
    {
        var filters = BuildFilters(configure);
        services.AddKeyedSingleton<IFilterPipeline>(key, (provider, _) =>
            new StandardFilterPipeline(filters, provider.GetRequiredService<ILogger<StandardFilterPipeline>>()));
        return services;
    }

    /// <summary>
    /// Adds a high-throughput word filter pipeline to the service collection. The pipeline is built
    /// using the provided configuration action. Prefer this over <see cref="AddFilterPipeline(IServiceCollection, Action{FilterPipelineBuilder})"/>
    /// when processing large volumes of text, as it avoids per-word heap allocations by walking the
    /// input as a <see cref="ReadOnlySpan{T}"/> and using stack-allocated buffers for word processing.
    /// </summary>
    /// <param name="services">The service collection to add the pipeline to.</param>
    /// <param name="configure">The configuration action to build the pipeline.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddEfficientFilterPipeline(
        this IServiceCollection services,
        Action<FilterPipelineBuilder> configure)
    {
        var filters = BuildFilters(configure);
        services.AddSingleton<IFilterPipeline>(provider =>
            new EfficientFilterPipeline(filters, provider.GetRequiredService<ILogger<EfficientFilterPipeline>>()));
        return services;
    }

    /// <summary>
    /// Adds an efficient word filter pipeline to the service collection which includes a keyed registration. The pipeline is built using the provided configuration action.
    /// Use this method when you want to register multiple pipelines with different configurations and retrieve them by key.
    /// </summary>
    /// <param name="services">The service collection to add the pipeline to.</param>
    /// <param name="key">The key to register the pipeline under.</param>
    /// <param name="configure">The configuration action to build the pipeline.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddEfficientFilterPipeline(
        this IServiceCollection services,
        string key,
        Action<FilterPipelineBuilder> configure)
    {
        var filters = BuildFilters(configure);
        services.AddKeyedSingleton<IFilterPipeline>(key, (provider, _) =>
            new EfficientFilterPipeline(filters, provider.GetRequiredService<ILogger<EfficientFilterPipeline>>()));
        return services;
    }

    private static Filters.IWordFilter[] BuildFilters(Action<FilterPipelineBuilder> configure)
    {
        var builder = new FilterPipelineBuilder();
        configure(builder);
        return builder.Build();
    }
}
