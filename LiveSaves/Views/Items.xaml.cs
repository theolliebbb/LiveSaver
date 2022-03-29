using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MvvmHelpers;
using LiveSaves.Services;
using MvvmHelpers.Commands;
using LiveSaves.ViewModels;

namespace LiveSaves.Views
{
    public partial class Items : ContentPage
    {
        
        
        static SQLiteConnection db;
        
      
        public Items()
        {
            InitializeComponent();
            Init();
        
        }
        

        public async void Init()
        {
            
            await Services.LiveService.GetLive();
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Lives{UserDisplayName.displayName}.db");

            db = new SQLiteConnection(databasePath);
            
        }
        
       

        async void Venue_Clicked(System.Object sender, System.EventArgs e)
        {
            var buttonClickHandler = (Button)sender;


            StackLayout ParentStackLayout = (StackLayout)buttonClickHandler.Parent;

            Button VenueLabel = (Button)ParentStackLayout.Children[3];

            var placemark = new Placemark
            {

                Thoroughfare = VenueLabel.Text
              
            };
            var options = new MapLaunchOptions { Name = VenueLabel.Text };

            try
            {
                await Map.OpenAsync(placemark, options);
            }
            catch (Exception ex)
            {
                // No map application available to open or placemark can not be located
            }

        }


    }
}
