namespace CalastoneTextFilter.Core.FilterPipeline.Filters;

public interface IWordFilter
{
    public bool ShouldKeep(ReadOnlySpan<char> word);
}
