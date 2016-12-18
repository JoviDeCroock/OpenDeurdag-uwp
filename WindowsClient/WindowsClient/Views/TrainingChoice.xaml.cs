
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
        ObservableCollection<WindowsClient.Models.Training> trainingsGent;

        public TrainingChoice()
        {
            this.InitializeComponent();

            trainingsGent = new ObservableCollection<WindowsClient.Models.Training>() {
                new WindowsClient.Models.Training() { Name="Bedrijfsmanagement"},
                new WindowsClient.Models.Training() { Name="Office management"},
                new WindowsClient.Models.Training() { Name="Retailmanagement"},
                new WindowsClient.Models.Training() { Name="Toegepaste informatica"}
                };

            listViewTrainingsGent.ItemsSource = trainingsGent;
        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void listViewHoGentItem_Click(object sender, ItemClickEventArgs e)
        {
            Training selectedTraining = (Training)e.ClickedItem;
            var dialog = new Windows.UI.Popups.MessageDialog("U klikte op " + selectedTraining.Name);

            await dialog.ShowAsync();
        }
    }
}
