using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WindowsClient.Utils;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Routes : Page
    {
        public ObservableCollection<RouteImage> Images { get; set; }
        public Routes()
        {
            this.InitializeComponent();
            //Images.Add(new RouteImage("ms-appx:///Images/route.png"));       
            Images = new ObservableCollection<RouteImage>();
            Images.Add(new RouteImage("ms-appx:///Images/route.png"));
            Images.Add(new RouteImage("ms-appx:///Images/GSCHB1.png"));

            ImageFlipView.ItemsSource = Images;
        }

        private async void scrollViewer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            var doubleTapPoint = e.GetPosition(scrollViewer);

            if (scrollViewer.ZoomFactor != 1)
            {
                scrollViewer.ZoomToFactor(1);
            }
            else if (scrollViewer.ZoomFactor == 1)
            {
                scrollViewer.ZoomToFactor(2);

                var dispatcher = Window.Current.CoreWindow.Dispatcher;
                //await dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                //{
                //    scrollViewer.ScrollToHorizontalOffset(doubleTapPoint.X);
                //    scrollViewer.ScrollToVerticalOffset(doubleTapPoint.Y);
                //});
            }
        }
    }


}
