using CalastoneTextFilter.App.FilterPipeline;
using CalastoneTextFilter.App.FilterPipeline.Filters;

namespace CalastoneTextFilter.Tests;

public class FilterMiddleVowelTests
{
    const int MIN_LENGTH = 3;
    const char CHARACTER_TO_FILTER = 't';

    private static FilterPipeline BuildPipeline()
    {
        return new(
    [
        new MiddleVowelFilter(),
        new MinLengthFilter(MIN_LENGTH),
        new ContainsCharacterFilter(CHARACTER_TO_FILTER),
    ]);
    }

    [Fact]
    public void Apply_RemovesWordsThatFailAnyFilter()
    {
        var pipeline = BuildPipeline();
        // "clean" fails Filter1 (odd-5, middle 'e' is vowel)
        // "to"    fails Filter2 (length 2)
        // "the"   fails Filter3 (contains 't')
        // "seven" passes all three filters (odd-5, middle 'v'; length 5; no 't')
        string result = pipeline.Apply("clean to the seven");
        Assert.Equal("seven", result);
    }

    [Fact]
    public void Apply_KeepsWordsPassingAllFilters()
    {
        var pipeline = BuildPipeline();
        // "seven": odd-5, middle index 2 = 'v' — not a vowel; length 5; no 't'
        // "nymph": odd-5, middle index 2 = 'm' — not a vowel; length 5; no 't'
        string result = pipeline.Apply("seven nymph");
        Assert.Equal("seven nymph", result);
    }

    [Fact]
    public void Apply_WithPunctuation()
    {
        var pipeline = BuildPipeline();
        string result = pipeline.Apply("clean; to - the# seven, nymph.");
        Assert.Equal("seven, nymph.", result);
    }

    [Fact]
    public void Apply_EmptyInput_ReturnsEmpty()
    {
        var pipeline = BuildPipeline();
        Assert.Equal(string.Empty, pipeline.Apply(string.Empty));
    }

    [Fact]
    public void Apply_AllWordsFiltered_ReturnsEmpty()
    {
        var pipeline = BuildPipeline();
        string result = pipeline.Apply("the to a");
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Apply_HandlesExtraWhitespace()
    {
        var pipeline = BuildPipeline();
        string result = pipeline.Apply("seven  nymph");
        Assert.Equal("seven nymph", result);
    }
}

