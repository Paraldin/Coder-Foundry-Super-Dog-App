using SuperDogApp.Models;
using SuperDogApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuperDogApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddComiconPage : ContentPage
    {
        public CreatePageViewModel ViewModel;
        public AddComiconPage()
        {
            ViewModel = new CreatePageViewModel(Navigation);
            InitializeComponent();
            statePicker.ItemsSource = Constants.States;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            ComicCon newEvent = new ComicCon();
            newEvent.EventName = eventNameEntry.Text;
            newEvent.City = cityNameEntry.Text;
            newEvent.State = statePicker.SelectedItem.ToString();
            newEvent.Attendance = Int32.Parse(attendanceEntry.Text);
            newEvent.Date = datePicker.Date;

            await ViewModel.CreateConEvent(newEvent);
            await Navigation.PopAsync();
        }
    }
}