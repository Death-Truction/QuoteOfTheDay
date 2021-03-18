using System;
using System.Windows;
using System.Windows.Input;
using QuoteOfTheDay.Services;
using TvSeriesCalendar.UtilityClasses;

namespace QuoteOfTheDay.ViewModels
{
    /// <summary>
    ///     The ViewModel for the Main Window.
    /// </summary>
    internal sealed class MainWindowViewModel : ObservableObject
    {
        private object _currentVm;
        private string _openOrCloseSettingsIconSource;


        private QuoteOfTheDayViewModel _quoteOfTheDayVm;
        private SettingsViewModel _settingsVm;

        public MainWindowViewModel()
        {
            SettingsVm = new SettingsViewModel();
            QuoteOfTheDayVm = new QuoteOfTheDayViewModel();
            OpenOrCloseSettingsCommand = new RelayCommand(OpenOrCloseSettings);
            OpenOrCloseSettingsIconSource = "/Icons/settingsIcon.png";
            CurrentVm = QuoteOfTheDayVm;
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Closed += MainWindow_Closed;
        }

        /// <summary>
        ///     Gets the command to open or close the Settings View.
        /// </summary>
        public ICommand OpenOrCloseSettingsCommand { get; }

        public Settings Settings { get; } = Settings.Instance;

        /// <summary>
        ///     Gets the current view to display.
        /// </summary>
        public object CurrentVm
        {
            get => _currentVm;
            private set => OnPropertyChanged(ref _currentVm, value);
        }

        /// <summary>
        ///     Gets the Settings Viewmodel.
        /// </summary>
        public SettingsViewModel SettingsVm
        {
            get => _settingsVm;
            private set => OnPropertyChanged(ref _settingsVm, value);
        }

        /// <summary>
        ///     Gets the QuoteOfTheDay Viewmodel.
        /// </summary>
        public QuoteOfTheDayViewModel QuoteOfTheDayVm
        {
            get => _quoteOfTheDayVm;
            private set => OnPropertyChanged(ref _quoteOfTheDayVm, value);
        }

        public string OpenOrCloseSettingsIconSource
        {
            get => _openOrCloseSettingsIconSource;
            set => OnPropertyChanged(ref _openOrCloseSettingsIconSource, value);
        }

        /// <summary>
        ///     Changes the ViewModel and therefore also the View to Settings.
        /// </summary>
        public void OpenOrCloseSettings()
        {
            if (CurrentVm.GetType() == typeof(QuoteOfTheDayViewModel))
                OpenSettings();
            else if (CurrentVm.GetType() == typeof(SettingsViewModel))
                CloseSettings();

            // Error, this should never be reached
            else
                throw new Exception("The CurrentVm holds a wrong value");
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Settings.Instance.SaveSettings();
        }

        private void OpenSettings()
        {
            OpenOrCloseSettingsIconSource = "/Icons/closeIcon.png";
            CurrentVm = SettingsVm;
        }

        private void CloseSettings()
        {
            OpenOrCloseSettingsIconSource = "/Icons/settingsIcon.png";
            CurrentVm = QuoteOfTheDayVm;
        }
    }
}