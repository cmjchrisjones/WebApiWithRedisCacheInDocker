using BookProcessor;
using BookProcessor.Models;
using FluentAssertions;
using Xunit;

namespace BookProcessorTests
{
    public class FormatForOutputTests
    {
        [Theory]
        [InlineData(new string[] { "James" }, "James")]
        [InlineData(new string[] { "James", "Patterson" }, "James Patterson")]
        [InlineData(new string[] { "James Patterson" }, "James Patterson")]
        [InlineData(new string[] { "James", "Patterson", "Maxine", "Peacock" }, "James Patterson Maxine Peacock")]
        [InlineData(new string[] { "James Patterson", "Maxine Peacock" }, "James Patterson Maxine Peacock")]
        public void WhenAnArrayOfAuthorsIsReturnedTheyGetFlattenedIntoOneString(string[] authors, string expectedResult)
        {
            // Arrange
            var book = new Book { Authors = authors };

            // Act
            var result = Format.ForOutputList(book);

            // Assert
            result.Author.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void WhenAuthorsArrayContainsNoAuthorsAnEmptyStringIsReturned()
        {
            // Arrange
            var book = new Book { Authors = new string[] { } };

            // Act
            var result = Format.ForOutputList(book);

            // Assert
            result.Author.Should().BeEmpty();
        }


        [Fact]
        public void WhenAuthorsArrayIsNullAnEmptyStringIsReturned()
        {
            // Arrange
            var book = new Book { Authors = null };

            // Act
            var result = Format.ForOutputList(book);

            // Assert
            result.Author.Should().BeEmpty();
        }

        [Fact]
        public void WhenBookDetailsIsNullThenNullIsReturned()
        {
            // Arrange

            // Act
            var result = Format.ForOutputList(null);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [InlineData("Binding", "Binding")]
        [InlineData("Hardcover", "Hardcover")]
        [InlineData("Paperback", "Paperback")]
        [InlineData("Print On Demand(paperback)", "Print On Demand(paperback)")]
        [InlineData("Audio Cd", "Audio Cd")]
        [InlineData("Spiral-bound", "Spiral-bound")]
        [InlineData("Mass Market Paperback", "Mass Market Paperback")]
        [InlineData("Binding,", "Binding")]
        [InlineData("Hardcover,", "Hardcover")]
        [InlineData("Paperback,", "Paperback")]
        [InlineData("Print On Demand(paperback),", "Print On Demand(paperback)")]
        [InlineData("Audio Cd,", "Audio Cd")]
        [InlineData("Spiral-bound,", "Spiral-bound")]
        [InlineData("Mass Market Paperback,", "Mass Market Paperback")]
        [InlineData("Comma,Separated,Value", "Comma Separated Value")]
        [InlineData(null, "")]

        public void BindingsAreFormattedForOutputAccordingly(string binding, string expected)
        {
            // Arrange
            var bookInfo = new Book { Binding = binding };

            // Act
            var result = Format.ForOutputList(bookInfo);

            // Assert
            result.Binding.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("Girl, Woman, Other", "Girl Woman Other")]
        [InlineData("Son of Escobar: First Born", "Son of Escobar: First Born")]
        [InlineData("Knee Deep in Life: Wife, Mother, Realist… and why we’re already enough", "Knee Deep in Life: Wife Mother Realist… and why we’re already enough")]
        public void TitlesAreFormattedForOutputAccordingly(string title, string expected)
        {
            // Arrange
            var bookInfo = new Book { Title = title };

            // Act
            var result = Format.ForOutputList(bookInfo);

            // Assert
            result.Title.Should().BeEquivalentTo(expected);
        }
    }
}