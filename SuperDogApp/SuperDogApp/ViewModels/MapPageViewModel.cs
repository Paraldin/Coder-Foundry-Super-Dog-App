using SuperDogApp.Models;
using SuperDogApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SuperDogApp.ViewModels
{
    class MapPageViewModel : BaseViewModel
    {
        private string _direction;
        private string _name;
        public SearchResultModel SearchResultModel { get; }
        public Map ConMap { get; private set; }

        public MapPageViewModel(ComicCon con)
        {
            SearchResultModel = new SearchResultModel();
            ConMap = new Map();
            ConMap.HasScrollEnabled = true;
            ConMap.HasZoomEnabled = true;
            ConMap.IsShowingUser = true;
            var searchString = $"{con.City}, {con.State}";
            Name = con.EventName;
            GetLocalPosition(searchString);
        }
        public string Direction
        {
            get { return _direction; }
            set { SetProperty(ref _direction, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public async void GetLocalPosition(string searchString)
        {
            try
            {
                IMapService mapsService = new MapService();
                var model = new SearchModel
                {
                    Name = searchString,
                    InputType = "textquery",
                    Fields = "name,geometry,formatted_address",
                };

                //Gets Co-Ordinates
                SearchResultModel resultModel = await mapsService.GetTextSearch(model);
                var lat = resultModel.Candidates.FirstOrDefault().Geometry.Location.Lat;
                var lng = resultModel.Candidates.FirstOrDefault().Geometry.Location.Lng;

                //Calls GoogleMap
                Position currentposition = new Position(lat, lng);
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(currentposition, Distance.FromKilometers(1));
                ConMap.MoveToRegion(mapSpan);

                //Creates Pin
                Pin pin = new Pin
                {
                    Label = Name,
                    Address = Direction,
                    Type = PinType.SearchResult,
                    Position = currentposition
                };
                ConMap.Pins.Add(pin);
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}
