using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookProcessor
{
    public class ProcessBooks
    {
        public async Task<(List<OutputDetails> OutputDetails, List<string> NotFound)> ByISBNsAsync(List<string> isbns, string apiKey)
        {
            (List<OutputDetails> OutputDetails, List<string> NotFound) results;

            results.OutputDetails = new List<OutputDetails>();
            results.NotFound = new List<string>();

            foreach (var isbn in isbns)
            {
                var formattedISBN = isbn.Replace("-", string.Empty);
                Console.WriteLine($"Getting info for {formattedISBN}");
                var cachedResult = TryGetFromCache(formattedISBN);

                if (!string.IsNullOrWhiteSpace(cachedResult))
                {
                    var serializedBook = SerializeBook.Serialize(cachedResult);
                    results.OutputDetails.Add(Format.ForOutputList(serializedBook));
                }
                else
                {
                    var bookInfo = await IsbnDb.GetInfoFromAPI(formattedISBN, apiKey);
                    if (!string.IsNullOrWhiteSpace(bookInfo))
                    {
                        var serializedBook = SerializeBook.Serialize(bookInfo);
                        var formattedBookDetails = Format.ForOutputList(serializedBook);

                        if (formattedBookDetails != null)
                        {
                            results.OutputDetails.Add(formattedBookDetails);
                        }

                        AddToCache(formattedISBN, bookInfo);
                    }
                    else
                    {
                        results.NotFound.Add(formattedISBN);
                    }
                }

                Console.WriteLine($"Finished {formattedISBN}");
            }
            return results;
        }

        public static string TryGetFromCache(string isbn)
        {
            return Redis.Get(isbn);
        }

        public static void AddToCache(string isbn, string info)
        {
            Redis.Put(isbn, info);
        }
    }
}