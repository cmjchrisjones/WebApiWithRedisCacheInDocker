using BookProcessor;
using BookProcessor.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookProcessorTests
{
    public class ProcessBooksTests
    {
        [Fact]
        public void WhenASingleBookIsNotFoundInTheCacheAndASuccessfulResponseFromThirdPartyIsReceivedThenBookInformationIsSavedIntoTheRedisCache()
        {
            // Arrange
            var processBooks = new Mock<IProcessBooks>();
            processBooks.Setup(_ => _.BySingleISBNAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new OutputDetails());

            // Act

            // Assert
        }
    }
}
