
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
using System.Text.RegularExpressions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrainingChoice : Page
    {
        //ObservableCollection<WindowsClient.Models.Training> trainings;
        ObservableCollection<TrainingWithImage> trainings;

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

            trainings = new ObservableCollection<TrainingWithImage>();

            foreach (RootObject root in result)
            {
                foreach(Models.Training t in root.Trainingen)
                {
                    if(trainings.Any(training => training.Name.Equals(t.Name)))
                    {
                        //t.Campussen.Add(root);
                        var temp = trainings.Where(training => training.Name.Equals(t.Name));
                        foreach(TrainingWithImage tr in temp)
                        {
                            tr.Campussen.Add(root.Name);
                        }
                    }
                    else
                    {
                        t.Campussen.Add(root.Name);
                        trainings.Add(new TrainingWithImage(t));
                    }
                }                
            }
            
            listViewTrainingsGent.ItemsSource = trainings;
        }

        private void listViewHoGentItem_Click(object sender, ItemClickEventArgs e)
        {
            TrainingWithImage selectedTraining = (TrainingWithImage) e.ClickedItem;

            chosenTraining.Text = selectedTraining.Name;
            descriptionOfTraining.Text = selectedTraining.Description;
            CampussesOfTraining.Text = String.Join(", ", selectedTraining.Campussen);

            //var dialog = new Windows.UI.Popups.MessageDialog("U klikte op " + selectedTraining.Name + "\nDeze richting kan u volgen in: " + String.Join(", ", selectedTraining.Campussen));
            //await dialog.ShowAsync();
        }

        public class TrainingWithImage
        {
            public int TrainingId { get; set; }
            public string Name { get; set; }
            public string stringCB { get; set; }
            public string Description { get; set; }
            public List<string> Campussen { get; set; }
            public IList<int> CampusId { get; set; }
            public string Feed { get; set; }
            public virtual IList<Student> Studenten { get; set; }
            public IList<int> StudentId { get; set; }
            public String Image { get; set; }

            public TrainingWithImage(Training training)
            {
                Name = training.Name;
                Description = training.Description;
                Feed = training.Feed;
                Campussen = new List<string>();
                Studenten = new List<Student>();
                CampusId = new List<int>();
                StudentId = new List<int>();

                Regex initials = new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?");
                string init = initials.Replace(training.Name, "$1").ToUpper();
                Image = "ms-appx:///Images/" + init + ".jpg";
            }
        }
    }
}
