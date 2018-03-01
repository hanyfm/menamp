using MenaWebAPI.Models;
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
    public partial class company : ContentPage
    {
        public List<metawif> xx;
        public company()
        {
            
            InitializeComponent();
            LoadData();            
            mySearchbar.TextChanged += MySearchbar_TextChanged;
            btmap.Clicked += Btmap_Clicked;
  
        }

        private void MySearchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string seacrtext = mySearchbar.Text;
            if (seacrtext.Length > 0)
            { myStreet.ItemsSource = xx.Where(c => c.company.Contains(seacrtext)).ToList();
                myStreet.Focus();
            }
            else
                myStreet.ItemsSource = xx;


                     
        }

        

        private void Btmap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CompanyMap(myStreet.Items[myStreet.SelectedIndex]));
        }

        public async void LoadData()
        {
            using (var client = new HttpClient())
            {
                var baseUri = "http://ws.eskanmena.info/api/company";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<List<metawif>>(responseJson);
                    myStreet.ItemsSource = Items;
                    xx = Items;
                    


                }
            }

        }
    }

  
}
