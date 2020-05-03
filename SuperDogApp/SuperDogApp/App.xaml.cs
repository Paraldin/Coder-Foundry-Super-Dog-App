using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuperDogApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Constants.CreateClient();
            MainPage = new NavigationPage(new StartupPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        static SuperDatabase database;
        public static SuperDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new SuperDatabase();
                }
                return database;
            }
        }
    }
}
