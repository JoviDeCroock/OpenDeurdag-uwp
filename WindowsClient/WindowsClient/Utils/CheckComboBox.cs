using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
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
            foreach (object objTemp in this.Items)
            {
                if (objTemp.GetType() == typeof(CheckBox))
                {
                    chkTemp = (CheckBox) objTemp;
                    if (chkTemp.IsChecked == true)
                    {
                        String chkValue = chkTemp.Content.ToString();
                        String[] values = chkValue.Split('-');
                        String campusName = values[0].Trim();
                        String trainingName = values[1].Trim();

                        bool found = false;
                        foreach (Training existingTraining in selectedItems)
                        {
                            if (existingTraining.Name.Equals(trainingName))
                            {
                                existingTraining.Campus.Add(campusName);
                                found = true;
                            }
                        }

                        if (!found)
                        {
                            Training training = new Training() { Campus = new List<string>() , Name = trainingName};
                            training.Campus.Add(campusName);
                            selectedItems.Add(training);
                        }
                    }
                }
            }
        }

    }
}
