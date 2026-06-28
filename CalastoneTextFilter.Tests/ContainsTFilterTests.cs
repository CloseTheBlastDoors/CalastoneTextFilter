using CalastoneTextFilter.App.Filters;

namespace CalastoneTextFilter.Tests
{
    public class ContainsTFilterTests
    {
        private readonly ContainsTFilter _filter = new();

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
}
