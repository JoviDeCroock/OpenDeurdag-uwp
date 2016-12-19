using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.Collections;
using WindowsClient.Models;


namespace WindowsClient.Utils
{
    public class CheckComboBox : ComboBox
    {
        private List<Training> selectedItems;

        public List<Training> SelectedItems
        {
            get
            {
                SetSelectedItems();
                return selectedItems;
            }
            set { selectedItems = value; }
        }

        internal void SetSelectedItems()
        {
            CheckBox chkTemp = null;
            selectedItems = new List<Training>();
            foreach (object objTemp in Items)
            {
                if (objTemp.GetType() == typeof(CheckBox))
                {
                    chkTemp = (CheckBox)objTemp;
                    if (chkTemp.IsChecked == true)
                    {
                        string chkValue = chkTemp.Content.ToString();
                        string[] values = chkValue.Split('-');
                        string campusName = values[0].Trim();
                        string trainingName = values[1].Trim();
                        Training training = new Training() {
                            Campussen = new List<string>(),
                            Name = trainingName};
                        training.Campussen.Add(campusName);

                        bool found = false;
                        foreach (Training existingTraining in selectedItems)
                        {
                            if (existingTraining.Name.Equals(trainingName))
                            {
                                existingTraining.Campussen.Add(campusName);
                                found = true;
                            }
                        }

                        if (!found)
                        {
                            training = new Training() { Campussen = new List<string>() , Name = trainingName};
                            training.Campussen.Add(campusName);
                            selectedItems.Add(training);
                        }
                    }
                }
            }
        }

    }
}
