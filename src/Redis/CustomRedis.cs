using StackExchange.Redis;
using System;

namespace Redis
{
    public class CustomRedis
    {
        private ConnectionMultiplexer connection;

        private IDatabase database;

        public CustomRedis(string connectionString)
        {
            if (connection == null)
            {
                connection = ConnectionMultiplexer.Connect(connectionString);
            }

            if (database == null)
            {
                database = connection.GetDatabase();
            }
        }

        public string Get(string key)
        {
            Console.WriteLine("Checking Cache");
            var result = database.StringGet(key);
            if (string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine("Not in cache");
            }
            else
            {
                Console.WriteLine("Found cache result - returning from cache");
            }
            return result;
        }

        public void Put(string key, string value)
        {
            Console.WriteLine($"Updating cache with new info for {key}");
            database.StringSet(key, value);
        }
    }
}
