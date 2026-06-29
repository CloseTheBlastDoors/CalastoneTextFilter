using CalastoneTextFilter.Core.FilterPipeline.Filters;

namespace CalastoneTextFilter.Tests;

public class ContainsCharacterFilterTests
{
    const char CHARACTER_TO_FILTER = 't';
    private readonly ContainsCharacterFilter _filter = new(CHARACTER_TO_FILTER);

    [Theory]
    [InlineData("the")]
    [InlineData("feet")]
    [InlineData("fortunately")]
    [InlineData("to")]
    [InlineData("Testing")]
    public void ShouldKeep_ReturnsFalse_WhenWordContainsLowercaseOrUppercaseT(string word)
    {
        Assert.False(_filter.ShouldKeep(word));
    }

    [Theory]
    [InlineData("Alice")]
    [InlineData("beginning")]
    [InlineData("her")]
    [InlineData("bank")]
    public void ShouldKeep_ReturnsTrue_WhenWordDoesNotContainT(string word)
    {
        Assert.True(_filter.ShouldKeep(word));
    }

    [Fact]
    public void ShouldKeep_ReturnsTrue_ForEmptyString()
    {
        Assert.True(_filter.ShouldKeep(string.Empty));
    }
}

