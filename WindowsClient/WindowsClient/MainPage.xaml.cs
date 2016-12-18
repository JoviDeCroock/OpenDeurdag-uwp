using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WindowsClient.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.System.Profile;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.Foundation.Metadata;
using System.Net.Http;
using Newtonsoft.Json;
//using RondleidingAPI.Models.Domain;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WindowsClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //on initial load go to newsfeed
            switch (AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Desktop":
                    mainfr.Navigate(typeof(NewsfeedFilter));
                    break;
            }

            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {

                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.HideAsync();
                }
            }
        }

        private void on_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void goToNewsFeed(object sender, RoutedEventArgs e)
        {
            switch (AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                    Frame.Navigate(typeof(NewsfeedFilter));
                    break;
                case "Windows.Desktop":
                    mainfr.Navigate(typeof(NewsfeedFilter));
                    break;
            }
        }

        private void goToTrainings(object sender, RoutedEventArgs e)
        {
            switch (AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                    Frame.Navigate(typeof(TrainingChoice));
                    break;
                case "Windows.Desktop":
                    mainfr.Navigate(typeof(TrainingChoice));
                    break;
            }
        }

        private void goToMyPreferences(object sender, RoutedEventArgs e)
        {
            switch (AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                    Frame.Navigate(typeof(MyPreferences));
                    break;
                case "Windows.Desktop":
                    mainfr.Navigate(typeof(MyPreferences));
                    break;
            }
        }

        private void updateTile()
        {
            /* TileWide310x150SmallImageAndText04 */
            string strxml = "<tile>"
                             + "<visual>"
                             + "<binding template='TileSmall'>"
                             + "<image id='1' src='Assets/pikachuTile.png'/>"
                             /*
                             + "<text id='1'>TEXT ID 1</text>"
                             + "<text id='2'>TEXT ID 2</text>"
                             */
                             + "</binding>"
                             + "</visual>"
                             + "</tile>";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(strxml);

            var updator = TileUpdateManager.CreateTileUpdaterForApplication();
            var notification = new TileNotification(xml);
            updator.Update(notification);
        }

        private async void fillData()
        {
            /*
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("http://localhost:5495/api/Campus"));
            var lists = JsonConvert.DeserializeObject<List<Campus>>(json);
            */
        }
    }
}
