using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
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
        private List<RootObject> result;
        public MyPreferences()
        {
            this.InitializeComponent();

            fillCheckboxes();   
        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void verzendGegevens_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            List<string> trainings = new List<string>();
            List<string> campussen = new List<string>();
            List<Models.Training> trainingsPost = new List<Models.Training>();
            List<Campus> campussenPost = new List<Campus>();
            if (listbox != null)
            {

                foreach (CheckBox c in this.listbox.Items)
                {
                    if ((bool)c.IsChecked)
                    {
                        string x = (string)c.Content;
                        string[] y = x.Split('-');
                        if (!trainings.Contains(y[1]))
                        {
                            trainings.Add(y[1].Trim());
                        }
                        if (!campussen.Contains(y[0]))
                        {
                            campussen.Add(y[0].Trim());
                        }
                    }
                }
            }
            foreach (RootObject r in result)
            {
                if(campussen.Contains(r.Name.Trim()))
                {
                    /*OMZETTEN NAAR CAMPUS*/
                    Campus c = new Models.Campus();
                    c.CampusId = r.CampusId;
                    c.City = r.City;
                    c.Feed = r.Feed;
                    c.HouseNumber = r.HouseNumber;
                    c.Name = r.Name;
                    c.Street = r.Street;
                    c.Telephone = r.Telephone;
                    c.Trainingen = r.Trainingen;
                    campussenPost.Add(c);
                }
                foreach (Models.Training t in r.Trainingen)
                {
                    if (trainings.Contains(t.Name.Trim()))
                    {
                        Models.Training test = trainingsPost.Where(p => p.Name == t.Name).FirstOrDefault();
                        if (test == null)
                        {
                            trainingsPost.Add(t);
                        }
                    }
                }
            }
            Student nieuw = new Student();
            nieuw.Province = (string) Province.SelectionBoxItem;
            nieuw.Name = firstname.Text + lastname.Text;
            nieuw.City = city.Text;
            nieuw.Street = street.Text;
            nieuw.HouseNumber = houseNumber.Text;
            nieuw.PrefTraining = trainingsPost;
            nieuw.PrefCampus = campussenPost;
            nieuw.Email = email.Text;
            string body = JsonConvert.SerializeObject(nieuw);
            var result2 = await client.PostAsync("http://localhost:50103/api/Students", new StringContent(body, Encoding.UTF8, "application/json"));
              
        }

        private async void fillCheckboxes()
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync("http://localhost:50103/api/Campus");
            result = JsonConvert.DeserializeObject<List<RootObject>>(json);

            foreach(RootObject root in result)
            {
                foreach(Models.Training t in root.Trainingen)
                {
                    CheckBox cb = new CheckBox();
                    cb.Content = root.Name + " - " + t.Name;
                    checkboxList.Add(cb);
                }         
            }
            if(checkComboBox != null)
                checkComboBox.ItemsSource = checkboxList;
            if(listbox != null)
                listbox.ItemsSource = checkboxList;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox x = (CheckBox)sender;
            x = checkboxList.Find(y => y.Content == x.Content);
            if ((bool)x.IsChecked)
            {
                x.IsChecked = false;
            }
            else
            {
                x.IsChecked = true;
            }
        }
    }
}
