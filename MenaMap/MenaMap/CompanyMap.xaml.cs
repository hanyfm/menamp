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
    public partial class CompanyMap : ContentPage
    {
        public class camps
        {
            public string old_site_code { get; set; }
            public string company { get; set; }
            public string street { get; set; }
            public double? latitude { get; set; }
            public Nullable<double> Longitude { get; set; }
            public int ID { get; set; }

        }
      
        public CompanyMap(string xstreet)
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
                var baseUri = "http://ws.eskanmena.info/api/camps";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<List<camps>>(responseJson);
                  
                    MainMap.MoveToRegion(
                                        MapSpan.FromCenterAndRadius(new Position(21.413376, 39.882343), Distance.FromMiles(1)));
                    foreach (var item in Items)
                    {
                        if (item.company == xstreet)
                        {
                            var position = new Position(Convert.ToDouble(item.Longitude), Convert.ToDouble(item.latitude)); // Latitude, Longitude
                            var pin = new Pin
                            {
                                //Type = PinType.Place,
                                Type = PinType.Generic,
                                Position = position,
                                Label = item.old_site_code,
                                Address = "الشاخص =" + item.street.ToString()
                            };
                            MainMap.Pins.Add(pin);
                        }
                    }


                }


            }
        }
    }
}
