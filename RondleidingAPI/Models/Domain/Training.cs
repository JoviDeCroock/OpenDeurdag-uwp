using RondleidingAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models
{
    public class Training
    {
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<TrainingCampus> Campussen { get; set; }
        public string Feed { get; set; }
        public virtual IList<StudentTraining> Studenten { get; set; }
        public Training() { Campussen = new List<TrainingCampus>(); Studenten = new List<StudentTraining>(); }

        public Training(string name, string description, string feed)
        {
            Name = name;
            Description = description;
            Feed = feed;
            Campussen = new List<TrainingCampus>();
            Studenten = new List<StudentTraining>();
        }

        public void addToCampus(TrainingCampus t)
        {
            t.Campus.addToTraining(t);
            Campussen.Add(t);
        }
        public void addToStudents(StudentTraining t)
        {
            t.Student.addTraining(t);
            Studenten.Add(t);
        }
    }
}