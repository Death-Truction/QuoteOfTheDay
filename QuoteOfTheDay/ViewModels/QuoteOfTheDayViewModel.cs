using QuoteOfTheDay.Services;
using TvSeriesCalendar.UtilityClasses;

namespace QuoteOfTheDay.ViewModels
{
    internal class QuoteOfTheDayViewModel : ObservableObject
    {
        private string _quoteFont;
        private int _quoteFontSize;
        private string _quoteText;

        internal QuoteOfTheDayViewModel()
        {
            QuoteText = GetRandomQuote.FromFile();
        }

        public Settings Settings => Settings.Instance;

        public string QuoteText
        {
            get => _quoteText;
            set => OnPropertyChanged(ref _quoteText, value);
        }
    }
}