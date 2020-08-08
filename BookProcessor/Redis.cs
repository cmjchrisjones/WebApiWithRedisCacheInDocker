using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookProcessor
{
    public static class Redis
    {
        public static string Get(string isbn)
        {
            Console.WriteLine("Checking Cache");
            using ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("redis:6379,password=");
            var db = muxer.GetDatabase();
            var result = db.StringGet(isbn);
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

        public static void Put(string isbn, string info)
        {
            Console.WriteLine($"Updating cache with new info for {isbn}");
            using ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("redis:6379,password=");
            var db = muxer.GetDatabase();
            db.StringSet(isbn, info);
        }
    }
}
