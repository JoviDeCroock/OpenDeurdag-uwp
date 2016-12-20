using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private List<Student> studenten;
        private List<Post> posts;
        public AdminPanel()
        {
            this.InitializeComponent();
            GetPosts();
        }

        public async void GetStudents()
        {
            studenten = new List<Student>();
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync("http://localhost:50103/api/Student");
            var result = JsonConvert.DeserializeObject<List<Student>>(json);
            foreach (Student p in result)
            {
                studenten.Add(p);
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
                TextBlock d = new TextBlock();
                d.Text = p;
                posts.Add(p);
            }
        }

        public async void PostPosts(Post p)
        {
            HttpClient client = new HttpClient();
            string body = JsonConvert.SerializeObject(p);
            var result = await client.PostAsync("http://localhost:50103/api/Posts", new StringContent(body, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode == true)
            {
                //Succes!
            }
        }
    }
}
