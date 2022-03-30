using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LiveSaves.Models;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LiveSaves.Views
{
    public partial class Forgot : ContentPage
    {
        static SQLiteConnection db;
        RegUser userDetails;
        public Forgot()
        {
            InitializeComponent();
            if (db != null)
                return;


            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<RegUser>();
        }

        async void ForgotButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<RegUser>();

            var results = db.Table<RegUser>().Where(v => v.UserName
                == InputUser.Text).ToList();




            if (results.Count > 0 && InputUser.Text != null)
            {
                await DisplayAlert("UserName Found!", "Please Enter New Password!", "OK!");
                Pass.IsVisible = true;
                Pass2.IsVisible = true;
                InputUser.IsVisible = false;
                ShowUser.Text = InputUser.Text;
                ShowUser.IsVisible = true;
                ForgotButton.IsVisible = false;
                ConfirmButton.IsVisible = true;

            }
            else
            {
                await DisplayAlert("Error!", "User Name does not exist!", "Try Again!");
            }
        }


        void ConfirmButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (Pass.Text != null)
            {
                var lowerPass = Pass.Text.ToLower();
                var upperPass = Pass.Text.ToUpper();

                {
                    if (Pass.Text != Pass2.Text)
                    {

                        DisplayAlert("Error!", "Your Passwords Do Not Match!!", "Try Again!");
                        Pass.Text = null;
                        Pass2.Text = null;
                    }
                    else if (Pass.Text == null || Pass2.Text == null)
                    {

                        DisplayAlert("Error!", "Please Input All Fields!", "Try Again!");
                        Pass.Text = null;
                        Pass2.Text = null;
                    }
                    else if (InputUser.Text == Pass.Text)
                    {

                        DisplayAlert("Error!", "UserName and Password cannot be identical!", "Try Again!");

                        Pass.Text = null;
                        Pass2.Text = null;
                    }


                    else if (lowerPass == Pass.Text || upperPass == Pass.Text)
                    {

                        DisplayAlert("Error!", "Make sure your password contains both Upper and Lowercase Letters!", "Try Again!");

                        Pass.Text = null;
                        Pass2.Text = null;
                    }
                    else if (Pass.Text.Length <= 7)
                    {

                        DisplayAlert("Error!", "Make sure your password is at least 8 characters!", "Try Again!");

                        Pass.Text = null;
                        Pass2.Text = null;
                    }


                    else
                    {

                        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");
                        db = new SQLiteConnection(databasePath);
                        db.CreateTable<RegUser>();
                        db.Table<RegUser>().ToList();
                        var thelist = db.Table<RegUser>()
                        .Where(v => v.UserName
                == InputUser.Text).FirstOrDefault();
                        thelist.Password = Pass.Text;
                        db.Update(thelist);


                        DisplayAlert("Success", "Password Successfully Changed!", "ok");



                        App.Current.MainPage = new NavigationPage(new LoginPage());

                    }
                }
            }
        }
    }
}
