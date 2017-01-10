namespace JFMG.Mobile.Calendar
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Xamarin.Forms;

    public partial class CalendarPage
    {
        private readonly ObservableCollection<string> _rangeCollection = new ObservableCollection<string>();
        private bool _shouldShowRange;

        public CalendarPage()
        {
            InitializeComponent();

            UpdateCalendar(ViewModel.CurrentDateTime);

            RangeFrom.MinimumDate = DateTime.Today;
            RangeFrom.Date = DateTime.Today;

            RangeUntil.MinimumDate = RangeFrom.Date;
            RangeUntil.Date = RangeFrom.Date.AddDays(7);

            RangeList.ItemsSource = _rangeCollection;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.PropertyChanged += OnPropertyChanged;
            ViewModel.DidSelectRangeClicked += OnSelectRangeClicked;

            RangeFrom.DateSelected += OnFromRangeSelected;
            RangeUntil.DateSelected += OnUntilRangeSelected;

            RangeList.ItemTapped += OnRangeListItemTapped;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.PropertyChanged -= OnPropertyChanged;
            ViewModel.DidSelectRangeClicked -= OnSelectRangeClicked;

            RangeFrom.DateSelected -= OnFromRangeSelected;
            RangeUntil.DateSelected -= OnUntilRangeSelected;

            RangeList.ItemTapped -= OnRangeListItemTapped;
        }

        private void OnRangeListItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Deselect item
            RangeList.ItemsSource = null;
            RangeList.ItemsSource = _rangeCollection;
        }

        private void OnFromRangeSelected(object sender, DateChangedEventArgs e)
        {
            RangeUntil.MinimumDate = RangeFrom.Date;
        }

        private void OnUntilRangeSelected(object sender, DateChangedEventArgs e)
        {
            RangeFrom.MaximumDate = RangeUntil.Date;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args?.PropertyName == nameof(ViewModel.CurrentDateTime))
                UpdateCalendar(ViewModel.CurrentDateTime);
        }

        private void OnSelectRangeClicked(object sender, EventArgs e)
        {
            _shouldShowRange = true;
            UpdateCalendar(ViewModel.CurrentDateTime);

            _rangeCollection.Add($"{RangeFrom.Date:d} - {RangeUntil.Date:d}");
        }

        private void UpdateCalendar(DateTime dateTime)
        {
            if (dateTime.Day != 1)
                throw new ArgumentException($"{nameof(dateTime)}.Day is {dateTime.Day} but should be 1!");

            // Remove days
            CalendarGrid.Children.Clear();

            // Update month and year label
            MonthLabel.Text = dateTime.ToString("MMMM");
            YearLabel.Text = dateTime.Year.ToString();

            // Add days
            var dayOfWeek = (int) dateTime.DayOfWeek - 1; // -1 to let the week start on Monday instead of Sunday
            if (dayOfWeek < 0)
                dayOfWeek = 6;

            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month) + 1;
            var row = 0;

            for (var day = 1; day < daysInMonth; day++)
            {
                var currentDay = dateTime.AddDays(day - 1);
                var isInRange = currentDay >= RangeFrom.Date && currentDay <= RangeUntil.Date;

                var label = new Label
                {
                    Text = day.ToString(),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                if (_shouldShowRange && isInRange)
                    label.BackgroundColor = Color.Red;

                CalendarGrid.Children.Add(label, dayOfWeek, row);

                dayOfWeek += 1;

                // Begin new row
                if (dayOfWeek > 6)
                {
                    dayOfWeek = 0;
                    row++;
                }
            }
        }
    }
}