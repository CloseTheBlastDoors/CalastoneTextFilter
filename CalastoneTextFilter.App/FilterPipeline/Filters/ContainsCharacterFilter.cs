namespace CalastoneTextFilter.App.FilterPipeline.Filters;

public class ContainsCharacterFilter(char character) : IWordFilter
{
    public bool ShouldKeep(ReadOnlySpan<char> word)
    {
        return !word.Contains(character.ToString(), StringComparison.OrdinalIgnoreCase);
    }
}

