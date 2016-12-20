using Newtonsoft.Json;
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
        private Dictionary<string, List<Student>> provincieDick = new Dictionary<string, List<Student>>();
        private List<CampusGegevens> campusses = new List<CampusGegevens>();
        private List<Student> studenten;
        private List<Post> posts;

        HttpClient client = new HttpClient();
        public AdminPanel()
        {
            this.InitializeComponent();
            provincieDick.Add("Oost-Vlaanderen", new List<Student>());
            provincieDick.Add("West-Vlaanderen", new List<Student>());
            provincieDick.Add("Brussel", new List<Student>());
            provincieDick.Add("Limburg", new List<Student>());
            provincieDick.Add("Henegouwen", new List<Student>());
            provincieDick.Add("Antwerpen", new List<Student>());
            provincieDick.Add("Luik", new List<Student>());
            provincieDick.Add("Luxemburg", new List<Student>());
            provincieDick.Add("Namen", new List<Student>());
            provincieDick.Add("Vlaams-Brabant", new List<Student>());
            provincieDick.Add( "Waals-Brabant", new List<Student>());
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
                provincieDick.Where(prov => prov.Key.Equals(p.Province)).FirstOrDefault().Value.Add(p);
                studenten.Add(p);
            }

            string jsonCampusses = await client.GetStringAsync("http://localhost:50103/api/Campus");
            var result2 = JsonConvert.DeserializeObject<List<Campus>>(jsonStudents);
            foreach(Campus c in result2)
            {
                CampusGegevens temp = new CampusGegevens();
                temp.name = c.Name;
                temp.provincies = provincieDick.Keys.ToList();
                temp.trainings = c.Trainingen;
                temp.aantalStuds = new List<string>();
                foreach(string p in temp.provincies)
                {
                    temp.aantalStuds.Add(provincieDick.Where(prov => prov.Key.Equals(p)).FirstOrDefault().Value.Count().ToString());
                }
                campusses.Add(temp);
            }


        }

        public async void GetPosts()
        {            
            /*Miss bij Posts een date adden pff.... Meer Migrations euj*/
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
            /*Miss bij Posts een date adden pff.... Meer Migrations euj*/
            HttpClient client = new HttpClient();
            try
            {
                var result = await client.GetStringAsync("http://localhost:50103/api/Posts/" + id);
                await client.DeleteAsync("http://localhost:50103/api/Posts/" + id);
            }
            catch(HttpRequestException e)
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
            }
        }

        private async void SendPost(object sender, RoutedEventArgs e)
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

            // Delete the post if the user clicked the primary button. 
            /// Otherwise, do nothing. 
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
