namespace CalastoneTextFilter.Core.FilterPipeline.Filters;

/// <summary>
/// An individual word filter that can be used in a filter pipeline. Each filter implements the ShouldKeep method, which determines whether a given word should be kept or filtered out based on specific criteria.
/// </summary>
public interface IWordFilter
{
    /// <summary>
    /// Pass in an individial word and return true if the word should be kept, or false if it should be filtered out.
    /// </summary>
    /// <param name="word">The word to check</param>
    /// <returns>True if the word should be kept, false if it should be filtered out.</returns>
    public bool ShouldKeep(ReadOnlySpan<char> word);
}
