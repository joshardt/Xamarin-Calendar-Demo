using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace JFMG.Mobile.Calendar
{
    using Xamarin.Forms;
    
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CalendarPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            base.OnResume();
            // Handle when your app resumes
        }
    }
}
