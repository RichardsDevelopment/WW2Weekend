using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WW2Weekend
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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

        }
    }
}
