using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace WindowsClient.Utils
{
    public class CheckComboBox : ComboBox
    {
        ArrayList selectedItems;

        public ArrayList SelectedItems
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
            selectedItems = new ArrayList();
            foreach (object objTemp in this.Items)
            {
                if (objTemp.GetType() == typeof(CheckBox))
                {
                    chkTemp = (CheckBox)objTemp;
                    if (chkTemp.IsChecked == true)
                    {
                        selectedItems.Add(chkTemp);

                    }
                }
            }
        }

    }
}
