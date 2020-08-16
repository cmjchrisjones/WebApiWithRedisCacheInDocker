using BookProcessor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var isbns = new List<string>
            {
                "1449373321",
                "1789800986",
                "1407152769",
                "1509830464",
                "9123651253",
                "0552159719",
                "055216089X",
                "0552159727",
                "0552159735",
                "sjkdfh"
            };

            var apiKey = Environment.GetEnvironmentVariable("ApiKey");
            var connectionString = Environment.GetEnvironmentVariable("RedisConnectionString");
            var results = await new ProcessBooks().ByISBNsAsync(isbns, apiKey, connectionString);
            WriteOutput.ToConsole(results.OutputDetails, results.NotFound);
            WriteOutput.ToCSV(results.OutputDetails, results.NotFound);
            System.Console.WriteLine("Finished");
        }
    }
}
