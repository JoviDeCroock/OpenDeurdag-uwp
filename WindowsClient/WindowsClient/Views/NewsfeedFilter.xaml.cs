﻿using System;
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
using WindowsClient.Models;
using Windows.System.Profile;

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

            
            checkboxFeeds = new List<CheckBox>();
            posts = new ObservableCollection<PostObject>();
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                fillPosts();
                
            } else
            {
                fillPostsMobile();
            }
            fillCheckboxes();
        }
        private void onFilterChange(object sender, RoutedEventArgs e)
        {
            List<PostObject> tempPosts = new List<PostObject>();
            feedLijst.Visibility = Visibility.Collapsed;

            //iterate through all the checkboxes
            foreach (CheckBox cb in checkboxFeeds)
            {
                //check if marked
                if (cb.IsChecked == true)
                {
                    feedLijst.Visibility = Visibility.Visible;

                    //get all posts linked to the checkboxes and add them to tempPosts    
                    var temp = posts.Where(p => p.page.ToLower().Equals(cb.Content.ToString().ToLower()));
                    foreach (PostObject p in temp)
                    {
                        tempPosts.Add(p);
                    }
                }
            }

            feedLijst.ItemsSource = tempPosts.OrderByDescending(p => p.created_time).ToList();

        }

        private async void fillPosts()
        {
            //create http client + set token
            HttpClient client = new HttpClient();
            
            //get all the links to the feeds 
            string campusJson = await client.GetStringAsync("http://localhost:50103/api/Campus");
            var Campusses = JsonConvert.DeserializeObject<List<RootObject>>(campusJson);
            Dictionary<string, string> dicPage = new Dictionary<string, string>();

            foreach(RootObject c in Campusses)
            {
                dicPage.Add(c.Name, c.Feed);
                foreach(WindowsClient.Models.Training t in c.Trainingen)
                {
                    if(!dicPage.ContainsKey(t.Name))                        
                        dicPage.Add(t.Name, t.Feed);
                }
            }

            //facebook token
            string token = "EAACEdEose0cBAKPym9PzFo7ZAf0iqWA4LTcHTwcnvohMDzHGA6GVV91hGVS36XCfWBT7yjSHENhHoSTBiVyxi8TLulFZBnZAdGuoC6pfa3PvC5n57TLOhOknzQt9sRbb6Dk0ZAlZBoLyrI8ryC03ihKXg7pmzOmR8ozh1G1j2KAZDZD";

            //get all the facebooks posts
            try
            {
                foreach (KeyValuePair<string, string> entry in dicPage)
                {
                    //This crap page doesn't wanne share its feeds >=(
                    if (!entry.Key.Equals("HoGent Schoonmeersen"))
                    {
                        //set the appropriate url and get json
                        string oauthUrl = string.Format("https://graph.facebook.com/v2.8/{0}/feed?access_token={1}", entry.Value, token);
                        string json = await client.GetStringAsync(oauthUrl);
                        //Debug.Write(json);
                        var result = JsonConvert.DeserializeObject<Wrapper>(json);

                        //convert the json to PostObjects
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
                            //add retrieved post to posts
                            posts.Add(post);
                        }
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

            string adminPosts = await client.GetStringAsync("http://localhost:50103/api/Posts");
            var result2 = JsonConvert.DeserializeObject<List<Post>>(adminPosts);
            foreach(Post p in result2)
            {
                PostObject PO = new PostObject();
                PO.page = "Admin";
                PO.message = p.Title + "\n\n" + p.Text;
                PO.created_time = p.Date;

                posts.Add(PO);
            }


        }

        private void onFilterChangeMobile(object sender, RoutedEventArgs e)
        {
            List<PostObject> tempPosts = new List<PostObject>();
            feedLijst.Visibility = Visibility.Collapsed;

            //iterate through all the checkboxes
            foreach (CheckBox cb in checkboxFeeds)
            {
                //check if marked
                if (cb != null && cb.IsChecked == true)
                {
                    feedLijst.Visibility = Visibility.Visible;

                    //get all posts linked to the checkboxes and add them to tempPosts    
                    var temp = posts.Where(p => p.page.ToLower().Equals(cb.Content.ToString().ToLower()));
                    foreach (PostObject p in temp)
                    {
                        tempPosts.Add(p);
                    }
                }
            }

            feedLijst.ItemsSource = tempPosts.OrderByDescending(p => p.created_time).ToList();

        }

        private async void fillPostsMobile()
        {
            //create http client + set token
            HttpClient client = new HttpClient();

            //get all the links to the feeds 
            Campus[] campussenObj = new Campus[]
                        {
                new Campus() { CampusId = 1, Name = "HoGent Schoonmeersen", City = "Gent", Street = "Valentin Vaerwyckweg", HouseNumber = "1", Telephone = "09 243 35 60", Feed = "Hogeschool-Gent-Campus-Schoonmeersen" },
                new Campus() { CampusId = 2, Name = "HoGent Aalst", City = "Aalst", Street = "Arbeidstraat", HouseNumber = "14", Telephone = "09 243 38 00", Feed = "HoGentCampusAalst" }
            };
            Training[] trainingenObj = new Training[]
            {
                new Training() { TrainingId = 1, Name="Toegepaste Informatica", Description="Computerrichting, hier start de leerling volledig vanaf de basis. Daarna heeft de leerling keuze om netwerken/programmeren te doen.", Feed = "hogenttoegepasteinformatica"},
                new Training() { TrainingId = 2, Name="Bedrijfsmanagement", Description="Praktijk georienteerde richting over het managen van een bedrijf.", Feed = "hogentbedrijfsmanagement"},
                new Training() { TrainingId = 3, Name="Retail management", Description="Voorbereiding op de job van strategisch manager in de detailhandel.", Feed = "hogentretailmanagement"},
                new Training() { TrainingId = 4, Name="Office management", Description="Voorbereidende richting op het organiserende en coordinerende aspect in bedrijven.", Feed = "hogentofficemanagement"}
            };

            Dictionary<string, string> dicPage = new Dictionary<string, string>();

            foreach (Campus c in campussenObj)
            {
                dicPage.Add(c.Name, c.Feed);
            }

            foreach (Training t in trainingenObj)
            {
                dicPage.Add(t.Name, t.Feed);
            }

            //facebook token
            string token = "EAACEdEose0cBADR1gM0UaWcwyEg8gdZCABEZC6CLsAGG1yCievZAYCZB1AuZBbc6HI7n78ZCAJfvdP34ZAZBELSWnSXNcZC9dAcuDrRRJiu64PZAD0uJRpB3aeT0VEwlxpBEuA0qBqWm5CY2Efw3dWuZC2hyuvJu5Uuv835C8VYDmEFggZDZD";

            //get all the facebooks posts
            try
            {
                foreach (KeyValuePair<string, string> entry in dicPage)
                {
                    //This crap page doesn't wanne share its feeds >=(
                    if (!entry.Key.Equals("HoGent Schoonmeersen"))
                    {
                        //set the appropriate url and get json
                        string oauthUrl = string.Format("https://graph.facebook.com/v2.8/{0}/feed?access_token={1}", entry.Value, token);
                        string json = await client.GetStringAsync(oauthUrl);
                        //Debug.Write(json);
                        var result = JsonConvert.DeserializeObject<Wrapper>(json);

                        //convert the json to PostObjects
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
                            //add retrieved post to posts
                            posts.Add(post);
                        }
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
        }

        private void fillCheckboxes()
        {
            //add all checkboxfilters to the list
            checkboxFeeds.Add(checkboxAalst);
            //checkboxFeeds.Add(checkboxSchoonmeersen);
            checkboxFeeds.Add(checkboxTI);
            checkboxFeeds.Add(checkboxBM);
            checkboxFeeds.Add(checkboxRM);
            checkboxFeeds.Add(checkboxOM);
            if (AnalyticsInfo.VersionInfo.DeviceFamily != "Windows.Mobile")
            {
                checkboxFeeds.Add(checkboxAdmin);
            }
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

    public class RootObject
    {
        public List<WindowsClient.Models.Training> Trainingen { get; set; }
        public int CampusId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Telephone { get; set; }
        public string Feed { get; set; }
    }
}
