using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteOfTheDay.Services
{
    internal sealed class Settings
    {
        private static readonly Settings _instance = new Settings();  
        // Explicit static constructor to tell C# compiler  
        // not to mark type as beforefieldinit  
        static Settings()
        {
        }

        private Settings()
        {
        }

        public static Settings Instance => _instance;

        internal static string LocalQuotesFilePath { get; set; }

    }
}