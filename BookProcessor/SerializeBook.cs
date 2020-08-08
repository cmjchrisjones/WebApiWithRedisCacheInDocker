using BookProcessor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BookProcessor
{
    public static class SerializeBook
    {
        public static Book Serialize(string bookDetails)
        {
            var book = JsonSerializer.Deserialize<Rootobject>(bookDetails);
            return book.book;
        }
    }
}
