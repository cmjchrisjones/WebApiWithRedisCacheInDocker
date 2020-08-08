using BookProcessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookProcessor
{
    public static class Format
    {
        public static OutputDetails ForOutputList(Book book)
        {
            Console.WriteLine("Serializing book info");
            StringBuilder flattenedAuthors = new StringBuilder();
            if (book is null)
            {
                Console.WriteLine("No book details, exiting...");
            }
            else
            {
                foreach (var author in book.authors)
                {
                    flattenedAuthors.Append(author.Replace(',',' '));
                    flattenedAuthors.Append(" ");
                }
                flattenedAuthors.ToString().TrimEnd(',');
                var formatted = new OutputDetails
                {
                    Author = flattenedAuthors.ToString(),
                    Binding = book.binding?.Replace(',', '-'),
                    Title = book.title?.Replace(',', '-'),
                    PublicationDate = book.publish_date?.Replace(',', '-'),
                    Publisher = book.publisher?.Replace(',', '-'),
                    ISBN = book.isbn,
                    ISBN13 = book.isbn13
                };

                return formatted;
            }
            return null;
        }
    }
}