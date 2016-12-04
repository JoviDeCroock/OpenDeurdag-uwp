using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsfeedFilter : Page
    {
        public NewsfeedFilter()
        {
            this.InitializeComponent();
            //fill lists of feeds here
            //veranderNaam1.ItemsSource = ;
            //veranderNaam2.ItemsSource = ;
            //veranderNaam3.ItemsSource = ;
            //veranderNaam4.ItemsSource = ;
            //veranderNaam5.ItemsSource = ;
            //veranderNaam6.ItemsSource = ;
        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void updateFeeds1(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible splitView.IsPaneOpen = !splitView.IsPaneOpen;
            //link voor booleantovisibilityconverter : http://stackoverflow.com/questions/39832208/how-to-use-booleantovisibilityconverter-in-uwp
            if (veranderNaam1.Visibility == Visibility.Collapsed)
            { }
        }

        private void updateFeeds2(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
        }

        private void updateFeeds3(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
        }

        private void updateFeeds4(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
        }

        private void updateFeeds5(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
        }

        private void updateFeeds6(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
        }
    }
}
