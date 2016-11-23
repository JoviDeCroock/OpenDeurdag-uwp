using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.Models
{
    class Person
    {
        string firstname;
        string lastname;
        address address;
        string email;
        Preference preference;
        public Person(string firstname, string lastname, string email, string street, string houseNumber, string city, string county, string training, string campus)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            setAddress(street, houseNumber, city, county);
            setPreference(training, campus);
        }
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public void setAddress(string street, string houseNumber, string city, string county)
        {
            address.Street = street;
            address.HouseNumber = houseNumber;
            address.City = city;
            address.County = county;
        }
        public void setPreference(string training, string campus)
        {
            preference.Training = training;
            preference.Campus = campus;
        }
    }
}
