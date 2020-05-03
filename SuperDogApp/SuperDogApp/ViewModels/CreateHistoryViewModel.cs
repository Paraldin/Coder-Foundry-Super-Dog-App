using SuperDogApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperDogApp.ViewModels
{
    class CreateHistoryViewModel
    {
        public ExistingConViewModel ComicConTemp;
        private readonly INavigation _nav;

        public CreateHistoryViewModel(INavigation nav, ComicCon con)
        {
            _nav = nav;
            ComicConTemp = new ExistingConViewModel();
            ComicConTemp.EventName = con.EventName;
            ComicConTemp.City = con.City;
            ComicConTemp.State = con.State;
        }

        public async Task CreateConEvent(ComicCon con)
        {
            await App.Database.SaveItemAsync(con);
        }
    }
}
