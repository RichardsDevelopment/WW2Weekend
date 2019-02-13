using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WW2Weekend
{
    public partial class MainPage : ContentPage
    {
        string url = "https://mwmuseum.com/news/";

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Map_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }
        private async void Events_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventsPage());
        }
        private void Donate_Clicked(object sender, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri("https://mwmuseum.com/donate/"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
