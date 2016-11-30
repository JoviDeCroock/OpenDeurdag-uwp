using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models
{
    public class RondleidingAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RondleidingAPIContext() : base("name=RondleidingAPIContext")
        {
        }

        public System.Data.Entity.DbSet<RondleidingAPI.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<RondleidingAPI.Models.Admin> Admins { get; set; }

        public System.Data.Entity.DbSet<RondleidingAPI.Models.Training> Trainings { get; set; }

        public System.Data.Entity.DbSet<RondleidingAPI.Models.Campus> Campus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Training>()
               .HasMany<Campus>(s => s.Campussen)
               .WithMany(c => c.Trainingen)
               .Map(cs =>
               {
                   cs.MapLeftKey("TrainingRefId");
                   cs.MapRightKey("CampusRefId");
                   cs.ToTable("TrainingCampus");
               });       
        }
   }
}
