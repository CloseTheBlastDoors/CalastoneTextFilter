namespace CalastoneTextFilter.App.FilterPipeline.Filters;

public interface IWordFilter
{
    public bool ShouldKeep(ReadOnlySpan<char> word);
}

