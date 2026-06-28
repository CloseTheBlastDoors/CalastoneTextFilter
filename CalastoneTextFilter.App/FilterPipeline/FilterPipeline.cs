using CalastoneTextFilter.App.FilterPipeline.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

public interface IFilterPipeline
{
    string Apply(string input);
}

public class FilterPipeline(IEnumerable<IWordFilter> filters, ILogger<FilterPipeline> logger) : IFilterPipeline
{
    private readonly IWordFilter[] _filters = [.. filters];
    private readonly ILogger<FilterPipeline> _logger = logger;

    public string Apply(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            _logger.LogWarning("Input string is null or whitespace. Returning empty string.");
            return string.Empty;
        }

        var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var keptWords = words.Where(word => _filters.All(filter => filter.ShouldKeep(StripNonLetterOrDigit(word))));
        return string.Join(" ", keptWords);
    }

    private static string StripNonLetterOrDigit(string word)
    {
        string strippedWord = new([.. word.Where(char.IsLetterOrDigit)]);
        return strippedWord;
    }
}

