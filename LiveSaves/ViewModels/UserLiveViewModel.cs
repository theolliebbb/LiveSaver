using MvvmHelpers;
using MvvmHelpers.Commands;
using LiveSaves.Models;
using LiveSaves.Services;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;
using LiveSaves;

using System.Windows.Input;
using LiveSaves.Views;
using Xamarin.Essentials;
using System.IO;
using SQLite;
using System;

namespace LiveSaves.ViewModels
{
    public class UserLiveViewModel 
    {
        public static string photofilename;
        public static string imagePath;
        public ObservableRangeCollection<Live> Live { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand PhotoCommand { get; }
        public AsyncCommand AddCommand { get; }
        static SQLiteConnection db;
        public AsyncCommand ClearCommand { get; }
        public AsyncCommand LogoutCommand { get; }
        public AsyncCommand<Live> RemoveCommand { get; }
        public string Welcome = $"Welcome, {LoginPage.User}";
        public Live SelectedLive { get; set; }
        public ICommand MapCommand { get; private set; }
        public bool IsRefreshing { get; private set; }

        public UserLiveViewModel()
        {


            
            Live = new ObservableRangeCollection<Live>();

            ClearCommand = new AsyncCommand(Clear);
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Live>(Remove);
            LogoutCommand = new AsyncCommand(LogoutC);
            
        }

        async Task Add()
        {
            if (db != null)
                return;
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
            var band = await App.Current.MainPage.DisplayPromptAsync("Band", "Name of Band");
            var date = await App.Current.MainPage.DisplayPromptAsync("Date", "Date");
            var venue = await App.Current.MainPage.DisplayPromptAsync("Venue", "Name of Venue");
            var image = imagePath;

            await LiveService.AddLive(band, date, venue, image);
            await Refresh();
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


        async Task Clear()
        {
            await LiveService.ClearLive();
            await Refresh();
        }
        async Task Remove(Live live)
        {
            await LiveService.RemoveLive(live.Id);
            await Refresh();
        }

        /*async Task GoToMap()
        {
            await 
        }*/
        public async Task Refresh()
        {

            IsRefreshing = true;

            await Task.Delay(200);

            Live.Clear();

            var lives = await LiveService.GetLive();

            Live.AddRange(lives);

            IsRefreshing = false;

        }
        public async Task LogoutC()
        {
            LiveService.Logout();
        }

    }
}