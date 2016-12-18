
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WindowsClient.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel; 

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrainingChoice : Page
    {
        ObservableCollection<WindowsClient.Models.Training2> trainingsGent;

        public TrainingChoice()
        {
            this.InitializeComponent();

            trainingsGent = new ObservableCollection<WindowsClient.Models.Training2>() {
                new WindowsClient.Models.Training2() { Name="Bedrijfsmanagement"},
                new WindowsClient.Models.Training2() { Name="Office management"},
                new WindowsClient.Models.Training2() { Name="Retailmanagement"},
                new WindowsClient.Models.Training2() { Name="Toegepaste informatica"}
                };

            listViewTrainingsGent.ItemsSource = trainingsGent;
        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void listViewHoGentItem_Click(object sender, ItemClickEventArgs e)
        {
            Training2 selectedTraining = (Training2)e.ClickedItem;
            var dialog = new Windows.UI.Popups.MessageDialog("U klikte op " + selectedTraining.Name);

            await dialog.ShowAsync();
        }
    }
}
