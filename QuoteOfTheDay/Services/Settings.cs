using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using QuoteOfTheDay.UtilityFunctions;
using TvSeriesCalendar.UtilityClasses;

namespace QuoteOfTheDay.Services
{
    /// <summary>
    /// A Singleton class for the settings.
    /// </summary>
    internal sealed class Settings : ObservableObject
    {
        private static readonly Settings _instance = new Settings();
        private int _fontSize;
        private FontFamily _quoteFont;
        private FontStyle _quoteFontStyle;
        private SolidColorBrush _backgroundColor;
        private SolidColorBrush _foregroundColor;

        public static Settings Instance => _instance;

        public string LocalQuotesFilePath { get; set; }

        public FontFamily QuoteFont
        {
            get => this._quoteFont;
            set => this.OnPropertyChanged(ref this._quoteFont, value);
        }

        public FontStyle QuoteFontStyle
        {
            get => this._quoteFontStyle;
            set => this.OnPropertyChanged(ref this._quoteFontStyle, value);
        }

        public int QuoteFontSize
        {
            get => this._fontSize;
            set => this.OnPropertyChanged(ref this._fontSize, value);
        }

        public SolidColorBrush BackgroundColor
        {
            get => this._backgroundColor;
            set => this.OnPropertyChanged(ref this._backgroundColor, value);
        }

        public SolidColorBrush ForegroundColor
        {
            get => this._foregroundColor;
            set => this.OnPropertyChanged(ref this._foregroundColor, value);
        }

        static Settings()
        {
        }

        private Settings()
        {
            this.loadSettings();
        }

        private void loadSettings()
        {
            this.LocalQuotesFilePath = Properties.Settings.Default.LocalQuotesFilePath;
            this.QuoteFont = new FontFamily(Properties.Settings.Default.QuoteFont);
            this.QuoteFontSize = Properties.Settings.Default.QuoteFontSize;
            this.BackgroundColor = (SolidColorBrush) new BrushConverter().ConvertFrom(Properties.Settings.Default.BackgroundColor);
            this.ForegroundColor = (SolidColorBrush) new BrushConverter().ConvertFrom(Properties.Settings.Default.ForegroundColor);
            this.QuoteFontStyle = HelpFunction.GetFontstyleFromString(Properties.Settings.Default.QuoteFontStyle);
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.LocalQuotesFilePath = this.LocalQuotesFilePath;
            Properties.Settings.Default.QuoteFont = this.QuoteFont.Source;
            Properties.Settings.Default.QuoteFontStyle = this.QuoteFontStyle.ToString();
            Properties.Settings.Default.QuoteFontSize = this.QuoteFontSize;
            Properties.Settings.Default.BackgroundColor = this.BackgroundColor.Color.ToString();
            Properties.Settings.Default.ForegroundColor = this.ForegroundColor.Color.ToString();
            Properties.Settings.Default.Save();
        }

    }
}