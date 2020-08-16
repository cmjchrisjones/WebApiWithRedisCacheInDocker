using Xunit;
using BookProcessor.Extensions;
using FluentAssertions;

namespace BookProcessorTests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("123-456-7890", "1234567890")]
        [InlineData("123-4567890", "1234567890")]
        [InlineData("1234567890", "1234567890")]
        [InlineData("123-456-789-0", "1234567890")]
        public void FormattingAnISBNWillRemoveAllHyphens(string exampleInput, string expectedOutput)
        {
            // Arrange

            // Act
            var result = exampleInput.RemoveHyphensFromISBN();

            // Assert
            result.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData("Some text without comma", "Some text without comma")]
        [InlineData("Some text, with a comma", "Some text with a comma")]
        [InlineData("Some text, with, multiple, commas with spaces after them", "Some text with multiple commas with spaces after them")]
        [InlineData("Some text,with,commas,with no spaces", "Some text with commas with no spaces")]
        [InlineData("Some Poorly formatted , , title with 2 adjacent commas", "Some Poorly formatted title with 2 adjacent commas")]
        public void ReplacingCommasWithWhiteSpace(string input, string expectedOutput)
        {
            // Arrange

            // Act
            var result = input.FormatForOutput();

            // Assert
            result.Should().Be(expectedOutput);
        }
    }
}
