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
    public class camps
    {
        public string old_site_code { get; set; }
        public string company { get; set; }
        public string street { get; set; }
        public double? latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string shakes { get; set; }
        public int ID { get; set; }

    }
  
    public partial class camp : ContentPage
    {
       
        List<camps> xsiteMap; 
        public camp()
        {
            InitializeComponent();
            LoadData();
            mySearchbar.TextChanged += MySearchbar_TextChanged;
            btmap.Clicked += Btmap_Clicked;

        }

        private void Btmap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new campMap(mylist.SelectedItem.ToString()));
        }

        private void MySearchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string seacrtext = mySearchbar.Text.ToUpper();
            if (seacrtext.Length > 0)
            {
                mylist.ItemsSource = xsiteMap.Where(c => c.shakes.Contains(seacrtext)).Select(c => c.shakes).ToList();

            }
            else
                mylist.ItemsSource = xsiteMap.Select(c => c.shakes);

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
                    xsiteMap = Items;
                    mylist.ItemsSource = Items.Select(c => c.shakes);

                }
            }

        }
    }




}
