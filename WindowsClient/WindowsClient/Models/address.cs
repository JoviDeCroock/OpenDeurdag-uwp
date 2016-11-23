using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.Models
{
    class address
    {
        string street;
        string houseNumber;
        string city;
        string county;

        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        public string HouseNumber
        {
            get { return houseNumber; }
            set { houseNumber = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string County
        {
            get { return county; }
            set { county = value; }
        }
    }
}
