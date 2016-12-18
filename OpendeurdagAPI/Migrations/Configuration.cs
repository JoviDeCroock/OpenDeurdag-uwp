namespace OpendeurdagAPI.Migrations
{
    using Models.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    internal sealed class Configuration : DbMigrationsConfiguration<OpendeurdagAPI.Models.OpendeurdagAPIContext>
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

        protected override void Seed(OpendeurdagAPI.Models.OpendeurdagAPIContext context)
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

            campussenObj[0].Trainingen.Add(trainingenObj[0]);
            campussenObj[0].TrainingIds.Add(trainingenObj[0].TrainingId);
            campussenObj[0].Trainingen.Add(trainingenObj[1]);
            campussenObj[0].TrainingIds.Add(trainingenObj[1].TrainingId);
            campussenObj[0].Trainingen.Add(trainingenObj[2]);
            campussenObj[0].TrainingIds.Add(trainingenObj[2].TrainingId);
            campussenObj[0].Trainingen.Add(trainingenObj[3]);
            campussenObj[0].TrainingIds.Add(trainingenObj[3].TrainingId);
            campussenObj[1].Trainingen.Add(trainingenObj[0]);
            campussenObj[1].TrainingIds.Add(trainingenObj[0].TrainingId);
            campussenObj[1].Trainingen.Add(trainingenObj[1]);
            campussenObj[1].TrainingIds.Add(trainingenObj[1].TrainingId);
            campussenObj[1].Trainingen.Add(trainingenObj[3]);
            campussenObj[1].TrainingIds.Add(trainingenObj[3].TrainingId);

            foreach (Campus c in campussenObj)
            {
                context.Campus.AddOrUpdate(c);
            }

            context.Admins.AddOrUpdate(x => x.AdminId,
                new Admin() { AdminId = 1, Email = "admin@gmail.com", Name = "Admin", Password = encode("Password") }
            );

            context.Posts.AddOrUpdate(x => x.PostId,
                new Post() { PostId = 1, Title = "WELKOM!", Text = "Welkom bij de coolste hogeschool evar!" }
             );
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }
            context.SaveChanges();
        }
    }
}
