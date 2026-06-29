using CalastoneTextFilter.Core.FilterPipeline.Filters;

namespace CalastoneTextFilter.Tests
{
    public class MinLengthFilterTests
    {
        [Theory]
        //Min length: 3
        [InlineData(3, "")]
        [InlineData(3, "a")]
        [InlineData(3, "to")]

        //Min length: 5
        [InlineData(5, "")]
        [InlineData(5, "a")]
        [InlineData(5, "to")]
        [InlineData(5, "they")]
        [InlineData(5, "word")]
        public void ShouldKeep_ReturnsFalse_WhenLengthLessThanLimit(int minLength, string word)
        {
            var filter = new MinLengthFilter(minLength);
            Assert.False(filter.ShouldKeep(word));
        }

        [Theory]
        //Min length: 3
        [InlineData(3, "they")]
        [InlineData(3, "word")]
        [InlineData(3, "hello")]
        [InlineData(3, "beginning")]

        //Min length: 5
        [InlineData(5, "Farmer")]
        [InlineData(5, "beginning")]
        public void ShouldKeep_ReturnsTrue_WhenLengthMoreThanLimit(int minLength, string word)
        {
            var filter = new MinLengthFilter(minLength);
            Assert.True(filter.ShouldKeep(word));
        }

        [Theory]
        [InlineData(3, "abc")]
        [InlineData(5, "Alice")]
        public void ShouldKeep_ReturnsTrue_WhenLengthExactlyMatches(int minLength, string word)
        {
            var filter = new MinLengthFilter(minLength);
            Assert.True(filter.ShouldKeep(word));
        }
    }
}