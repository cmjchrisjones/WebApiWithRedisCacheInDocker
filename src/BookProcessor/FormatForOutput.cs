using BookProcessor.DTO;
using BookProcessor.Extensions;
using BookProcessor.Models;
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

            if (book is null)
            {
                Console.WriteLine("No book details, exiting...");
            }
            else
            {
                var formatted = new OutputDetails
                {
                    Author = FlattenAuthors(book.Authors),
                    Binding = book.Binding.FormatForOutput(),
                    Title = book.Title.FormatForOutput(),
                    PublicationDate = book.PublishDate.FormatForOutput(),
                    Publisher = book.Publisher.FormatForOutput(),
                    ISBN = book.ISBN,
                    ISBN13 = book.ISBN13
                };

                return formatted;
            }
            return null;
        }

        private static string FlattenAuthors(string[] authors)
        {
            if (authors != null && authors.Length > 0)
            {
                StringBuilder flattenedAuthors = new StringBuilder();

                foreach (var author in authors)
                {
                    flattenedAuthors.Append(author + " ");
                }
                return flattenedAuthors.ToString().Trim();
            }
            return string.Empty;
        }
    }
}