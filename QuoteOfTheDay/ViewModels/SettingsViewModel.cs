using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using QuoteOfTheDay.Services;
using QuoteOfTheDay.UtilityFunctions;
using TvSeriesCalendar.UtilityClasses;

namespace QuoteOfTheDay.ViewModels
{
    internal class SettingsViewModel : ObservableObject
    {
        private List<string> _availableFontStyles;
        private Color _backgroundColorPicker;
        private Color _foregroundColorPicker;
        private string _quoteFilepath;
        private readonly Settings _settings = Settings.Instance;

        public SettingsViewModel()
        {
            Fonts = getInstalledFontsAsStringList();
            SetAvailableFontStyles();
            BackgroundColorPicker = _settings.BackgroundColor.Color;
            ForegroundColorPicker = _settings.ForegroundColor.Color;
            QuoteFilepath = _settings.LocalQuotesFilePath;
            SelectQuoteFileCommand = new RelayCommand(SelectQuoteFile);
        }

        public ICommand SelectQuoteFileCommand { get; }

        public string QuoteFont
        {
            get => _settings.QuoteFont.Source;
            set
            {
                _settings.QuoteFont = new FontFamily(value);
                SetAvailableFontStyles();
                QuoteFontStyle = AvailableFontStyles[0];
            }
        }

        public List<string> AvailableFontStyles
        {
            get => _availableFontStyles;
            set => OnPropertyChanged(ref _availableFontStyles, value);
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
            get => _backgroundColorPicker;
            set
            {
                OnPropertyChanged(ref _backgroundColorPicker, value);
                _settings.BackgroundColor = new SolidColorBrush(value);
            }
        }

        public Color ForegroundColorPicker
        {
            get => _foregroundColorPicker;
            set
            {
                OnPropertyChanged(ref _foregroundColorPicker, value);
                _settings.ForegroundColor = new SolidColorBrush(value);
            }
        }

        public string QuoteFilepath
        {
            get => _quoteFilepath;
            set
            {
                OnPropertyChanged(ref _quoteFilepath, value);
                _settings.LocalQuotesFilePath = value;
            }
        }


        public List<string> Fonts { get; }

        private List<string> getInstalledFontsAsStringList()
        {
            FontCollection fc = new InstalledFontCollection();
            return fc.Families.Select(family => family.Name).ToList();
        }

        private void SetAvailableFontStyles()
        {
            AvailableFontStyles = _settings.QuoteFont.FamilyTypefaces.Select(typeface => typeface.Style.ToString())
                .Distinct().ToList();
        }

        private void SelectQuoteFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true) QuoteFilepath = fileDialog.FileName;
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