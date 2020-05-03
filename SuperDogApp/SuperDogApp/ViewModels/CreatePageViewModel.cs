using SuperDogApp.Extensions;
using SuperDogApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuperDogApp.ViewModels
{
    public class CreatePageViewModel
    {
        private readonly INavigation _nav;

        public CreatePageViewModel(INavigation nav)
        {
            _nav = nav;
        }

        public async Task CreateConEvent(ComicCon con)
        {
            await App.Database.SaveItemAsync(con);
        }
    }
}
