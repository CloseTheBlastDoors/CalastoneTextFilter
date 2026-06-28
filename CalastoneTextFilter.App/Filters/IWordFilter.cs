namespace CalastoneTextFilter.App.Filters;

public interface IWordFilter
{
    public bool ShouldKeep(ReadOnlySpan<char> word);
}

