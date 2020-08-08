using BookProcessor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BookProcessor
{
    public static class WriteOutput
    {
        public static void ToConsole(List<OutputDetails> outputDetails, List<string> notFound)
        {
            foreach (var o in outputDetails)
            {
                Console.WriteLine($"{o.ISBN}({o.ISBN13}) - {o.Title} by {o.Author} is a {o.Binding} published by {o.Publisher} on {o.PublicationDate}");
            }

            if (notFound.Any())
            {
                Console.WriteLine("The following books were not found");

                foreach (var p in notFound)
                {
                    Console.WriteLine(p);
                }
            }
        }

        public static void ToCSV(List<OutputDetails> outputDetails, List<string> notFound)
        {
            var csv = new StringBuilder();
            csv.Append("ISBN(ISBN13),Title,Author,Binding,Publisher,PublicationDate\r\n");
            foreach (var o in outputDetails)
            {
                csv.Append($"{o.ISBN} ({o.ISBN13}),{o.Title},{o.Author},{o.Binding},{o.Publisher},{o.PublicationDate}\r\n");
            }

            if (notFound.Any())
            {
                csv.Append("\r\n\r\nThe following ISBNs were not found\r\n\r\n");

                foreach (var p in notFound)
                {
                    csv.Append(p);
                }
            }
            File.WriteAllText("output.csv", csv.ToString());
        }

        public static FileStreamResult ToFileOutAsCSV(List<OutputDetails> outputDetails, List<string> notFound)
        {
            var csv = new StringBuilder();
            csv.Append("ISBN(ISBN13),Title,Author,Binding,Publisher,PublicationDate\r\n");
            foreach (var o in outputDetails)
            {
                csv.Append($"{o.ISBN} ({o.ISBN13}),{o.Title},{o.Author},{o.Binding},{o.Publisher},{o.PublicationDate}\r\n");
            }

            if (notFound.Any())
            {
                csv.Append("\r\n\r\nThe following ISBNs were not found\r\n\r\n");

                foreach (var p in notFound)
                {
                    csv.Append(p);
                }
            }
            File.WriteAllText("output.csv", csv.ToString());

            Stream s = new FileStream("output.csv", FileMode.Open);
            return new FileStreamResult(s, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "output.csv"
            };
        }
    }
}