using CalastoneTextFilter.App.FilterPipeline.Filters;

namespace CalastoneTextFilter.Tests
{
    public class MinLengthFilterTests
    {
        const int MIN_LENGTH = 3;
        private readonly MinLengthFilter _filter = new(MIN_LENGTH);

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("to")]
        public void ShouldKeep_ReturnsFalse_WhenLengthLessThan3(string word)
        {
            Assert.False(_filter.ShouldKeep(word));
        }

        [Theory]
        [InlineData("they")]
        [InlineData("word")]
        [InlineData("hello")]
        [InlineData("beginning")]
        public void ShouldKeep_ReturnsTrue_WhenLengthMoreThen3(string word)
        {
            Assert.True(_filter.ShouldKeep(word));
        }

        [Fact]
        public void ShouldKeep_ExactlyLength3_ReturnsTrue()
        {
            Assert.True(_filter.ShouldKeep("abc"));
        }
    }
}
