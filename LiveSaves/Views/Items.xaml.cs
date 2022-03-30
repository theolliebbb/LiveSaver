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
        
        List<Live> live;
        static SQLiteConnection db;
        public ObservableRangeCollection<Live> Live { get; set; }

        public Items()
        {
            InitializeComponent();
            Init();
          
        }

        protected override void OnAppearing()
        {
            if (BindingContext is UserLiveViewModel vm)
            {
                vm.Refresh();
            }
        }

        private async void EditEvent(object sender, ItemTappedEventArgs e)
        {



            var details = e.Item as Live;


            await Navigation.PushAsync(new EditPage(details));
            
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
