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
    public partial class MapPage : ContentPage
    {
        public MapPage(ComicCon con)
        {
            InitializeComponent();
            BindingContext = new MapPageViewModel(con);
        }
    }
}