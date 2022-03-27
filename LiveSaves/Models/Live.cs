using System;
using SQLite;
using Xamarin.Forms;

namespace LiveSaves.Models
{
    public class Live
    {
       
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Band { get; set; }
        public string Date { get; set; }
        public string Venue { get; set; }
        public string Image { get; set; }
        public string MapLocation { get; set; }
    }
}
