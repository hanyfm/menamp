using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MenaMap
{
    public partial class SitestMap : ContentPage
    {
        public class siteMap
        {
            public string old_site_code { get; set; }
            public string section { get; set; }
            public Nullable<int> area { get; set; }
            public Nullable<double> latitude { get; set; }
            public Nullable<double> Longitude { get; set; }
            public Nullable<double> latitude2 { get; set; }
            public Nullable<double> Longitude2 { get; set; }
            public Nullable<double> latitude1 { get; set; }
            public Nullable<double> Longitude1 { get; set; }
            public Nullable<double> latitude3 { get; set; }
            public Nullable<double> Longitude3 { get; set; }
            public Nullable<double> latitude4 { get; set; }
            public Nullable<double> Longitude4 { get; set; }
        }

        public SitestMap (string xstreet)
        {
            InitializeComponent();
            LoadData(xstreet);
            
            MainMap.MoveToRegion(
                    MapSpan.FromCenterAndRadius(new Position(21.413376, 39.882343), Distance.FromMiles(1)));            
        }

        public async void LoadData(string xstreet)
        {
            using (var client = new HttpClient())
            {
                var baseUri = "http://ws.eskanmena.info/api/sites";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<List<siteMap>>(responseJson);
                  
                    MainMap.MoveToRegion(
                                        MapSpan.FromCenterAndRadius(new Position(21.413376, 39.882343), Distance.FromMiles(1)));
                    foreach (var item in Items)
                    {
                        if (item.old_site_code == xstreet)
                        {
                            var position = new Position(Convert.ToDouble(item.Longitude), Convert.ToDouble(item.latitude)); // Latitude, Longitude
                            var pin = new Pin
                            {
                                //Type = PinType.Place,
                                Type = PinType.Generic,
                                Position = position,
                                Label = item.old_site_code,
                                Address = "المطوف =" + item.old_site_code.ToString()
                            };
                            MainMap.Pins.Add(pin);
                        }
                    }


                }


            }
        }
    }
}
