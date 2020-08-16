using BookProcessor.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookProcessor
{
    public interface IProcessBooks
    {
        Task<(List<OutputDetails> OutputDetails, List<string> NotFound)> ByISBNsAsync(List<string> isbns, string apiKey, string connectionString);
  
        Task<OutputDetails> BySingleISBNAsync(string isbn, string apiKey, string connectionString);
    }
}