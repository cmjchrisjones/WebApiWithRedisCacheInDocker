using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookProcessor
{
    public static class IsbnDb
    {
        public static async Task<string> GetInfoFromAPI(string isbn, string apiKey)
        {
            if (string.IsNullOrEmpty(isbn))
            {
                throw new ArgumentNullException("No ISBN provided");
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException("No API key found");
            };

            Console.WriteLine("Calling the API");
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", apiKey);
            var url = $"https://api2.isbndb.com/book/{isbn}";
            Console.WriteLine($"URL: {url}");
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var bookDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine(bookDetails);
                return bookDetails;
            }
            return null;
        }
    }
}
