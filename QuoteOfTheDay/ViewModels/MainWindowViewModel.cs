using System.ComponentModel;
using System.Windows;
using QuoteOfTheDay.Services;

namespace QuoteOfTheDay.ViewModels
{
    using System;
    using System.Windows.Input;
    using TvSeriesCalendar.UtilityClasses;

    /// <summary>
    /// The ViewModel for the Main Window.
    /// </summary>
    internal sealed class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// Gets the command to open or close the Settings View.
        /// </summary>
        public ICommand OpenOrCloseSettingsCommand { get; }


        private QuoteOfTheDayViewModel _quoteOfTheDayVm;
        private SettingsViewModel _settingsVm;
        private object _currentVm;
        private string _openOrCloseSettingsIconSource;

        public MainWindowViewModel()
        {
            this.SettingsVm = new SettingsViewModel();
            this.QuoteOfTheDayVm = new QuoteOfTheDayViewModel();
            this.OpenOrCloseSettingsCommand = new RelayCommand(this.OpenOrCloseSettings);
            this.OpenOrCloseSettingsIconSource = "/Icons/settingsIcon.png";
            this.CurrentVm = this.QuoteOfTheDayVm;
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Closed += this.MainWindow_Closed;
            }

        }

        public Settings Settings { get; private set; } = Settings.Instance;

        /// <summary>
        /// Gets the current view to display.
        /// </summary>
        public object CurrentVm
        {
            get => this._currentVm;
            private set => this.OnPropertyChanged(ref this._currentVm, value);
        }

        /// <summary>
        /// Gets the Settings Viewmodel.
        /// </summary>
        public SettingsViewModel SettingsVm
        {
            get => this._settingsVm;
            private set => this.OnPropertyChanged(ref this._settingsVm, value);
        }

        /// <summary>
        /// Gets the QuoteOfTheDay Viewmodel.
        /// </summary>
        public QuoteOfTheDayViewModel QuoteOfTheDayVm
        {
            get => this._quoteOfTheDayVm;
            private set => this.OnPropertyChanged(ref this._quoteOfTheDayVm, value);
        }

        public string OpenOrCloseSettingsIconSource
        {
            get => this._openOrCloseSettingsIconSource;
            set => this.OnPropertyChanged(ref this._openOrCloseSettingsIconSource, value);
        }

        /// <summary>
        /// Changes the ViewModel and therefore also the View to Settings.
        /// </summary>
        public void OpenOrCloseSettings()
        {
            if (this.CurrentVm.GetType() == typeof(QuoteOfTheDayViewModel))
            {
                this.OpenSettings();
            }
            else if (this.CurrentVm.GetType() == typeof(SettingsViewModel))
            {
                this.CloseSettings();
            }

            // Error, this should never be reached
            else
            {
                throw new Exception("The CurrentVm holds a wrong value");
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Settings.Instance.SaveSettings();
        }

        private void OpenSettings()
        {
            this.OpenOrCloseSettingsIconSource = "/Icons/closeIcon.png";
            this.CurrentVm = this.SettingsVm;
        }

        private void CloseSettings()
        {
            this.OpenOrCloseSettingsIconSource = "/Icons/settingsIcon.png";
            this.CurrentVm = this.QuoteOfTheDayVm;
        }

    }
}
