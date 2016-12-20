using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Profile;
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
    public sealed partial class AdminLogin : Page
    {
        public AdminLogin()
        {
            this.InitializeComponent();
            username.Text = "admin@gmail.com";
            password.Password = "password";
        }

        private async void Aanmelden(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Admin admin = new Admin();
            admin.Email = username.Text;
            admin.Password = password.Password;

            string body = JsonConvert.SerializeObject(admin);
            var result = await client.PostAsync("http://localhost:50103/api/Admins", new StringContent(body, Encoding.UTF8, "application/json"));
            if(result.IsSuccessStatusCode == true)
            {
                Frame.Navigate(typeof(AdminPanel));
            }
        }
    }
}
