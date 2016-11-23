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
        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }

    /*
    private void OnHardwareButtonsBackPressed(object sender, BackPressedEventArgs e)
    {
        // This is the missing line!
        e.Handled = true;

        // Close the App if you are on the startpage
        if (mMainFrame.CurrentSourcePageType == typeof(Startpage))
            App.Current.Exit();

        // Navigate back
        if (mMainFrame.CanGoBack)
        {
            mMainFrame.GoBack();
        }
    }*/
}
