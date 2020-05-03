using SuperDogApp.Models;
using SuperDogApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuperDogApp.ViewModels
{
    public class MainPageViewModel
    {
        private readonly INavigation _nav;

        public ObservableCollection<ComicCon> ComicCons { get; private set; }
        public ObservableCollection<string> SelectionOptions { get; private set; }
        public MathViewModel MathAmounts { get; private set; }
        public ICommand RefreshList { get; }
        public ICommand CreateComicCon { get; }
        public ICommand CreateComicConHistory { get; }
        public ICommand EditComicCon { get; }
        public ICommand DeleteComicCon { get; }
        public ICommand MapComicCon { get; }
        public MainPageViewModel(INavigation nav)
        {
            ComicCons = new ObservableCollection<ComicCon>();
            SelectionOptions = new ObservableCollection<string>();
            RefreshList = new Command(async () => await RefreshConList());
            CreateComicCon = new Command(async () => await CreatePage());
            CreateComicConHistory = new Command<ComicCon>(async (con) => await RenderHistoryPage(con));
            DeleteComicCon = new Command<ComicCon>(async (con) => await DeleteCon(con));
            EditComicCon = new Command<ComicCon>(async (con) => await RenderEditPage(con));
            MapComicCon = new Command<ComicCon>(async (con) => await RenderMapPage(con));
            MathAmounts = new MathViewModel();
            _nav = nav;
        }

        private async Task RefreshConList()
        {
            var list = await App.Database.GetConsAsync();
            ComicCons.Clear();
            foreach(var con in list)
            {
                ComicCons.Add(con);
            }
            RefreshSelection();
        }
        public void UpdateMath(string eventName)
        {
            if(eventName == "--All Cons Together--")
            {
                var attendanceRecords = ComicCons.Select(c => c.Attendance).ToList();
                DoTheMaths(attendanceRecords);
            }
            else
            {
                var attendanceRecords = ComicCons.Where(c => c.EventName == eventName).Select(c => c.Attendance).ToList();
                DoTheMaths(attendanceRecords);
            }
        }

        private void DoTheMaths(List<int> attendanceRecords)
        {
            MathAmounts.Total = attendanceRecords.Sum();
            MathAmounts.Most = attendanceRecords.Max();
            MathAmounts.Least = attendanceRecords.Min();
            MathAmounts.Average = attendanceRecords.Average();
        }

        public void UpdateMath()
        {
            MathAmounts.Total = 0;
            MathAmounts.Most = 0;
            MathAmounts.Least = 0;
            MathAmounts.Average = 0;
        }

        private void RefreshSelection()
        {
            SelectionOptions.Clear();
            SelectionOptions.Add("--All Cons Together--");
            foreach(var con in ComicCons.Select(c => c.EventName).Distinct())
            {
                SelectionOptions.Add(con.ToString());
            }
        }

        private async Task CreatePage()
        {
            await _nav.PushAsync(new AddComiconPage());
        }
        
        private async Task RenderHistoryPage(ComicCon con)
        {
            await _nav.PushAsync(new CreateHistoryPage(con, true));
        }
        private async Task RenderEditPage(ComicCon con)
        {
            await _nav.PushAsync(new CreateHistoryPage(con, false));
        }
        private async Task RenderMapPage(ComicCon con)
        {
            await _nav.PushAsync(new MapPage(con));
        }
        private async Task DeleteCon(ComicCon con)
        {
            ComicCons.Remove(con);
            await App.Database.DeleteItemAsync(con);
        }
    }
}
