
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
using Windows.System.Profile;

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
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                fillTrainings();
            }
            else
            {
                fillTrainingsMobile();
            }
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
                foreach (Models.Training t in root.Trainingen)
                {
                    if (trainings.Any(training => training.Name.Equals(t.Name)))
                    {
                        //t.Campussen.Add(root);
                        var temp = trainings.Where(training => training.Name.Equals(t.Name));
                        foreach (TrainingWithImage tr in temp)
                        {
                            tr.Campussen.Add(root.Name);
                        }
                    }
                    else
                    {
                        TrainingWithImage twi = new TrainingWithImage(t);
                        twi.Campussen.Add(root.Name);
                        trainings.Add(twi);
                    }
                }
            }

            listViewTrainingsGent.ItemsSource = trainings;
        }

        private void fillTrainingsMobile()
        {
            Campus schoonmeersen = new Campus() { CampusId = 1, Name = "HoGent Schoonmeersen", City = "Gent", Street = "Valentin Vaerwyckweg", HouseNumber = "1", Telephone = "09 243 35 60", Feed = "Hogeschool-Gent-Campus-Schoonmeersen" };
            Campus aalst = new Campus() { CampusId = 2, Name = "HoGent Aalst", City = "Aalst", Street = "Arbeidstraat", HouseNumber = "14", Telephone = "09 243 38 00", Feed = "HoGentCampusAalst" };


            trainings = new ObservableCollection<TrainingWithImage>();

            TrainingWithImage twi = new TrainingWithImage(new Training() { TrainingId = 1, Name = "Toegepaste Informatica", Description = "Computerrichting, hier start de leerling volledig vanaf de basis. Daarna heeft de leerling keuze om netwerken/programmeren te doen.", Feed = "hogenttoegepasteinformatica" });
            twi.Campussen.Add(schoonmeersen.Name);
            twi.Campussen.Add(aalst.Name);

            TrainingWithImage twi2 = new TrainingWithImage(new Training() { TrainingId = 2, Name = "Bedrijfsmanagement", Description = "Praktijk georienteerde richting over het managen van een bedrijf.", Feed = "hogentbedrijfsmanagement" });
            twi2.Campussen.Add(schoonmeersen.Name);
            twi2.Campussen.Add(aalst.Name);

            TrainingWithImage twi3 = new TrainingWithImage(new Training() { TrainingId = 3, Name = "Retail management", Description = "Voorbereiding op de job van strategisch manager in de detailhandel.", Feed = "hogentretailmanagement" });
            twi3.Campussen.Add(schoonmeersen.Name);

            TrainingWithImage twi4 = new TrainingWithImage(new Training() { TrainingId = 4, Name = "Office management", Description = "Voorbereidende richting op het organiserende en coordinerende aspect in bedrijven.", Feed = "hogentofficemanagement" });
            twi4.Campussen.Add(schoonmeersen.Name);
            twi4.Campussen.Add(aalst.Name);

            trainings.Add(twi);
            trainings.Add(twi2);
            trainings.Add(twi3);
            trainings.Add(twi4);

            listViewTrainingsGent.ItemsSource = trainings;
        }

        private void listViewHoGentItem_Click(object sender, ItemClickEventArgs e)
        {
            TrainingWithImage selectedTraining = (TrainingWithImage)e.ClickedItem;

            chosenTraining.Text = selectedTraining.Name;
            campussesSentence.Visibility = Visibility.Visible;
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
