using CalastoneTextFilter.Core.FilterPipeline.Filters;

namespace CalastoneTextFilter.Tests;

public class ContainsCharacterFilterTests
{
    private readonly ContainsCharacterFilter _filterStandardChar = new('t');
    private readonly ContainsCharacterFilter _filterAlternateChar = new('f');

    [Theory]
    [InlineData("the")]
    [InlineData("feet")]
    [InlineData("fortunately")]
    [InlineData("to")]
    [InlineData("Testing")]
    public void ShouldKeep_ReturnsFalse_WhenWordContainsLowercaseOrUppercaseT(string word)
    {
        Assert.False(_filterStandardChar.ShouldKeep(word));
    }

    [Theory]
    [InlineData("Alice")]
    [InlineData("beginning")]
    [InlineData("her")]
    [InlineData("bank")]
    public void ShouldKeep_ReturnsTrue_WhenWordDoesNotContainT(string word)
    {
        Assert.True(_filterStandardChar.ShouldKeep(word));
    }

    [Fact]
    public void ShouldKeep_ReturnsTrue_ForEmptyString()
    {
        Assert.True(_filterStandardChar.ShouldKeep(string.Empty));
    }

    [Theory]
    [InlineData("feet")]
    [InlineData("fortunately")]
    public void ShouldKeep_ReturnsFalse_WhenWordContainsLowercaseOrUppercaseF(string word)
    {
        Assert.False(_filterAlternateChar.ShouldKeep(word));
    }

    [Theory]
    [InlineData("the")]
    [InlineData("to")]
    [InlineData("Testing")]
    public void ShouldKeep_ReturnsTrue_WhenWordDoesNotContainF(string word)
    {
        Assert.True(_filterAlternateChar.ShouldKeep(word));
    }
}

