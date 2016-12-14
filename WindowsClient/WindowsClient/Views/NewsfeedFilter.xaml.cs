using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GraphVersionHelper;
using Facebook;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsfeedFilter : Page
    {
        ObservableCollection<PostObject> posts;

        public NewsfeedFilter()
        {
            this.InitializeComponent();

            getDataTest();
            //fill lists of feeds here
            

        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void onFilterChange(object sender, RoutedEventArgs e)
        {
            
        }

        private void updateFeeds1(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible splitView.IsPaneOpen = !splitView.IsPaneOpen;
            //link voor booleantovisibilityconverter (miss beter?) : http://stackoverflow.com/questions/39832208/how-to-use-booleantovisibilityconverter-in-uwp
            if (veranderNaam1.Visibility == Visibility.Collapsed)
                veranderNaam1.Visibility = Visibility.Visible;
            else
                veranderNaam1.Visibility = Visibility.Collapsed;
            
        }

        private void updateFeeds2(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
            if (veranderNaam2.Visibility == Visibility.Collapsed)
                veranderNaam2.Visibility = Visibility.Visible;
            else            
                veranderNaam2.Visibility = Visibility.Collapsed;
        }

        private void updateFeeds3(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
            if (veranderNaam3.Visibility == Visibility.Collapsed)
                veranderNaam3.Visibility = Visibility.Visible;
            else            
                veranderNaam3.Visibility = Visibility.Collapsed;
        }

        private void updateFeeds4(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
            if (veranderNaam4.Visibility == Visibility.Collapsed)
                veranderNaam4.Visibility = Visibility.Visible;
            else            
                veranderNaam4.Visibility = Visibility.Collapsed;
        }

        private void updateFeeds5(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
            if (veranderNaam5.Visibility == Visibility.Collapsed)
                veranderNaam5.Visibility = Visibility.Visible;
            else            
                veranderNaam5.Visibility = Visibility.Collapsed;
        }

        private void updateFeeds6(object sender, RoutedEventArgs e)
        {
            //set list of feeds visible or invisible
            if (veranderNaam6.Visibility == Visibility.Collapsed)
                veranderNaam6.Visibility = Visibility.Visible;
            else            
                veranderNaam6.Visibility = Visibility.Collapsed;
        }


        private async void /*Task<ObservableCollection<PostObject>>*/ getDataTest()
        {
            
            HttpClient client = new HttpClient();

            string token = "EAACEdEose0cBAIlVtfowVZA89gc3V7zDsHW3ezQuZAvDoOZB3WDHEh0fI8ZCM7YJZBoAmqDR2UMcUHzQYZC05stGVAjc9O1c54ELWZC6ZB1iNMAk8KlDnODm0CqYnRxmQSNyxxnL9rkYjSnZBGlpaV5ZBlZAzBZCQLm1ZA0lmPBZAsRV558QZDZD";

            string[] pages = new string[] {"HoGentCampusAalst" , "hogenttoegepasteinformatica", "874745529279221" };
            Dictionary<string, string> dicPage = new Dictionary<string, string>();
            dicPage.Add("Campus Aalst", "HoGentCampusAalst");

            foreach (KeyValuePair<string, string> entry in dicPage)
            {

                string oauthUrl = string.Format("https://graph.facebook.com/v2.8/{0}/feed?access_token={1}", entry.Value, token);
                string json = await client.GetStringAsync(oauthUrl);
                var result = JsonConvert.DeserializeObject<Wrapper>(json);

                posts = new ObservableCollection<PostObject>();

                for (int i = 0; i < 5; ++i)
                {
                    PostObject post = new PostObject()
                    {
                        id = result.data[i].id,
                        story = result.data[i].story,
                        message = result.data[i].message,
                        created_time = result.data[i].created_time
                    };

                    posts.Add(post);

                }
            }

            //return posts;
        }

    }

    class Wrapper
    {
        public PostObject[] data;

    }

    class PostObject
    {
        public string id { get; set; }
        public string story { get; set; }
        public string message { get; set; }
        public DateTime created_time { get; set; }
        public string page { get; set; }
    }
}
