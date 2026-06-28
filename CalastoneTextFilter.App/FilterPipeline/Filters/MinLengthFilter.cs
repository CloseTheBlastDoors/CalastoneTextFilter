namespace CalastoneTextFilter.App.FilterPipeline.Filters;

public class MinLengthFilter(int length) : IWordFilter
{
    public bool ShouldKeep(ReadOnlySpan<char> word)
    {
        return word.Length >= length;
    }
}

