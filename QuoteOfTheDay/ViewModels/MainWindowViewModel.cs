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

        internal MainWindowViewModel()
        {
            this.SettingsVm = new SettingsViewModel();
            this.QuoteOfTheDayVm = new QuoteOfTheDayViewModel();
            this.OpenOrCloseSettingsCommand = new RelayCommand(this.OpenOrCloseSettings);
            this.OpenOrCloseSettingsIconSource = "/Icons/settingsIcon.png";
            this.CurrentVm = this.QuoteOfTheDayVm;
        }

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

        private void OpenSettings()
        {
            this.CurrentVm = this.SettingsVm;
            this.OpenOrCloseSettingsIconSource = "/Icons/closeIcon.png";
        }

        private void CloseSettings()
        {
            this.CurrentVm = this.QuoteOfTheDayVm;
            this.OpenOrCloseSettingsIconSource = "/Icons/settingsIcon.png";
        }
    }
}
