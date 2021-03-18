using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using QuoteOfTheDay.UtilityFunctions;
using TvSeriesCalendar.UtilityClasses;

namespace QuoteOfTheDay.Services
{
    /// <summary>
    ///     A Singleton class for the settings.
    /// </summary>
    internal sealed class Settings : ObservableObject
    {
        private SolidColorBrush _backgroundColor;
        private int _fontSize;
        private SolidColorBrush _foregroundColor;
        private FontFamily _quoteFont;
        private FontStyle _quoteFontStyle;

        private Settings()
        {
            loadSettings();
        }

        public static Settings Instance { get; } = new Settings();

        public string LocalQuotesFilePath { get; set; }

        public FontFamily QuoteFont
        {
            get => _quoteFont;
            set => OnPropertyChanged(ref _quoteFont, value);
        }

        public FontStyle QuoteFontStyle
        {
            get => _quoteFontStyle;
            set => OnPropertyChanged(ref _quoteFontStyle, value);
        }

        public int QuoteFontSize
        {
            get => _fontSize;
            set => OnPropertyChanged(ref _fontSize, value);
        }

        public SolidColorBrush BackgroundColor
        {
            get => _backgroundColor;
            set => OnPropertyChanged(ref _backgroundColor, value);
        }

        public SolidColorBrush ForegroundColor
        {
            get => _foregroundColor;
            set => OnPropertyChanged(ref _foregroundColor, value);
        }

        private void loadSettings()
        {
            LocalQuotesFilePath = Properties.Settings.Default.LocalQuotesFilePath;
            if (string.IsNullOrEmpty(LocalQuotesFilePath))
            {
                string publicUserPath = Environment.GetEnvironmentVariable("PUBLIC") ?? string.Empty;
                LocalQuotesFilePath = Path.Combine(publicUserPath, "Downloads\\Quotes.txt");
            }

            QuoteFont = new FontFamily(Properties.Settings.Default.QuoteFont);
            QuoteFontSize = Properties.Settings.Default.QuoteFontSize;
            BackgroundColor =
                (SolidColorBrush) new BrushConverter().ConvertFrom(Properties.Settings.Default.BackgroundColor);
            ForegroundColor =
                (SolidColorBrush) new BrushConverter().ConvertFrom(Properties.Settings.Default.ForegroundColor);
            QuoteFontStyle = HelpFunction.GetFontstyleFromString(Properties.Settings.Default.QuoteFontStyle);
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.LocalQuotesFilePath = LocalQuotesFilePath;
            Properties.Settings.Default.QuoteFont = QuoteFont.Source;
            Properties.Settings.Default.QuoteFontStyle = QuoteFontStyle.ToString();
            Properties.Settings.Default.QuoteFontSize = QuoteFontSize;
            Properties.Settings.Default.BackgroundColor = BackgroundColor.Color.ToString();
            Properties.Settings.Default.ForegroundColor = ForegroundColor.Color.ToString();
            Properties.Settings.Default.Save();
        }
    }
}