namespace CalastoneTextFilter.Core.FilterPipeline.Filters;

public class ContainsCharacterFilter(char character) : IWordFilter
{
    private readonly string _character = character.ToString();

    public bool ShouldKeep(ReadOnlySpan<char> word)
    {
        return !MemoryExtensions.Contains(word, _character, StringComparison.OrdinalIgnoreCase);
    }
}
