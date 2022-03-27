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
  
    public partial class LoginPage : ContentPage
    {


        public LoginPage()
        {
            InitializeComponent();
        }

        async void Login_Clicked(System.Object sender, System.EventArgs e)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserDB.db3");
            var db = new SQLiteConnection(databasePath);
            var myquery = db.Table<RegUser>().Where(u => u.UserName == InputUser.Text && u.Password == InputPass.Text).FirstOrDefault(); ;
            if(myquery != null)
            {
                
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
    }
}