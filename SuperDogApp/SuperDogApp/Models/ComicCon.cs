using SQLite;
using SuperDogApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDogApp.Models
{
    public class ComicCon : BaseViewModel
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public string EventName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Attendance { get; set; }
        public DateTime Date { get; set; }
    }
}
