using LiveSaves.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LiveSaves.Services
{
    public static class LiveService
    {
        
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;

            
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserLives.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Live>();
        }

        public static async Task AddLive(string band, string date, string venue, string image)
        {
            await Init();

            var live = new Live
            {
                Band = band,
                Date = date,
                Venue = venue,
                Image = "plus.jpg",
                MapLocation = venue,
                

        };
            
            var id = await db.InsertAsync(live);
        }


        public static async Task ClearLive()
        {

            await Init();

            await db.DeleteAllAsync<Live>();
        }

        public static async Task RemoveLive(int id)
        {

            await Init();

            await db.DeleteAsync<Live>(id);
        }

        public static async Task<IEnumerable<Live>> GetLive()
        {
            await Init();

            var live = await db.Table<Live>().ToListAsync();
            return live;
        }
    }
}