using CalastoneTextFilter.Core.FilterPipeline.Filters;

namespace CalastoneTextFilter.Core.FilterPipeline;

/// <summary>
/// Utility class to build a filter pipeline by adding various filters. It allows chaining of filter additions and finally builds an array of filters that can be used in a filtering process.
/// </summary>
public class FilterPipelineBuilder
{
    private readonly List<IWordFilter> _filters = [];

    /// <summary>
    /// Adds a filter that checks if a word has a vowel in the middle position. This filter will be applied to the words in the filtering process.
    /// </summary>
    /// <returns>this instance - so that further filters can be chained.</returns>
    public FilterPipelineBuilder AddMiddleVowelFilter()
    {
        _filters.Add(new MiddleVowelFilter());
        return this;
    }

    /// <summary>
    /// Adds a filter that checks if a word meets a minimum length requirement. This filter will be applied to the words in the filtering process.
    /// </summary>
    /// <param name="length">The minimum length a word must have to be kept. (i.e. not filtered out)</param>
    /// <returns>this instance - so that further filters can be chained.</returns>
    public FilterPipelineBuilder AddMinLengthFilter(int length)
    {
        _filters.Add(new MinLengthFilter(length));
        return this;
    }

    /// <summary>
    /// Adds a filter that checks if a word contains a specific character. This filter will be applied to the words in the filtering process.
    /// </summary>
    /// <param name="character">The character to check for. If the word contains this character, it will be kept.</param>
    /// <returns>this instance - so that further filters can be chained.</returns>
    public FilterPipelineBuilder AddCharacterFilter(char character)
    {
        _filters.Add(new ContainsCharacterFilter(character));
        return this;
    }

    internal IWordFilter[] Build() => [.. _filters];
}
