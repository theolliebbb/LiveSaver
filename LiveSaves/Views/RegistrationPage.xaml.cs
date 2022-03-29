using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using SQLite;
using SQLitePCL;

using LiveSaves.Models;
using Xamarin.Essentials;

namespace LiveSaves.Views
{
    public partial class RegistrationPage : ContentPage
    {
        static SQLiteConnection db;
        public RegistrationPage()
        {
            InitializeComponent();
            if (db != null)
                return;


            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");

            db = new SQLiteConnection(databasePath);

            db.CreateTable<RegUser>();
        }

        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateUser(string user)
        {
           
            var results = db.Table<RegUser>().Where(v => v.UserName == user).ToList();

            return (results.Count > 0);
        }

        public bool ValidateEmail(string email)
        {
            
            var results = db.Table<RegUser>().Where(v => v.Email == email).ToList();

            return (results.Count > 0);
        }


        void Register_Clicked(object sender, System.EventArgs e)
        {
            
            
            if (Pass.Text != null)
            {
                var lowerPass = Pass.Text.ToLower();
                var upperPass = Pass.Text.ToUpper();

                {
                    if (Pass.Text != Pass2.Text)
                    {
                        BadPass.IsVisible = false;
                        NoUser.IsVisible = false;
                        NoEmail.IsVisible = false;
                        NoPass.IsVisible = false;
                        NoPass2.IsVisible = false;
                        DisplayAlert("Error!", "Your Passwords Do Not Match!!", "Try Again!");
                        Pass.Text = null;
                        Pass2.Text = null;
                    }
                    else if (UserName.Text == null || Pass.Text == null || Pass2.Text == null || Email.Text == null)
                    {
                        BadPass.IsVisible = false;
                        NoUser.IsVisible = false;
                        NoEmail.IsVisible = false;
                        NoPass.IsVisible = false;
                        NoPass2.IsVisible = false;
                        DisplayAlert("Error!", "Please Input All Fields!", "Try Again!");
                        
                    }
                    else if (UserName.Text == Pass.Text)
                    {
                        BadPass.IsVisible = false;
                        NoUser.IsVisible = false;
                        NoEmail.IsVisible = false;
                        NoPass.IsVisible = false;
                        NoPass2.IsVisible = false;
                        DisplayAlert("Error!", "UserName and Password cannot be identical!", "Try Again!");
                        UserName.Text = null;
                        Pass.Text = null;
                        Pass2.Text = null;
                    }

                    else if (IsValidEmail(Email.Text) == false)
                    {
                        BadPass.IsVisible = false;
                        NoUser.IsVisible = false;
                        NoEmail.IsVisible = false;
                        NoPass.IsVisible = false;
                        NoPass2.IsVisible = false;
                        DisplayAlert("Error!", "Please Input Valid Email!", "Try Again!");
                        NoEmail.IsVisible = true;
                        Email.Text = null;
                    }


                    else if (lowerPass == Pass.Text || upperPass == Pass.Text)
                    {
                        BadPass.IsVisible = false;
                        NoUser.IsVisible = false;
                        NoEmail.IsVisible = false;
                        NoPass.IsVisible = false;
                        NoPass2.IsVisible = false;
                        DisplayAlert("Error!", "Make sure your password contains both Upper and Lowercase Letters!", "Try Again!");
                        BadPass.IsVisible = true;
                        Pass.Text = null;
                        Pass2.Text = null;
                    }
                    else if (Pass.Text.Length <= 7)
                    {
                        BadPass.IsVisible = false;
                        NoUser.IsVisible = false;
                        NoEmail.IsVisible = false;
                        NoPass.IsVisible = false;
                        NoPass2.IsVisible = false;
                        DisplayAlert("Error!", "Make sure your password is at least 8 characters!", "Try Again!");
                        BadPass.IsVisible = true;
                        Pass.Text = null;
                        Pass2.Text = null;
                    }
                    else if (ValidateUser(UserName.Text) == true)
                    {
                        BadPass.IsVisible = false;
                        NoUser.IsVisible = false;
                        NoEmail.IsVisible = false;
                        NoPass.IsVisible = false;
                        NoPass2.IsVisible = false;
                        DisplayAlert("Error!", "User Name already in use!", "Try Again!");
                    }
                    else if (ValidateEmail(Email.Text) == true)
                    {
                        BadPass.IsVisible = false;
                        NoUser.IsVisible = false;
                        NoEmail.IsVisible = false;
                        NoPass.IsVisible = false;
                        NoPass2.IsVisible = false;
                        DisplayAlert("Error!", "Email already in use!", "Try Again!");
                    }
                    else
                    {

                        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "UserDB.db3");
                        db = new SQLiteConnection(databasePath);
                        db.CreateTable<RegUser>();

                        var item = new RegUser()
                        {
                            UserName = UserName.Text,
                            Email = Email.Text,
                            Password = Pass.Text,
                        };

                        db.Insert(item);

                        DisplayAlert("Success", "User Successfully Registered", "ok");



                        App.Current.MainPage = new NavigationPage(new LoginPage());
                    };
                }


            }
            else
            {
                DisplayAlert("Error!", "Please Input All Fields!", "Try Again!");
            }
        }

        private void insertorupdate()
        {
            throw new NotImplementedException();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}