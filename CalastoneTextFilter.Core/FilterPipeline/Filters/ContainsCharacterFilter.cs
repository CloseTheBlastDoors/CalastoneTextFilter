namespace CalastoneTextFilter.Core.FilterPipeline.Filters;

/// <summary>
/// A word filter implementation that filters out words containing a specific character. The character to filter by is provided during the construction of the filter.
/// </summary>
/// <param name="character">The character to filter by. If words contain this character, they will be filtered out.</param>
public class ContainsCharacterFilter(char character) : IWordFilter
{
    private readonly string _character = character.ToString();

    /// <summary>
    /// Checks if the word contains the specified character.
    /// </summary>
    /// <param name="word">The word to check</param>
    /// <returns>True if the word doesn't contain the specified character, false if it does and should be filtered out.</returns>
    public bool ShouldKeep(ReadOnlySpan<char> word)
    {
        return !MemoryExtensions.Contains(word, _character, StringComparison.OrdinalIgnoreCase);
    }
}
