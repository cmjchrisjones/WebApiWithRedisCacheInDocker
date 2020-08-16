using BookProcessor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BookProcessor
{
    public static class SerializeBook
    {
        public static Book Serialize(string bookDetails)
        {
            var book = JsonConvert.DeserializeObject<Rootobject>(bookDetails);
            return book.book;
        }
    }
}
