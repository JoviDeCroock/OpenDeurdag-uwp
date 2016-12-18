using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Windowsclient.Models
{
    public class Training
    {
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<Campus> Campussen { get; set; }
        public IList<int> CampusId { get; set; }
        public string Feed { get; set; }
        public virtual IList<Student> Studenten { get; set; }
        public IList<int> StudentId { get; set; }
        public Training() { Campussen = new List<Campus>(); Studenten = new List<Student>(); CampusId = new List<int>();StudentId = new List<int>();}

        public Training(string name, string description, string feed)
        {
            Name = name;
            Description = description;
            Feed = feed;
            Campussen = new List<Campus>();
            Studenten = new List<Student>();
            CampusId = new List<int>();
            StudentId = new List<int>();
        }

        public void addToCampus(Campus t)
        {
            CampusId.Add(t.CampusId);
            Campussen.Add(t);
        }
        public void addToStudents(Student t)
        {
            StudentId.Add(t.StudentId);
            Studenten.Add(t);
        }
    }
}