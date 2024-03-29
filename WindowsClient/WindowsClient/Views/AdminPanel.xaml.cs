﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class AdminPanel : Page
    {
        private List<Student> provincie = new List<Student>();
        private List<CampusGegevens> campusses = new List<CampusGegevens>();
        private List<Student> studenten;
        private List<Post> posts;

        HttpClient client = new HttpClient();
        public AdminPanel()
        {
            this.InitializeComponent();
            GetStudents();
            GetPosts();
        }

        public async void GetStudents()
        {
            studenten = new List<Student>();
            HttpClient client = new HttpClient();
            string jsonStudents = await client.GetStringAsync("http://localhost:50103/api/Students");

            var result = JsonConvert.DeserializeObject<List<Student>>(jsonStudents);
            foreach (Student p in result)
            {
                studenten.Add(p);
            }
            string jsonCampusses = await client.GetStringAsync("http://localhost:50103/api/Campus");
            Dictionary<string, int> aantalPerTrainingGent = new Dictionary<string, int>();
            Dictionary<string, int> aantalPerTrainingAalst = new Dictionary<string, int>();

            aantalPerTrainingGent.Add("Toegepaste Informatica", 0);
            aantalPerTrainingGent.Add("Retail management", 0);
            aantalPerTrainingGent.Add("Office management", 0);
            aantalPerTrainingGent.Add("Bedrijfsmanagement", 0);
            aantalPerTrainingAalst.Add("Toegepaste Informatica", 0);
            aantalPerTrainingAalst.Add("Retail management", 0);
            aantalPerTrainingAalst.Add("Office management", 0);
            aantalPerTrainingAalst.Add("Bedrijfsmanagement", 0);

            var result2 = JsonConvert.DeserializeObject<List<Campus>>(jsonCampusses);
            List<string> testPerTraining = new List<string>();
            CampusGegevens temp = new CampusGegevens();
            temp.campussen = new List<string>();
            foreach (Campus c in result2)
            {                
                temp.campussen.Add(c.Name);
                temp.aantalStuds = new List<string>();
                temp.trainings = c.Trainingen;
                foreach (Training t in temp.trainings)
                {
                    foreach (Student st in studenten)
                    {
                        if (st.PrefCampus.Where(ca => ca.Name == c.Name).FirstOrDefault() != null)
                        {
                            foreach (Training xd in st.PrefTraining)
                            {
                                if (xd.Name == t.Name)
                                {
                                    if (c.Name == "HoGent Schoonmeersen")
                                    {
                                        aantalPerTrainingGent[t.Name]++;
                                    }
                                    else
                                    {
                                        aantalPerTrainingAalst[t.Name]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            List<GegevensHelper> GegevensGent = new List<GegevensHelper>();
            List<GegevensHelper> GegevensAalst = new List<GegevensHelper>();
            foreach (string training in aantalPerTrainingGent.Keys)
            {
                GegevensGent.Add(new GegevensHelper() { Aantal = aantalPerTrainingGent[training], Richting = training +"\t" });
            }
            foreach (string training in aantalPerTrainingAalst.Keys)
            {
                GegevensAalst.Add(new GegevensHelper() { Aantal = aantalPerTrainingAalst[training], Richting = training + "\t" });
            }

            ListGent.ItemsSource = GegevensGent;
            ListAalst.ItemsSource = GegevensAalst;
        }

        public async void GetPosts()
        {
            posts = new List<Post>();
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync("http://localhost:50103/api/Posts");
            var result = JsonConvert.DeserializeObject<List<Post>>(json);
            foreach (Post p in result)
            {
                posts.Add(p);
            }
            listViewPosts.ItemsSource = posts;
        }

        public async void DeletePost(int id)
        {
            HttpClient client = new HttpClient();
            try
            {
                var result = await client.GetStringAsync("http://localhost:50103/api/Posts/" + id);
                await client.DeleteAsync("http://localhost:50103/api/Posts/" + id);
            }
            catch (HttpRequestException e)
            {
                GetPosts();
                ContentDialog deleteFileDialog = new ContentDialog()
                {
                    Title = "Error",
                    Content = "Deze post is al verwijderd.",
                    PrimaryButtonText = "OK",
                };
                ContentDialogResult result = await deleteFileDialog.ShowAsync();
            }
        }

        public async void PostPosts(Post p)
        {

            string body = JsonConvert.SerializeObject(p);
            var result = await client.PostAsync("http://localhost:50103/api/Posts", new StringContent(body, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode == true)
            {
                //succes
                Title.Text = "";
                Message.Text = "";
            }
        }

        private async void SendPost(object sender, RoutedEventArgs e)
        {
            if (Title.Text != "" && Message.Text != "")
            {
                Post newPost = new Post();
                newPost.Title = Title.Text;
                newPost.Text = Message.Text;
                newPost.Date = DateTime.Now;
                string body = JsonConvert.SerializeObject(newPost);
                var result = await client.PostAsync("http://localhost:50103/api/Posts", new StringContent(body, Encoding.UTF8, "application/json"));
                if (result.IsSuccessStatusCode == true)
                {
                    //Succes!
                }
            }
            else
            {
                ContentDialog deleteFileDialog = new ContentDialog()
                {
                    Title = "Error",
                    Content = "Gelieve alle velden in te vullen.",
                    PrimaryButtonText = "OK",
                };
                ContentDialogResult result = await deleteFileDialog.ShowAsync();
            }
        }

        private async void OnClickDelete(object sender, RoutedEventArgs e)
        {
            ContentDialog deleteFileDialog = new ContentDialog()
            {
                Title = "Delete post permanently?",
                Content = "If you delete this post, you won't be able to recover it. Do you want to delete it?",
                PrimaryButtonText = "Delete",
                SecondaryButtonText = "Cancel"
            };
            ContentDialogResult result = await deleteFileDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                Button temp = (Button)e.OriginalSource;
                DeletePost((int)temp.DataContext);
            }
        }

        private void refreshPosts(object sender, RoutedEventArgs e)
        {
            GetPosts();
        }    
    }
}
