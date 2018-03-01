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
    public partial class MainPage : ContentPage
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

        public List<camps> site_data;
        public MainPage()
        {
            InitializeComponent();
            LoadData();



          

            MainMap.MoveToRegion(
                    MapSpan.FromCenterAndRadius(new Position(21.413376, 39.882343), Distance.FromMiles(1)));
            /*
            foreach (var item in site_data)
            {
                var position = new Position(Convert.ToDouble(item.latitude), Convert.ToDouble(item.Longitude)); // Latitude, Longitude
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = "custom pin",
                    Address = "custom detail info"
                };
                MainMap.Pins.Add(pin);
            }*/
        }

        public async void LoadData()
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
                    site_data = Items;
                    MainMap.MoveToRegion(
                                        MapSpan.FromCenterAndRadius(new Position(21.413376, 39.882343), Distance.FromMiles(1)));
                    foreach (var item in site_data)
                    {
                        var position = new Position(Convert.ToDouble(item.Longitude), Convert.ToDouble(item.latitude)); // Latitude, Longitude
                        var pin = new Pin
                        {
                            //Type = PinType.Place,
                            Type = PinType.Generic,
                            Position = position,
                            Label = item.old_site_code,                           
                            Address = "مساحة الموقع =" + item.company.ToString()
                        };
                        MainMap.Pins.Add(pin);
                    }
                    

                }


            }



        }
    }
}
