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

namespace LiveSaves.ViewModels
{
    public class UserLiveViewModel 
    {
        public ObservableRangeCollection<Live> Live { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand ClearCommand { get; }
        public AsyncCommand<Live> RemoveCommand { get; }

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
        }

        async Task Add()
        {
            var band = await App.Current.MainPage.DisplayPromptAsync("Band", "Name of Band");
            var date = await App.Current.MainPage.DisplayPromptAsync("Date", "Date");
            var venue = await App.Current.MainPage.DisplayPromptAsync("Venue", "Name of Venue");
            var image = "plus.jpg";
            await LiveService.AddLive(band, date, venue, image);
            await Refresh();
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
    }
}