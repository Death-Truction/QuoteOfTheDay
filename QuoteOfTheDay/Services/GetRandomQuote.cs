using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace QuoteOfTheDay.Services
{
    /// <summary>
    /// Class to get random quotes from different sources.
    /// </summary>
    internal static class GetRandomQuote
    {
        public class quotesRest
        {
            public string quote { get; set; }
        }

        /// <summary>
        /// Get a random Quote from the Quotes.txt file if it exists.
        /// </summary>
        /// <returns>A random Quote</returns>
        internal static string FromFile()
        {
            if (!File.Exists("Quotes.txt"))
            {
                return "Quotes.txt File Not Found!";
            }

            IEnumerable<string> lines = File.ReadLines("Quotes.txt").ToList();
            int numberOfQuotes = lines.Count();
            int randomQuoteIndex = new Random().Next(0, numberOfQuotes);
            return lines.ElementAt(randomQuoteIndex);
        }


        //currently not working
        /* internal static string FromWeb()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(""); // need an api

                HttpResponseMessage response = client.GetAsync("").GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    return "Could not get a quote from the webservice";
                }

                dynamic result = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                return result?.contents.quotes[0].quote;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "Could not get a quote from the webservice";
            }

        }
        */
    }
}
