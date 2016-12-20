using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WindowsClient.Models;
using WindowsClient.Utils;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsClient.Views
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Routes : Page
    {
        //public Campus Destination { get; set; }

        public ObservableCollection<RouteImage> Images { get; set; }
        public Routes()
        {
            this.InitializeComponent();
            fillCampusses();
            //ShowRouteOnMap();


            //Images.Add(new RouteImage("ms-appx:///Images/route.png"));       
            //Images = new ObservableCollection<RouteImage>();
            //Images.Add(new RouteImage("ms-appx:///Images/route.png"));
            //Images.Add(new RouteImage("ms-appx:///Images/GSCHB1.png"));
            //ImageFlipView.ItemsSource = Images;
        }

        private async void fillCampusses()
        {
            //create http client + set token
            HttpClient client = new HttpClient();

            //get all the links to the feeds 
            string campusJson = await client.GetStringAsync("http://localhost:50103/api/Campus");
            var Camps = JsonConvert.DeserializeObject<List<RootObject>>(campusJson);
            //Dictionary<string, RootObject> dicPage = new Dictionary<string, RootObject>();
            var items = new ObservableCollection<Campus>();
            foreach (RootObject c in Camps)
            {
                //dicPage.Add(c.Name, c);
                items.Add(new Campus() { CampusId = c.CampusId, City = c.City, Feed = c.Feed, HouseNumber = c.HouseNumber, Name = c.Name, Street = c.Street, Telephone = c.Telephone });
            }
            Campusses.ItemsSource = items;
            Campusses.SelectedItem = items[0];

            if (Campusses.SelectedItem != null)
            {
                ShowRouteOnMap();
            }

            var itemsLevels = new ObservableCollection<Level>();
            for(int i = 0; i <= 4; i++)
            {
                itemsLevels.Add(new Level() { LevelName = i==0?"Benedenverdieping":i + "e verdieping", LevelNr = i });
            }
            Levels.ItemsSource = itemsLevels;
            Levels.SelectedItem = itemsLevels[0];

            if(Levels.SelectedItem != null)
            {
                ShowLevel();
            }
        }

        private void ShowLevel()
        {
            LevelImage.Source = new BitmapImage(new Uri("ms-appx:///Images/GSCHB" + ((Level) Levels.SelectedItem).LevelNr + ".png"));
        }

        private async void ShowRouteOnMap()
        {
            HttpClient client = new HttpClient();

            var coordinatJson = await client.GetStringAsync("http://dev.virtualearth.net/REST/v1/Locations?CountryRegion=Belgium&adminDistrict=&locality=" + ((Campus)Campusses.SelectedItem).City + "&postalCode=&addressLine=" + ((Campus)Campusses.SelectedItem).Street + ((Campus)Campusses.SelectedItem).HouseNumber + "&key=QJnnkMOv4orFAt7p4GK1~ikA6-gj9lszj5iNbmmhGUA~AihogMlA2PlLcNuwEBlmsXhaMUVihNwVB_ja3eWdfaNts9aEQTutaGi9q66wMGFW");

            var Coordinats = JsonConvert.DeserializeObject<RootObjectLocation>(coordinatJson);

            double latitude = Coordinats.resourceSets[0].resources[0].geocodePoints[0].coordinates[0];
            double longitude = Coordinats.resourceSets[0].resources[0].geocodePoints[0].coordinates[1];

            // Request permission to access location
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 100 };

                    Geoposition startPosition = await geolocator.GetGeopositionAsync().AsTask();


                    BasicGeoposition endLocation = new BasicGeoposition() { Latitude = latitude, Longitude = longitude };

                    BasicGeoposition gep = new BasicGeoposition();
                    gep.Latitude = startPosition.Coordinate.Latitude;
                    gep.Longitude = startPosition.Coordinate.Longitude;
                    Geopoint gp1 = new Geopoint(gep);
                    Geopoint gp2 = new Geopoint(endLocation);


                    // Get the route between the points.
                    MapRouteFinderResult routeResult =
                          await MapRouteFinder.GetDrivingRouteAsync(
                          gp1,
                          gp2,
                          MapRouteOptimization.Time,
                          MapRouteRestrictions.None);

                    // Create a MapIcon.
                    MapIcon mapIcon1 = new MapIcon();
                    mapIcon1.Location = gp1;
                    mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    mapIcon1.Title = "Uw locatie";
                    mapIcon1.ZIndex = 0;
                    // Add the MapIcon to the map.
                    MyMap.MapElements.Add(mapIcon1);

                    MapIcon mapIcon2 = new MapIcon();
                    mapIcon2.Location = gp2;
                    mapIcon2.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    mapIcon2.Title = ((Campus) Campusses.SelectedItem).Name;
                    mapIcon1.ZIndex = 0;
                    MyMap.MapElements.Add(mapIcon2);


                    if (routeResult.Status == MapRouteFinderStatus.Success)
                    {
                        // Use the route to initialize a MapRouteView.
                        MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                        viewOfRoute.RouteColor = Colors.Red;
                        viewOfRoute.OutlineColor = Colors.Red;

                        // Add the new MapRouteView to the Routes collection
                        // of the MapControl.
                        if (MyMap.Routes.Count > 0)
                        {
                            MyMap.Routes.RemoveAt(0);
                        }
                        MyMap.Routes.Add(viewOfRoute);

                        // Fit the MapControl to the route.
                        await MyMap.TrySetViewBoundsAsync(
                              routeResult.Route.BoundingBox,
                              null,
                              Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
                    }

                    break;

                case GeolocationAccessStatus.Denied:
                    var dialog = new Windows.UI.Popups.MessageDialog("Gelieve uw locatie te delen. Dit kan u doen in de privacyinstellingen van uw toestel.");
                    await dialog.ShowAsync();
                    break;
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((Campus) Campusses.SelectedItem).City == "Gent")
            {
                LevelStack.Visibility = Visibility.Visible;
                LevelImage.Visibility = Visibility.Visible;
            }
            else
            {
                LevelStack.Visibility = Visibility.Collapsed;
                LevelImage.Visibility = Visibility.Collapsed;
            }
            ShowRouteOnMap();
        }

        private void SelectionLevelChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowLevel();
        }

        public class Level
        {
            public string LevelName { get; set; }
            public int LevelNr { get; set; }
        }


        //private async void scrollViewer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        //{
        //    var scrollViewer = sender as ScrollViewer;
        //    var doubleTapPoint = e.GetPosition(scrollViewer);

        //    if (scrollViewer.ZoomFactor != 1)
        //    {
        //        scrollViewer.ZoomToFactor(1);
        //    }
        //    else if (scrollViewer.ZoomFactor == 1)
        //    {
        //        scrollViewer.ZoomToFactor(2);

        //        var dispatcher = Window.Current.CoreWindow.Dispatcher;
        //        //await dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
        //        //{
        //        //    scrollViewer.ScrollToHorizontalOffset(doubleTapPoint.X);
        //        //    scrollViewer.ScrollToVerticalOffset(doubleTapPoint.Y);
        //        //});
        //    }
        //}

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

        public class Address
        {
            public string addressLine { get; set; }
            public string adminDistrict { get; set; }
            public string adminDistrict2 { get; set; }
            public string countryRegion { get; set; }
            public string formattedAddress { get; set; }
            public string locality { get; set; }
            public string postalCode { get; set; }
        }

        public class GeocodePoint
        {
            public string type { get; set; }
            public List<double> coordinates { get; set; }
            public string calculationMethod { get; set; }
            public List<string> usageTypes { get; set; }
        }

        public class ResourceSet
        {
            public int estimatedTotal { get; set; }
            public List<Resource> resources { get; set; }
        }

        public class Resource
        {
            public string __type { get; set; }
            public List<double> bbox { get; set; }
            public string name { get; set; }
            public Point point { get; set; }
            public Address address { get; set; }
            public string confidence { get; set; }
            public string entityType { get; set; }
            public List<GeocodePoint> geocodePoints { get; set; }
            public List<string> matchCodes { get; set; }
        }


        public class RootObjectLocation
        {
            public string authenticationResultCode { get; set; }
            public string brandLogoUri { get; set; }
            public string copyright { get; set; }
            public List<ResourceSet> resourceSets { get; set; }
            public int statusCode { get; set; }
            public string statusDescription { get; set; }
            public string traceId { get; set; }
        }

        private async void ImageDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var sv = sender as ScrollViewer;

            if (sv == null) return;
            Point p = e.GetPosition(sv);

            TimeSpan period = TimeSpan.FromMilliseconds(10);

            Windows.System.Threading.ThreadPoolTimer.CreateTimer(async (source) =>
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (sv.ZoomFactor <= 1)
                    {
                        //var k = sv.ChangeView(p.X + sv.HorizontalOffset * 2, p.Y + sv.VerticalOffset * 2, 2);
                        var k = sv.ChangeView(50, 50, 2);
                    }
                    else
                    {
                        sv.ChangeView(sv.HorizontalOffset / 2 - p.X, sv.VerticalOffset / 2 - p.Y, 1);
                    }
                });
            }
            , period);

        }
    }


}
