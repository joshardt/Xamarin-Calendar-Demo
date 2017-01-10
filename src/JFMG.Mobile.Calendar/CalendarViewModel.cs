namespace JFMG.Mobile.Calendar
{
    using System;
    using System.ComponentModel;
    using Xamarin.Forms;

    public sealed class CalendarViewModel : INotifyPropertyChanged
    {
        private DateTime _currentDateTime;
        private DateTime _fromTime;
        private DateTime _untilTime;

        public CalendarViewModel()
        {
            var startDateTime = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
            CurrentDateTime = startDateTime;

            Back = new Command(() =>
            {
                try
                {
                    CurrentDateTime = CurrentDateTime.AddMonths(-1);
                }
                catch (Exception) // possible ArgumentOutOfRangeException
                {
                    CurrentDateTime = DateTime.MinValue;
                }
            });

            Next = new Command(() =>
            {
                try
                {
                    CurrentDateTime = CurrentDateTime.AddMonths(1);
                }
                catch (Exception) // possible ArgumentOutOfRangeException
                {
                    CurrentDateTime = DateTime.MaxValue;
                }
            });

            Func<bool> canSelectRange = () => FromTime <= UntilTime;
            SelectRange = new Command(() => DidSelectRangeClicked?.Invoke(this, EventArgs.Empty), canSelectRange);
        }

        public event EventHandler DidSelectRangeClicked;

        #region Properties

        public DateTime CurrentDateTime
        {
            get { return _currentDateTime; }
            private set
            {
                _currentDateTime = value;
                RaisePropertyChanged(nameof(CurrentDateTime));
            }
        }

        public DateTime FromTime
        {
            get { return _fromTime; }
            set
            {
                _fromTime = value;
                SelectRange.ChangeCanExecute();
            }
        }

        public DateTime UntilTime
        {
            get { return _untilTime; }
            set
            {
                _untilTime = value;
                SelectRange.ChangeCanExecute();
            }
        }

        public Command Back { get; }
        public Command Next { get; }
        public Command SelectRange { get; }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}