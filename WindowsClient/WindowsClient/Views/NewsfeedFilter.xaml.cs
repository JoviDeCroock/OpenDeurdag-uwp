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
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsfeedFilter : Page
    {
        ObservableCollection<PostObject> posts;
        List<CheckBox> checkboxFeeds;

        public NewsfeedFilter()
        {
            this.InitializeComponent();

            posts = new ObservableCollection<PostObject>();
            checkboxFeeds = new List<CheckBox>();

            fillPosts();
            fillCheckboxes();
        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void onFilterChange(object sender, RoutedEventArgs e)
        {
            List<PostObject> tempPosts = new List<PostObject>();

            //overlopen alle checkboxes
            foreach (CheckBox cb in checkboxFeeds)
            {
                //kijk of checkbox aangevinkt is
                if (cb.IsChecked == true)
                {
                    //alle posts ophalen die geassocieerd worden met de checkbox en toevoegen aan tempPosts        
                    var temp = posts.Where(p => p.page.ToLower().Equals(cb.Content.ToString().ToLower()));
                    foreach (PostObject p in temp)
                    {
                        tempPosts.Add(p);
                    }
                }
            }

            feedLijst.ItemsSource = tempPosts;
            feedLijst.Visibility = Visibility.Visible;

        }

        private async void /*Task<ObservableCollection<PostObject>>*/ fillPosts()
        {

            HttpClient client = new HttpClient();
            string token = "EAACEdEose0cBADktuPcZB4sfnFKkKdgu7gjmEW7rOLXaDVY7gZAjtZCaU94EZAfCdEqmQfZAMLwZBZCBI1ZAQ3h2lNZB8rQQhwPs1xZCQtEFZAQZCgH6YsongbAsKZAyNAQF9cqO5dwLFnw7uR6hEfOd3XiOgfLInrZCdguyCmgwqW4wrZAMgZDZD";

            Dictionary<string, string> dicPage = new Dictionary<string, string>();
            dicPage.Add("HoGent Aalst", "HoGentCampusAalst");
            //dicPage.Add("HoGent Schoonmeersen", "Hogeschool-Gent-Campus-Schoonmeersen");
            dicPage.Add("Toegepaste Informatica", "hogenttoegepasteinformatica");
            dicPage.Add("Bedrijfsmanagement", "hogentbedrijfsmanagement");
            dicPage.Add("Retail management", "hogentretailmanagement");
            dicPage.Add("Office management", "hogentofficemanagement");

            try
            {
                foreach (KeyValuePair<string, string> entry in dicPage)
                {
                    string oauthUrl = string.Format("https://graph.facebook.com/v2.8/{0}/feed?access_token={1}", entry.Value, token);
                    string json = await client.GetStringAsync(oauthUrl);
                    Debug.Write(json);
                    var result = JsonConvert.DeserializeObject<Wrapper>(json);

                    for (int i = 0; i < 5; ++i)
                    {
                        PostObject post = new PostObject()
                        {
                            id = result.data[i].id,
                            story = result.data[i].story,
                            message = result.data[i].message,
                            created_time = result.data[i].created_time,
                            page = entry.Key
                        };

                        posts.Add(post);

                    }
                }
            }
            catch (HttpRequestException e)
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Er ging iets mis.");
                if (e.Message.Contains("400"))
                {
                    dialog = new Windows.UI.Popups.MessageDialog("Error 400 : Bad request. Refresh facebook token.\nhttps://developers.facebook.com/tools/explorer/");
                    await dialog.ShowAsync();
                }
                
                await dialog.ShowAsync();
            }
            catch (Exception e)
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Er ging iets mis.");
                await dialog.ShowAsync();
            }

            //return posts;
        }

        private void fillCheckboxes()
        {
            //alle checkboxfilters toevoegen aan een lijst
            checkboxFeeds.Add(checkboxAalst);
            //checkboxFeeds.Add(checkboxSchoonmeersen);
            checkboxFeeds.Add(checkboxTI);
            checkboxFeeds.Add(checkboxBM);
            checkboxFeeds.Add(checkboxRM);
            checkboxFeeds.Add(checkboxOM);
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
