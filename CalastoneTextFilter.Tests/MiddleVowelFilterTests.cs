using CalastoneTextFilter.App.FilterPipeline.Filters;

namespace CalastoneTextFilter.Tests;

public class MiddleVowelFilterTests
{
    private readonly MiddleVowelFilter _filter = new();

    [Theory]
    [InlineData("Alice")]
    [InlineData("a")]
    [InlineData("get")]
    [InlineData("clean")]
    [InlineData("currently")]
    public void ShouldKeep_ReturnsFalse_WhenOddLengthCentreIsVowel(string word)
    {
        Assert.False(_filter.ShouldKeep(word));
    }

    [Theory]
    [InlineData("the")]
    [InlineData("fry")]
    [InlineData("river")]
    [InlineData("seven")]
    public void ShouldKeep_ReturnsTrue_WhenOddLengthCentreIsNotVowel(string word)
    {
        Assert.True(_filter.ShouldKeep(word));
    }

    [Theory]
    [InlineData("aeon")]    // length 4, centre = 'e','o' — both vowels
    [InlineData("what")]    // length 4, centre = 'h','a' — 'a' is a vowel
    [InlineData("very")]    // length 4, centre = 'e','r' — 'e' is a vowel
    public void ShouldKeep_ReturnsFalse_WhenEvenLengthEitherCentreCharIsVowel(string word)
    {
        Assert.False(_filter.ShouldKeep(word));
    }

    [Theory]
    [InlineData("rather")]  // length 6, centre = 't','h' — neither is a vowel
    [InlineData("rhythm")]  // length 6, centre = 'y','t' — neither is a vowel
    public void ShouldKeep_ReturnsTrue_WhenEvenLengthNoCentreCharIsVowel(string word)
    {
        Assert.True(_filter.ShouldKeep(word));
    }

    [Fact]
    public void ShouldKeep_ReturnsTrue_ForEmptyString()
    {
        Assert.True(_filter.ShouldKeep(string.Empty));
    }
}

