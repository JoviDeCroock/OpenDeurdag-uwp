namespace RondleidingAPI.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RondleidingAPI.Models.RondleidingAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RondleidingAPI.Models.RondleidingAPIContext context)
        {
            context.Campus.AddOrUpdate(x => x.CampusId,
                new Campus() { CampusId = 1, Name = "HoGent Schoonmeersen", City = "Gent", Street = "Valentin Vaerwyckweg", HouseNumber = "1", Telephone = "09 243 35 60" },
                new Campus() { CampusId = 2, Name = "HoGent Aalst", City = "Aalst", Street = "Arbeidstraat", HouseNumber = "14", Telephone = "09 243 38 00" }
            );
            context.Trainings.AddOrUpdate(x => x.TrainingId,
                new Training() { TrainingId = 1, Name="Toegepaste Informatica", Description="Toekomstrichting boiz"},
                new Training() { TrainingId = 1, Name = "Toegepaste Informatica", Description = "Toekomstrichting boiz" }
            );
            context.Admins.AddOrUpdate(x => x.AdminId,
                new Admin() { AdminId = 1, Email = "admin@gmail.com", Name = "Admin", wachtwoord = "Password" }
            );
        }
    }
}
