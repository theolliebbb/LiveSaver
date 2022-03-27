using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LiveSaves.Models;
using Xamarin.Essentials;
using System.IO;
using LiveSaves.ViewModels;
using LiveSaves.Services;
namespace LiveSaves.Views
{
    public partial class Items : ContentPage
    {


        public Items()
        {
            InitializeComponent();
            Init();
        }
        

        public async void Init()
        {
           await Services.LiveService.GetLive();
            
        }
        
        async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var buttonClickHandler = (Button)sender;


            StackLayout ParentStackLayout = (StackLayout)buttonClickHandler.Parent;

            Image ImageLabel = (Image)ParentStackLayout.Children[0];
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                LoadPhotoAsync(photo);
                ImageLabel.Source = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

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
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }

        async void Venue_Clicked(System.Object sender, System.EventArgs e)
        {
            var buttonClickHandler = (Button)sender;


            StackLayout ParentStackLayout = (StackLayout)buttonClickHandler.Parent;

            Button VenueLabel = (Button)ParentStackLayout.Children[4];

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

        void Image_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
