
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WindowsClient.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrainingChoice : Page
    {
        ObservableCollection<WindowsClient.Models.Training> trainingsGent;

        public TrainingChoice()
        {
            this.InitializeComponent();           

            fillTrainings();            
        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void fillTrainings()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync("http://localhost:50103/api/Campus");
            var result = JsonConvert.DeserializeObject<List<RootObject>>(json);

            trainingsGent = new ObservableCollection<Models.Training>();
            int index = 0;

            foreach (RootObject root in result)
            {

                //trainingsGent.Add(t);
                foreach(Models.Training t in root.Trainingen)
                {
                 
                    t.Campussen.Add(root.Name);

                }

                
            }
            
            listViewTrainingsGent.ItemsSource = trainingsGent;
        }

        private async void listViewHoGentItem_Click(object sender, ItemClickEventArgs e)
        {
            Models.Training selectedTraining = (Models.Training)e.ClickedItem;
            var dialog = new Windows.UI.Popups.MessageDialog("U klikte op " + selectedTraining.Name);

            await dialog.ShowAsync();
        }
    }
}
