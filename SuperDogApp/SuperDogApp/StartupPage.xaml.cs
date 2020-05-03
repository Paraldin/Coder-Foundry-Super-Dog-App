using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuperDogApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class StartupPage : ContentPage
    {
        public StartupPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            await Navigation.PushModalAsync(new NavigationPage(new Views.MainPage()));
            (sender as Button).IsEnabled = true;
        }
    }
}
