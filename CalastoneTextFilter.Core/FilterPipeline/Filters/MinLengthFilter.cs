namespace CalastoneTextFilter.Core.FilterPipeline.Filters;

/// <summary>
/// A word filter implementation that filters out words that are shorter than a specified minimum length. The minimum length is provided during the construction of the filter.
/// </summary>
/// <param name="length">The minimum length a word must have to be kept.</param>
public class MinLengthFilter(int length) : IWordFilter
{
    /// <summary>
    /// Check if the word meets minimum length requirement. If the word is shorter than the specified length, it is filtered out (returns false). If it meets or exceeds the length, it is kept (returns true).
    /// </summary>
    /// <param name="word">The word to check</param>
    /// <returns>True if the word meets the minimum length requirement, false otherwise</returns>
    public bool ShouldKeep(ReadOnlySpan<char> word)
    {
        return word.Length >= length;
    }
}
