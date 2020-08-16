using BookProcessor.DTO;
using BookProcessor.Extensions;
using Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookProcessor
{
    public class ProcessBooks : IProcessBooks
    {
        public async Task<(List<OutputDetails> OutputDetails, List<string> NotFound)> ByISBNsAsync(List<string> isbns, string apiKey, string connectionString)
        {
            (List<OutputDetails> OutputDetails, List<string> NotFound) results;

            results.OutputDetails = new List<OutputDetails>();
            results.NotFound = new List<string>();

            foreach (var isbn in isbns)
            {
                var formattedISBN = isbn.RemoveHyphensFromISBN();
                Console.WriteLine($"Getting info for {formattedISBN}");
                var cachedResult = TryGetFromCache(formattedISBN, connectionString);

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

                        AddToCache(formattedISBN, bookInfo, connectionString);
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

        public async Task<OutputDetails> BySingleISBNAsync(string isbn, string apiKey, string connectionString)
        {
            var formattedISBN = isbn.RemoveHyphensFromISBN();
            Console.WriteLine($"Getting info for {formattedISBN}");
            var cachedResult = TryGetFromCache(formattedISBN, connectionString);
            OutputDetails formattedBookDetails = null;
            if (!string.IsNullOrWhiteSpace(cachedResult))
            {
                var serializedBook = SerializeBook.Serialize(cachedResult);
                return Format.ForOutputList(serializedBook);
            }
            else
            {
                var bookInfo = await IsbnDb.GetInfoFromAPI(formattedISBN, apiKey);
                if (!string.IsNullOrWhiteSpace(bookInfo))
                {
                    var serializedBook = SerializeBook.Serialize(bookInfo);
                    formattedBookDetails = Format.ForOutputList(serializedBook);

                    if (formattedBookDetails != null)
                    {
                        AddToCache(formattedISBN, bookInfo, connectionString);
                    }


                }
            }
            Console.WriteLine($"Finished {formattedISBN}");
            return formattedBookDetails;
        }

        public string TryGetFromCache(string isbn, string connectionString)
        {
            return new CustomRedis(connectionString).Get(isbn);
        }

        public void AddToCache(string isbn, string info, string connectionString)
        {
           new CustomRedis(connectionString).Put(isbn, info);
        }
    }
}