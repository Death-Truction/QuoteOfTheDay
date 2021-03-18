using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuoteOfTheDay.Services
{
    /// <summary>
    ///     Class to get random quotes from different sources.
    /// </summary>
    internal static class GetRandomQuote
    {
        /// <summary>
        ///     Get a random Quote from the Quotes.txt file if it exists.
        /// </summary>
        /// <returns>A random Quote</returns>
        internal static string FromFile()
        {
            string quotePath = Settings.Instance.LocalQuotesFilePath;
            if (!File.Exists(quotePath)) return $"Could not find the file: \n {quotePath}";

            IEnumerable<string> lines = File.ReadLines(quotePath).ToList();
            int numberOfQuotes = lines.Count();
            int randomQuoteIndex = new Random().Next(0, numberOfQuotes);
            return lines.ElementAt(randomQuoteIndex);
        }

        public static class quotesRest
        {
            public static string quote { get; set; }
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