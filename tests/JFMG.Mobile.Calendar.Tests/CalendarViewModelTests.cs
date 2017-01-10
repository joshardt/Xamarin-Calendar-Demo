namespace JFMG.Mobile.Calendar.Tests
{
    using System;
    using Xunit;

    public class CalendarViewModelTests
    {
        [Fact]
        public void CanExecuteBackButtonTest()
        {
            // Prepare
            var viewModel = new CalendarViewModel();

            // Test
            Assert.True(viewModel.Back.CanExecute(null));
        }

        [Fact]
        public void CanExecuteNextButtonTest()
        {
            // Prepare
            var viewModel = new CalendarViewModel();

            // Test
            Assert.True(viewModel.Next.CanExecute(null));
        }

        [Fact]
        public void CanExecuteSelectRangeButtonTest()
        {
            // Prepare
            var viewModel = new CalendarViewModel();

            // Test
            Assert.True(viewModel.SelectRange.CanExecute(null));
        }

        [Fact]
        public void CannotExecuteSelectRangeButtonTest()
        {
            // Prepare
            var viewModel = new CalendarViewModel();

            // Act
            viewModel.FromTime = DateTime.MaxValue;
            viewModel.UntilTime = DateTime.MinValue;

            viewModel.SelectRange.ChangeCanExecute();

            // Test
            Assert.False(viewModel.SelectRange.CanExecute(null));
        }

        [Fact]
        public void CurrentDateTimeStartsWithDayOneTest()
        {
            // Prepare
            var viewModel = new CalendarViewModel();

            // Act
            var dayOne = viewModel.CurrentDateTime.Day;

            // Test
            Assert.InRange(dayOne, 1, 1);
        }
    }
}