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
        ObservableCollection<Training> trainingsGent;
        ObservableCollection<Training> trainingsAalst;

        public TrainingChoice()
        {
            this.InitializeComponent();

            trainingsGent = new ObservableCollection<Training>() {
                new Training() { Name="Bedrijfsmanagement"},
                new Training() { Name="Office management"},
                new Training() { Name="Retailmanagement"},
                new Training() { Name="Toegepaste informatica"}

                };

            trainingsAalst = new ObservableCollection<Training>() {
                new Training() { Name="Geen idee"},
                new Training() { Name="Iemand help"},
                new Training() { Name="Ik weet enkel dat er dit gevolgd kan worden"},
                new Training() { Name="Toegepaste informatica"}

                };

            listViewTrainingsGent.ItemsSource = trainingsGent;
            listViewTrainingsAalst.ItemsSource = trainingsAalst;
        }

        private void onHomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void openListTrainingGent_Click(object sender, RoutedEventArgs e)
        {
            if (listViewTrainingsGent.Visibility == Visibility.Collapsed)
            {
                //make ListView visible
                listViewTrainingsGent.Visibility = Visibility.Visible;

            }
            else
            {
                listViewTrainingsGent.Visibility = Visibility.Collapsed;
            }
        }

        private void openListTrainingAalst_Click(object sender, RoutedEventArgs e)
        {
            if (listViewTrainingsAalst.Visibility == Visibility.Collapsed)
            {
                //make ListView visible
                listViewTrainingsAalst.Visibility = Visibility.Visible;

            }
            else
            {
                listViewTrainingsAalst.Visibility = Visibility.Collapsed;
            }
        }

        private async void listViewGentItem_Click(object sender, ItemClickEventArgs e)
        {
            Training selectedTraining = (Training)e.ClickedItem;
            var dialog = new Windows.UI.Popups.MessageDialog("U klikte op " + selectedTraining.Name);

            await dialog.ShowAsync();
        }

        private async void listViewAalstItem_Click(object sender, ItemClickEventArgs e)
        {
            Training selectedTraining = (Training)e.ClickedItem;
            var dialog = new Windows.UI.Popups.MessageDialog("U klikte op " + selectedTraining.Name);

            await dialog.ShowAsync();
        }
    }
}
