using QuoteOfTheDay.UtilityFunctions;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Media;
using TvSeriesCalendar.UtilityClasses;
using QuoteOfTheDay.Services;

namespace QuoteOfTheDay.ViewModels
{

    internal class SettingsViewModel : ObservableObject
    {
        private Settings _settings = Settings.Instance;
        private List<string> _availableFontStyles;
        private Color _backgroundColorPicker;
        private Color _foregroundColorPicker;
        private string _quoteSource;


        public SettingsViewModel()
        {
            this.Fonts = this.getInstalledFontsAsStringList();
            this.SetAvailableFontStyles();
            this.BackgroundColorPicker = this._settings.BackgroundColor.Color;
            this.ForegroundColorPicker = this._settings.ForegroundColor.Color;
            this.QuoteSource = this._settings.QuoteSource;
        }

        public string QuoteFont
        {
            get => _settings.QuoteFont.Source;
            set
            {
                this._settings.QuoteFont = new System.Windows.Media.FontFamily(value);
                this.SetAvailableFontStyles();
                this.QuoteFontStyle = this.AvailableFontStyles[0];
            }
        }

        public List<string> AvailableFontStyles
        {
            get => this._availableFontStyles;
            set => this.OnPropertyChanged(ref this._availableFontStyles, value);
        }

        public string QuoteFontStyle
        {
            get => _settings.QuoteFontStyle.ToString();
            set => _settings.QuoteFontStyle = HelpFunction.GetFontstyleFromString(value);
        }

        public int QuoteFontSize
        {
            get => _settings.QuoteFontSize;
            set => _settings.QuoteFontSize = value;
        }

        public Color BackgroundColorPicker
        {
            get => this._backgroundColorPicker;
            set
            {
                this.OnPropertyChanged(ref this._backgroundColorPicker, value);
                this._settings.BackgroundColor = new SolidColorBrush(value);
            }
        }

        public Color ForegroundColorPicker
        {
            get => this._foregroundColorPicker;
            set
            {
                this.OnPropertyChanged(ref this._foregroundColorPicker, value);
                this._settings.ForegroundColor = new SolidColorBrush(value);
            }
        }

        public string QuoteSource
        {
            get => this._quoteSource;
            set
            { 
                this.OnPropertyChanged(ref this._quoteSource, value);
                this._settings.QuoteSource = value;
            }
        }

        //public List<string> QuoteSources => new List<string> {"Web", "File"};

        public List<string> Fonts { get; }

        private List<string> getInstalledFontsAsStringList()
        {
            FontCollection FC = new InstalledFontCollection();
            return FC.Families.Select(family => family.Name).ToList();
        }

        private void SetAvailableFontStyles()
        {
            this.AvailableFontStyles = this._settings.QuoteFont.FamilyTypefaces.Select(typeface => typeface.Style.ToString()).Distinct().ToList();
        }
    }
}


// Code to switch between material design themes etc.

/*
using System.Drawing.Text;
using (InstalledFontCollection fontsCollection = new InstalledFontCollection())
{
    FontFamily[] fontFamilies = fontsCollection.Families;
    List<string> fonts = new List<string>();   
    foreach (FontFamily font in fontFamilies)
    {
       fonts.Add(font.Source);
    }
}
 */

/*
 * public event PropertyChangedEventHandler? PropertyChanged;

        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;

            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark;

            if (paletteHelper.GetThemeManager() is { } themeManager)
            {
                themeManager.ThemeChanged += (_, e) =>
                {
                    IsDarkTheme = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
                };
            }
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (this.MutateVerbose(ref _isDarkTheme, value, e => PropertyChanged?.Invoke(this, e)))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        public IEnumerable<Swatch> Swatches { get; }

        public ICommand ApplyPrimaryCommand { get; } = new AnotherCommandImplementation(o => ApplyPrimary((Swatch)o));

        private static void ApplyPrimary(Swatch swatch) 
            => ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));

        public ICommand ApplyAccentCommand { get; } = new AnotherCommandImplementation(o => ApplyAccent((Swatch)o));

        private static void ApplyAccent(Swatch swatch)
        {
            if (swatch is { AccentExemplarHue: not null } )
            {
                ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));
            }
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
    }
 */
