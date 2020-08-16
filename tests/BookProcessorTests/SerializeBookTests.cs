using BookProcessor;
using BookProcessor.Models;
using FluentAssertions;
using Xunit;

namespace BookProcessorTests
{
    public class SerializeBookTests
    {
        [Fact]
        public void ResultGetsSerializedProperty()
        {
            // Arrange
            var mockResponse = "{\"book\":{\"publisher\":\"Arnold / Oxford University Press\",\"synopsys\":\"A Marvel Of Compression And Balance.\",\"language\":\"En\",\"format\":\"print\",\"overview\":\" <br>\\nBy the twentieth century Ireland had a history very different from that of most other western European countries. The two most profound shocks of the century, the world wars, affected Ireland indirectly, while partition and civil war were embittering first-hand experiences whose legacies continue to shape the course of events. This masterful new history is the first account to cover the whole of Ireland, north and south, from the origins of Sinn Fein at the beginning of the century to the Stormont agreement at the end. Townshend provides a concise, comprehensive, and balanced survey of the key points in Ireland's history.\",\"image\":\"https://images.isbndb.com/covers/33/56/9780340663356.jpg\",\"title_long\":\"Ireland: The 20th (twentieth) Century\",\"edition\":\"1\",\"dimensions\":\"xiii, 281 p. : ill., maps ; 25 cm.\",\"pages\":281,\"date_published\":1999,\"subjects\":[\"History\",\"DA959 .T69 1999\",\"941.5082\"],\"authors\":[\"Charles Townshend\"],\"title\":\"Ireland: The 20th (twentieth) Century\",\"isbn13\":\"9780340663356\",\"msrp\":0.37,\"binding\":\"Hardcover\",\"publish_date\":1999,\"isbn\":\"0340663359\"}}";

            var expectedResponse = new Rootobject
            {
                book = new Book
                {
                    Publisher = "Arnold / Oxford University Press",
                    Language = "En",
                    //overview = " <br>\nBy the twentieth century Ireland had a history very different from that of most other western European countries. The two most profound shocks of the century, the world wars, affected Ireland indirectly, while partition and civil war were embittering first-hand experiences whose legacies continue to shape the course of events. This masterful new history is the first account to cover the whole of Ireland, north and south, from the origins of Sinn Fein at the beginning of the century to the Stormont agreement at the end. Townshend provides a concise, comprehensive, and balanced survey of the key points in Ireland's history.",
                    //format = "print",
                    //synopsys = "A Marvel Of Compression And Balance.",
                    Image = "https://images.isbndb.com/covers/33/56/9780340663356.jpg",
                    TitleLong = "Ireland: The 20th (twentieth) Century",
                    //edition = "1",
                    Dimensions = "xiii, 281 p. : ill., maps ; 25 cm.",
                    Pages = 281,
                    DatePublished = "1999",
                    //subjects = new string[] { "History", "DA959 .T69 1999", "941.5082" },
                    Authors = new string[] { "Charles Townshend" },
                    Title = "Ireland: The 20th (twentieth) Century",
                    ISBN13 = "9780340663356",
                    MSRP = "0.37",
                    Binding = "Hardcover",
                    PublishDate = "1999",
                    ISBN = "0340663359"
                }
            };

            // Act
            var result = SerializeBook.Serialize(mockResponse); 

            // Assert
            result.Should().BeEquivalentTo(expectedResponse.book);
        }
    }
}
