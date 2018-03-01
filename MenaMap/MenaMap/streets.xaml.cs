using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using MenaWebAPI.Models;

namespace MenaMap
{    
    public partial class streets : ContentPage
    {       
       
    public streets()
        {
            InitializeComponent();
            LoadData();
           btmap.Clicked += Btmap_Clicked;
        }

       private void Btmap_Clicked(object sender, EventArgs e)
       {
            // lbl.Text = myStreet.Items[myStreet.SelectedIndex];
            Navigation.PushAsync(new StreetMap(myStreet.Items[myStreet.SelectedIndex]));
            //   Navigation.PushAsync(new StreetMap(myStreet.Items[myStreet.SelectedIndex]));
        }

        public async void LoadData()
        {
            using (var client = new HttpClient())
            {
                var baseUri = "http://ws.eskanmena.info/api/streets";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<List<MenaStreets>>(responseJson);
                    myStreet.ItemsSource = Items;
                    
                }
            }

        }
    }
}


     
     

