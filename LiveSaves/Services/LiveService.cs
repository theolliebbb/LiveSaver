using LiveSaves.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using LiveSaves.Views;

namespace LiveSaves.Services
{
    public static class LiveService
    {
        public static string Use = LoginPage.User;
        static SQLiteConnection db;
        static async Task Init()
        {
            db = null;
            if (db != null)
                return;

           
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Lives{UserDisplayName.displayName}.db");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<Live>();
        }
        static async Task Initminus()
        {
            if (db != null)
                return;


            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Lives{UserDisplayName.displayName}.db");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<Live>();
        }

        public static async Task AddLive(string band, string date, string venue, string image)
        {
            await Initminus();

            var live = new Live
            {
                Band = band,
                Date = date,
                Venue = venue,
                Image = image
                
               
                

        };
            
            var id = db.Insert(live);
        }



        public static async Task ClearLive()
        {

            await Initminus();

             db.DeleteAll<Live>();
        }

        public static async Task RemoveLive(int id)
        {

            await Init();

            db.Delete<Live>(id);
        }


        public static async Task<IEnumerable<Live>> GetLive()
        {
            await Initminus();

            var live = db.Table<Live>().ToList();
            return live;
        }
        public static void Logout()
        {
            db = null;
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}