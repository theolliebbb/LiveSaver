using System;
using System.Collections.Generic;
using LiveSaves.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LiveSaves.Views;
using SQLite;
using System.IO;
using Xamarin.Essentials;
using ISQLites;
using System.Threading.Tasks;

namespace LiveSaves.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPage : ContentPage
    {
        public static string photofilename;
        public static string imagePath;
        static SQLiteConnection db;
        Live liveDetails = new Live();
        public NewPage()
        {
            InitializeComponent();
            
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Lives{UserDisplayName.displayName}.db");

                db = new SQLiteConnection(databasePath);
                
                
            }
        }



        private void SaveEvent(object sender, EventArgs e)
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"Lives{UserDisplayName.displayName}.db");

            db = new SQLiteConnection(databasePath);



            var dater = date.Date.ToString("dddd, dd MMMM yyyy");
            liveDetails.Band = band.Text;
            liveDetails.Date = dater;
            liveDetails.Venue = venue.Text;
            liveDetails.Image = image.Text;
            db.Insert(liveDetails);
            App.Current.MainPage = new NavigationPage(new Items());



        }
   
        async void Image_ClickedAsync(System.Object sender, System.EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                LoadPhotoAsync(photo);
                photofilename = photo.FileName;
                imagePath = Path.Combine(FileSystem.AppDataDirectory, photofilename);


            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
            image.Text = imagePath;
            ImageViewer.Source = imagePath;
            ImageViewer.IsVisible = true;

        }
        async void LoadPhotoAsync(FileResult photo)
        {
            // canceled
            string PhotoPath;
            if (photo == null)
            {

                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;

        }
    }
}