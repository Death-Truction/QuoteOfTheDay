using System;
using System.Collections.Generic;
using System.Text;
using QuoteOfTheDay.Services;
using TvSeriesCalendar.UtilityClasses;

namespace QuoteOfTheDay.ViewModels
{
    internal class QuoteOfTheDayViewModel : ObservableObject
    {
        private string _quoteText;
        private string _quoteFont;
        private int _quoteFontSize;

        public Settings Settings => Settings.Instance;

        internal QuoteOfTheDayViewModel()
        {
            this.QuoteText = GetRandomQuote.FromFile();
        }

        public string QuoteText
        {
            get => this._quoteText;
            set => this.OnPropertyChanged(ref this._quoteText, value);
        }

    }
}
