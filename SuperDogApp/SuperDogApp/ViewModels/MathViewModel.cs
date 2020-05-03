using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDogApp.ViewModels
{
    public class MathViewModel : BaseViewModel
    {
        int _total;
        int _most;
        int _least;
        double _average;

        public int Total {
            get { return _total; }
            set
            {
                SetProperty(ref _total, value);
            }
        }
        public int Most {
            get { return _most; }
            set
            {
                SetProperty(ref _most, value);
            }
        }
        public int Least {
            get { return _least; }
            set
            {
                SetProperty(ref _least, value);
            }
        }
        public double Average {
            get { return _average; }
            set
            {
                SetProperty(ref _average, value);
            }
        }
        public MathViewModel()
        {
            Total = 0;
            Most = 0;
            Least = 0;
            Average = 0;
        }
    }
}
