using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDogApp.ViewModels
{
    public class ExistingConViewModel : BaseViewModel
    {
        private string _eventName;
        private string _city;
        private string _state;
        public string EventName
        {
            get { return _eventName; }
            set
            {
                SetProperty(ref _eventName, value);
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                SetProperty(ref _city, value);
            }
        }
        public string State
        {
            get { return _state; }
            set
            {
                SetProperty(ref _state, value);
            }
        }
    }
}
