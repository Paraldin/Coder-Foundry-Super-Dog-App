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
    public partial class CreateHistoryPage : ContentPage
    {
        private readonly bool _isNewCon;
        private readonly int _conId;

        CreateHistoryViewModel ViewModel;
        public CreateHistoryPage(ComicCon con, bool isNewCon)
        {
            ViewModel = new CreateHistoryViewModel(Navigation, con);
            BindingContext = ViewModel;
            InitializeComponent();

            eventNameEntry.Text = ViewModel.ComicConTemp.EventName;
            cityNameEntry.Text = ViewModel.ComicConTemp.City;
            statePicker.Text = ViewModel.ComicConTemp.State;
            attendanceEntry.Text = isNewCon ? "" : con.Attendance.ToString();
            submittButton.Text = isNewCon ? "Add" : "Edit";
            _isNewCon = isNewCon;
            _conId = con.Id;
        }

         private async void Button_Clicked(object sender, EventArgs e)
         {
             ComicCon newEvent = new ComicCon();
            newEvent.Id = _isNewCon ? 0 : _conId;
             newEvent.EventName = eventNameEntry.Text;
             newEvent.City = cityNameEntry.Text;
             newEvent.State = statePicker.Text;
             newEvent.Attendance = Int32.Parse(attendanceEntry.Text);
             newEvent.Date = datePicker.Date;

             await ViewModel.CreateConEvent(newEvent);
             await Navigation.PopAsync();
         }
    }
}