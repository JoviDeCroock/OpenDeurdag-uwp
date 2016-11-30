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
    }
}
