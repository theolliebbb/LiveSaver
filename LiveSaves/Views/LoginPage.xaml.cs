using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using Xamarin.Forms.Xaml;
using LiveSaves.Views;
using System.IO;
using LiveSaves.Models;
using Xamarin.Essentials;

namespace LiveSaves.Views
{
    public class UserDisplayName
    {
        public static string displayName;

        void Forgot_Clicked(System.Object sender, System.EventArgs e)
        {
        }

    }
    public partial class LoginPage : ContentPage
    {
        static SQLiteConnection db;
        public bool ValidateExistence()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");
            var db = new SQLiteConnection(databasePath);
            var results = db.Table<RegUser>();
            
            return (results != null);
        }
        public static string User;
        private bool DoesTableExist(string name)
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");
            var db = new SQLiteConnection(databasePath);
            SQLiteCommand command = db.CreateCommand("SELECT COUNT(1) FROM SQLITE_MASTER WHERE TYPE = @TYPE AND NAME = @NAME");
            command.Bind("@TYPE", "table");
            command.Bind("@NAME", name);

            int result = command.ExecuteScalar<int>();
            return (result > 0);
        }
        public LoginPage()
        {
            InitializeComponent();
            if (db != null)
                return;

            
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<RegUser>();

        }

        async void ClearAll_Clicked(System.Object sender, System.EventArgs e)
        {





            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<RegUser>();


            var results = db.Table<RegUser>().Where(v => v.UserName
                == InputUser.Text && v.Password == InputPass.Text).ToList();



            User = InputUser.Text;
            if (results.Count > 0 && InputUser.Text != null && InputPass.Text != null)
            {

                var thelist = db.Table<RegUser>()
                        .Where(v => v.UserName
                == InputUser.Text).FirstOrDefault();
                
                db.Delete(thelist);
                await DisplayAlert("Success!", "User Successfully Deleted!", "OK");
            }
            else
            {
                await DisplayAlert("Error!", "Please Enter Correct User Name and Password Combination to Delete!", "Try Again!");
            }

        }
        async void Login_Clicked(System.Object sender, System.EventArgs e)
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<RegUser>();


            var results = db.Table<RegUser>().Where(v => v.UserName
                == InputUser.Text && v.Password == InputPass.Text).ToList();



                User = InputUser.Text;
                if (results.Count > 0 && InputUser.Text != null && InputPass.Text != null)
                {

                    UserDisplayName.displayName = InputUser.Text;
                    App.Current.MainPage = new NavigationPage(new Items());
                }
                else
                {
                    await DisplayAlert("Error!", "User Name and Password Combination Invalid!", "Try Again!");
                }
            
        }

        async void Register_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new RegistrationPage());
        }
        async void Forgot_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Forgot());
        }
    }
}