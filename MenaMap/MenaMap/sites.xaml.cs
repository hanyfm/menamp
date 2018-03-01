using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MenaMap
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sites : ContentPage
    {
        public class siteList
        {
            public string old_site_code { get; set; }
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
 
        List<siteMap> xsiteMap;
        public sites()
        {
            InitializeComponent();
            LoadData();
            mySearchbar.TextChanged += MySearchbar_TextChanged;
            btmap.Clicked += Btmap_Clicked;

        }

        private void Btmap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SitestMap(mylist.SelectedItem.ToString()));
        }

        private void MySearchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string seacrtext = mySearchbar.Text.ToUpper();
            if (seacrtext.Length > 0)
            {
                mylist.ItemsSource = xsiteMap.Where(c => c.old_site_code.Contains(seacrtext)).Select(c => c.old_site_code).ToList();

            }
            else
                mylist.ItemsSource = xsiteMap.Select(c => c.old_site_code);

        }

        public async void LoadData()
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
                    xsiteMap = Items;
                    mylist.ItemsSource = Items.Select(c => c.old_site_code);

                }
            }

        }
    }




}
