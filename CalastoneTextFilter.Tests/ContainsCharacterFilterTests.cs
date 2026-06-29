using CalastoneTextFilter.Core.FilterPipeline.Filters;

namespace CalastoneTextFilter.Tests;

public class ContainsCharacterFilterTests
{
    [Theory]
    //Test 't' char
    [InlineData('t', "the")]
    [InlineData('t', "feet")]
    [InlineData('t', "fortunately")]
    [InlineData('t', "to")]
    [InlineData('t', "Testing")]

    //Test 'f' char
    [InlineData('f', "fool")]
    [InlineData('f', "flume")]
    [InlineData('f', "Farmer")]
    public void ShouldKeep_ReturnsFalse_WhenWordContainsLowercaseOrUppercaseFilterChar(char filterChar, string word)
    {
        var charFilter = new ContainsCharacterFilter(filterChar);
        Assert.False(charFilter.ShouldKeep(word));
    }

    [Theory]
    //Test 't' char
    [InlineData('t', "Alice")]
    [InlineData('t', "Beginning")]
    [InlineData('t', "her")]
    [InlineData('t', "bank")]
    [InlineData('t', "fool")]

    //Test 'f' char
    [InlineData('f', "Testing")]
    [InlineData('f', "the")]
    [InlineData('f', "Alice")]
    [InlineData('f', "her")]
    public void ShouldKeep_ReturnsTrue_WhenWordDoesNotContainFilterChar(char filterChar, string word)
    {
        var charFilter = new ContainsCharacterFilter(filterChar);
        Assert.True(charFilter.ShouldKeep(word));
    }

    [Fact]
    public void ShouldKeep_ReturnsTrue_ForEmptyString()
    {
        var charFilter = new ContainsCharacterFilter('t');
        Assert.True(charFilter.ShouldKeep(string.Empty));
    }
}

