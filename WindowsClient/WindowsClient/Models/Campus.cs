using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsClient.Models
{
    public class Campus
    {
        public int CampusId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Telephone { get; set; }
        public string Feed { get; set; }
        public virtual List<Training> Trainingen { get; set; }
        public IList<int> TrainingIds { get; set; }
        public virtual IList<Student> Studenten { get; set; }
        public IList<int> StudentIds { get; set; }
        public Campus() { Trainingen = new List<Training>(); Studenten = new List<Student>(); TrainingIds = new List<int>();StudentIds = new List<int>();}
        public Campus(string name, string city, string street, string houseNumber, string telephone, string feed)
        {
            Feed = feed;
            Studenten = new List<Student>();
            Trainingen = new List<Training>();
            TrainingIds = new List<int>();
            StudentIds = new List<int>();
            Name = name;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            Telephone = telephone;
        }
        public void addToTraining(Training t)
        {
            TrainingIds.Add(t.TrainingId);
            Trainingen.Add(t);
        }
        public void addToStudents(Student t)
        {
            StudentIds.Add(t.StudentId);
            Studenten.Add(t);
        }
    }
}