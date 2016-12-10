using RondleidingAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models
{
    public class Student : User
    {
        public int StudentId { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public virtual IList<StudentCampus> PrefCampus { get; set; }
        public virtual IList<StudentTraining> PrefTraining { get; set; }
        public Student() { PrefCampus = new List<StudentCampus>(); PrefTraining = new List<StudentTraining>(); }
        public Student(string street, string houseNumber, string province, string city, string name, string email)
        {
            Street = street;
            HouseNumber = houseNumber;
            Province = province;
            City = city;
            Name = name;
            Email = email;
            PrefCampus = new List<StudentCampus>();
            PrefTraining = new List<StudentTraining>();
        }

        public void addCampus(StudentCampus c)
        {
            c.Campus.addToStudents(c);
            PrefCampus.Add(c);
        }
        public void removeCampus(int id)
        {     
            /*Beter uitwerken*/
            StudentCampus d = PrefCampus.Where(campus => campus.CampusId == id).FirstOrDefault();
            if (d != null)
                PrefCampus.Remove(d);
        }

        public void addTraining(StudentTraining t)
        {
            t.Training.addToStudents(t);
            PrefTraining.Add(t);
        }
        public void removeTraining(int id)
        {
            /*Beter uitwerken*/
            StudentTraining t = PrefTraining.Where(training => training.TrainingId == id).FirstOrDefault();
            if (t != null)
                PrefTraining.Remove(t);
        }

    }
}