using Parks.Interfaces;
using System;
using System.Reflection;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parks.JSON;
using Newtonsoft.Json;

namespace Parks
{
    public partial class AboutPage : ContentPage
    {


        public AboutPage()
        {
            InitializeComponent();
            mapVisualizacao.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                            new Xamarin.Forms.Maps.Position(39.833, -98.583),
                            Distance.FromMeters(1500000.0)
                    )
            );


        }

        override
        protected void OnAppearing()
        {

            mapVisualizacao.Pins.Clear();

            adicionarMarcadores();
        }



        private async void adicionarMarcadores()
        {
            var items = await GetParksAsync();
            foreach (var item in items)
            {
                var pin = new MyPin()
                {
                    Position = new Position(item.Latitude, item.Longitude),
                    Label = item.Name,
                    Park = item
                };

                mapVisualizacao.Pins.Add(pin);
                pin.Clicked += Clicked_Marker;

            }


        }

        public void Clicked_Marker(object sender, EventArgs e)
        {
            var p = sender as MyPin;
            mostrarDetalhes(p.Park);
            //DisplayAlert("Tapped!", "Pin was tapped.", "OK");
        }

        private async void mostrarDetalhes(Park park)
        {

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(park)));
        }

        public class MyPin : Pin
        {
            public Park Park { get; set; }
        }
        private async Task<IEnumerable<Park>> GetParksAsync()
        {
            var assembly = typeof(AboutPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Parks.Assets.data.data.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            List<Park> parks = JsonConvert.DeserializeObject<List<Park>>(text);
            return parks;

        }
    }


}
