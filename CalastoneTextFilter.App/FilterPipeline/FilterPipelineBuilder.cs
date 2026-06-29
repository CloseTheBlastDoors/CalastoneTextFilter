using CalastoneTextFilter.App.FilterPipeline.Filters;

namespace CalastoneTextFilter.App.FilterPipeline;

public class FilterPipelineBuilder
{
    private readonly List<IWordFilter> _filters = [];

    public FilterPipelineBuilder AddMiddleVowelFilter()
    {
        _filters.Add(new MiddleVowelFilter());
        return this;
    }

    public FilterPipelineBuilder AddMinLengthFilter(int length)
    {
        _filters.Add(new MinLengthFilter(length));
        return this;
    }

    public FilterPipelineBuilder AddCharacterFilter(char character)
    {
        _filters.Add(new ContainsCharacterFilter(character));
        return this;
    }

    internal IWordFilter[] Build() => [.. _filters];
}
