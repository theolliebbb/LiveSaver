using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LiveSaves.Views;
[assembly: ExportFont("Azonix-1VB0.otf", Alias = "Fonty")]
namespace LiveSaves
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
