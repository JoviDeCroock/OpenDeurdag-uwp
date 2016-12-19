using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsClient.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyPreferences : Page
    {
        private List<CheckBox> checkboxList = new List<CheckBox>();
        private List<Models.Training> trainings = new List<Models.Training>();

        public MyPreferences()
        {
            this.InitializeComponent();

            fillCheckboxes();   
        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void verzendGegevens_Click(object sender, RoutedEventArgs e)
        {
            List<Models.Training> trainings = this.checkComboBox.SelectedItems;
        }

        private async void fillCheckboxes()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync("http://localhost:50103/api/Campus");
            var result = JsonConvert.DeserializeObject<List<RootObject>>(json);



            foreach(RootObject root in result)
            {
                foreach(Models.Training t in root.Trainingen)
                {
                    CheckBox cb = new CheckBox();
                    cb.Content = root.Name + " - " + t.Name;
                    checkboxList.Add(cb);
                }     
                foreach(Models.Training t in root.Trainingen)
                {
                    t.Campussen.Add(root.Name);
                    t.stringCB = root.Name + " - " + t.Name;
                    trainings.Add(t);
                }           
            }

            checkComboBox.ItemsSource = checkboxList;
            listbox.ItemsSource = checkboxList;
        }

        private void test(object sender, RoutedEventArgs e)
        {
            string x = "";
        }
    }

}
