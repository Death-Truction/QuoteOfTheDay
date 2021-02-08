using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;

namespace QuoteOfTheDay.UtilityFunctions
{
    internal static class HelpFunction
    {
        internal static FontStyle GetFontstyleFromString(string fontStyle)
        {
            var propertyInfo = typeof(FontStyles).GetProperty(fontStyle, BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase);
            return (FontStyle)propertyInfo.GetValue(null, null);
        }
    }
}
