
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
        ObservableCollection<WindowsClient.Models.Training> trainings;

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

            trainings = new ObservableCollection<Models.Training>();

            foreach (RootObject root in result)
            {
                foreach(Models.Training t in root.Trainingen)
                {
                    if(trainings.Any(training => training.Name.Equals(t.Name)))
                    {
                        //t.Campussen.Add(root);
                        var temp = trainings.Where(training => training.Name.Equals(t.Name));
                        foreach(Models.Training tr in temp)
                        {
                            tr.Campussen.Add(root.Name);
                        }
                    }
                    else
                    {
                        t.Campussen.Add(root.Name);
                        trainings.Add(t);
                    }
                }                
            }
            
            listViewTrainingsGent.ItemsSource = trainings;
        }

        private void listViewHoGentItem_Click(object sender, ItemClickEventArgs e)
        {
            Models.Training selectedTraining = (Models.Training)e.ClickedItem;

            chosenTraining.Text = selectedTraining.Name;
            descriptionOfTraining.Text = selectedTraining.Description;
            CampussesOfTraining.Text = String.Join(", ", selectedTraining.Campussen);
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);

            //var dialog = new Windows.UI.Popups.MessageDialog("U klikte op " + selectedTraining.Name + "\nDeze richting kan u volgen in: " + String.Join(", ", selectedTraining.Campussen));
            //await dialog.ShowAsync();
        }
    }
}
