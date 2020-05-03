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
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel ViewModel;
        public MainPage()
        {
            ViewModel = new MainPageViewModel(Navigation);
            BindingContext = ViewModel;
            InitializeComponent();
            eventList.ItemsSource = ViewModel.ComicCons;
            eventPicker.ItemsSource = ViewModel.SelectionOptions;
        }

        private void eventPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (sender as Picker).SelectedItem;
            if (selectedItem != null)
            {
                ViewModel.UpdateMath(selectedItem.ToString());
            }
            else
            {
                ViewModel.UpdateMath();
            }
        }

        private void eventList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}