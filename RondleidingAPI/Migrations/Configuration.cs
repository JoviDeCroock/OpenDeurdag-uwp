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
            AutomaticMigrationsEnabled = false;
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
                new Training() { TrainingId = 1, Name="Toegepaste Informatica", Description="Ieksdé Toekomstrichting boiz", Feed = "hogenttoegepasteinformatica"}
            };

            var trainingen = new[]
            {
                new TrainingCampus() {Training = trainingenObj[0], Campus= campussenObj[0]},
                new TrainingCampus() {Training = trainingenObj[0], Campus= campussenObj[1]}
            };

            foreach(TrainingCampus t in trainingen)
            {
                context.TCampus.AddOrUpdate(t);
            }

            foreach(Campus c in campussenObj)
            {
                if(c.Trainingen == null)
                {
                    c.Trainingen = new List<TrainingCampus>();
                }
                c.addToTraining(trainingen[0]);
            }
            foreach(Training t in trainingenObj)
            {
                if(t.Campussen == null)
                {
                    t.Campussen = new List<TrainingCampus>();
                }
                t.addToCampus(trainingen[0]);

            }

            foreach (Campus c in campussenObj)
            {
                context.Campus.AddOrUpdate(c);
                context.SaveChanges();
            }

            context.SaveChanges();
            foreach (Training t in trainingenObj)
            {
                context.Trainings.AddOrUpdate(t);
                context.SaveChanges();
            }

            context.Admins.AddOrUpdate(x => x.AdminId,
                new Admin() { AdminId = 1, Email = "admin@gmail.com", Name = "Admin", Password = encode("Password") }
            );

            context.SaveChanges();
        }
    }
}
