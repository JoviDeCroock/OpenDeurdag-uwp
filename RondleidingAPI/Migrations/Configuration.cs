namespace RondleidingAPI.Migrations
{
    using Models;
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
            var campussen = new[]
            {
                new Campus() { CampusId = 1, Name = "HoGent Schoonmeersen", City = "Gent", Street = "Valentin Vaerwyckweg", HouseNumber = "1", Telephone = "09 243 35 60" },
                new Campus() { CampusId = 2, Name = "HoGent Aalst", City = "Aalst", Street = "Arbeidstraat", HouseNumber = "14", Telephone = "09 243 38 00" }
            };
            var trainingen = new[]
            {
                new Training() { TrainingId = 1, Name="Toegepaste Informatica", Description="Toekomstrichting boiz"}
            };
            foreach(Campus c in campussen)
            {
                if(c.Trainingen == null)
                {
                    c.Trainingen = new List<Training>();
                }
                c.Trainingen.Add(trainingen[0]);
            }
            foreach(Training t in trainingen)
            {
                if(t.Campussen == null)
                {
                    t.Campussen = new List<Campus>();
                }
                t.Campussen.Add(campussen[0]);
            }

            foreach (Campus c in campussen)
            {
                context.Campus.AddOrUpdate(c);
            }

            context.SaveChanges();
            foreach (Training t in trainingen)
            {
                context.Trainings.AddOrUpdate(t);
            }

            context.Admins.AddOrUpdate(x => x.AdminId,
                new Admin() { AdminId = 1, Email = "admin@gmail.com", Name = "Admin", Password = encode("Password") }
            );
            context.SaveChanges();
        }
    }
}
