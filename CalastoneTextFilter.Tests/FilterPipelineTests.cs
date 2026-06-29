using CalastoneTextFilter.Core.FilterPipeline;
using CalastoneTextFilter.Core.FilterPipeline.Filters;
using Microsoft.Extensions.Logging.Abstractions;

namespace CalastoneTextFilter.Tests;

public class FilterPipelineTests
{
    const int MIN_LENGTH = 3;
    const char CHARACTER_TO_FILTER = 't';

    private static IFilterPipeline BuildPipeline(bool efficient)
    {
        IWordFilter[] filters =
        [
            new MiddleVowelFilter(),
            new MinLengthFilter(MIN_LENGTH),
            new ContainsCharacterFilter(CHARACTER_TO_FILTER),
        ];

        return efficient
            ? new EfficientFilterPipeline(filters, NullLogger<EfficientFilterPipeline>.Instance)
            : new StandardFilterPipeline(filters, NullLogger<StandardFilterPipeline>.Instance);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Apply_RemovesWordsThatFailAnyFilter(bool efficient)
    {
        var pipeline = BuildPipeline(efficient);
        // "clean" fails Filter1 (odd-5, middle 'e' is vowel)
        // "to"    fails Filter2 (length 2)
        // "the"   fails Filter3 (contains 't')
        // "seven" passes all three filters (odd-5, middle 'v'; length 5; no 't')
        string result = pipeline.Apply("clean to the seven");
        Assert.Equal("seven", result);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Apply_KeepsWordsPassingAllFilters(bool efficient)
    {
        var pipeline = BuildPipeline(efficient);
        // "seven": odd-5, middle index 2 = 'v' — not a vowel; length 5; no 't'
        // "nymph": odd-5, middle index 2 = 'm' — not a vowel; length 5; no 't'
        string result = pipeline.Apply("seven nymph");
        Assert.Equal("seven nymph", result);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Apply_WithPunctuation(bool efficient)
    {
        var pipeline = BuildPipeline(efficient);
        string result = pipeline.Apply("clean; to - the# seven, nymph.");
        Assert.Equal("seven, nymph.", result);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Apply_EmptyInput_ReturnsEmpty(bool efficient)
    {
        var pipeline = BuildPipeline(efficient);
        Assert.Equal(string.Empty, pipeline.Apply(string.Empty));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Apply_AllWordsFiltered_ReturnsEmpty(bool efficient)
    {
        var pipeline = BuildPipeline(efficient);
        string result = pipeline.Apply("the to a");
        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Apply_HandlesExtraWhitespace(bool efficient)
    {
        var pipeline = BuildPipeline(efficient);
        string result = pipeline.Apply("seven  nymph");
        Assert.Equal("seven nymph", result);
    }
}
