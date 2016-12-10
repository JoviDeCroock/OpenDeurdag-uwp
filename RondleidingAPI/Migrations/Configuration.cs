namespace RondleidingAPI.Migrations
{
    using Models;
    using Models.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<RondleidingAPI.Models.RondleidingAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        private string encode(string pw)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(pw));
            return Convert.ToBase64String(hashedDataBytes);
        }

        protected override void Seed(RondleidingAPI.Models.RondleidingAPIContext context)
        {
            var campussenObj = new[]
            {
                new Campus() { CampusId = 1, Name = "HoGent Schoonmeersen", City = "Gent", Street = "Valentin Vaerwyckweg", HouseNumber = "1", Telephone = "09 243 35 60", Feed = "Hogeschool-Gent-Campus-Schoonmeersen" },
                new Campus() { CampusId = 2, Name = "HoGent Aalst", City = "Aalst", Street = "Arbeidstraat", HouseNumber = "14", Telephone = "09 243 38 00", Feed = "HoGentCampusAalst" }
            };
            var trainingenObj = new[]
            {
                new Training() { TrainingId = 1, Name="Toegepaste Informatica", Description="Toekomstrichting boiz", Feed = "hogenttoegepasteinformatica"},
                new Training() { TrainingId = 2, Name="Bedrijfsmanagement", Description="Bedrijfsrichting boiz", Feed = "hogentbedrijfsmanagement"},
                new Training() { TrainingId = 3, Name="Retail management", Description="Retailrichthing boiz", Feed = "hogentretailmanagement"},
                new Training() { TrainingId = 4, Name="Office management", Description="Management boiz", Feed = "hogentofficemanagement"}
            };

            var trainingen = new[]
            {
                /*Richtingen in beide campussen*/
                new TrainingCampus() {Training = trainingenObj[0], Campus= campussenObj[0]},
                new TrainingCampus() {Training = trainingenObj[0], Campus= campussenObj[1]},
                new TrainingCampus() {Training = trainingenObj[1], Campus= campussenObj[0]},        
                new TrainingCampus() {Training = trainingenObj[1], Campus= campussenObj[1]},
                new TrainingCampus() {Training = trainingenObj[3], Campus= campussenObj[0]},
                new TrainingCampus() {Training = trainingenObj[3], Campus= campussenObj[1]},
                /*Richting alleen in Aalst*/
                new TrainingCampus() {Training = trainingenObj[2], Campus= campussenObj[0]}
            };

            foreach(TrainingCampus t in trainingen)
            {
                context.TCampus.AddOrUpdate(t);
            }
            if (campussenObj[0].Trainingen == null)
            {
                campussenObj[0].Trainingen = new List<TrainingCampus>();
            }
            if (campussenObj[1].Trainingen == null)
            {
                campussenObj[1].Trainingen = new List<TrainingCampus>();
            }
            campussenObj[0].addToTraining(trainingen[0]);
            campussenObj[0].addToTraining(trainingen[2]);
            campussenObj[0].addToTraining(trainingen[4]);
            campussenObj[0].addToTraining(trainingen[6]);
            campussenObj[1].addToTraining(trainingen[1]);
            campussenObj[1].addToTraining(trainingen[3]);
            campussenObj[1].addToTraining(trainingen[5]);
            if (trainingenObj[0].Campussen == null)
            {
                trainingenObj[0].Campussen = new List<TrainingCampus>();
            }
            if (trainingenObj[1].Campussen == null)
            {
                trainingenObj[1].Campussen = new List<TrainingCampus>();
            }
            trainingenObj[0].addToCampus(trainingen[0]);
            trainingenObj[0].addToCampus(trainingen[1]);
            trainingenObj[1].addToCampus(trainingen[2]);
            trainingenObj[1].addToCampus(trainingen[3]);
            trainingenObj[3].addToCampus(trainingen[4]);
            trainingenObj[3].addToCampus(trainingen[5]);
            trainingenObj[2].addToCampus(trainingen[6]);

            foreach (Campus c in campussenObj)
            {
                context.Campus.AddOrUpdate(c);
            }
            foreach (Training t in trainingenObj)
            {
                context.Training.AddOrUpdate(t);
            }

            context.Admin.AddOrUpdate(x => x.AdminId,
                new Admin() { AdminId = 1, Email = "admin@gmail.com", Name = "Admin", Password = encode("Password") }
            );

            context.Posts.AddOrUpdate(x => x.PostId,
                new Post() { PostId = 1, Title="WELKOM!", Text="Welkom bij de coolste hogeschool evar!" }   
             );

            context.SaveChanges();
        }
    }
}
