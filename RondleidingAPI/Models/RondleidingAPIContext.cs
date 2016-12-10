using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using RondleidingAPI.Models.Domain;

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

        public System.Data.Entity.DbSet<Student> Student { get; set; }
        public System.Data.Entity.DbSet<Admin> Admin { get; set; }
        public System.Data.Entity.DbSet<Training> Training { get; set; }
        public System.Data.Entity.DbSet<Campus> Campus { get; set; }
        public System.Data.Entity.DbSet<TrainingCampus> TCampus { get; set; }
        public System.Data.Entity.DbSet<StudentCampus> SCampus { get; set; }
        public System.Data.Entity.DbSet<StudentTraining> STraining { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            /*OPTIE2 XD      AH WACHT DA WERKT OOK NIET*/
            /*modelBuilder.Entity<Training>()
               .HasMany<Campus>(s => s.Campussen)
               .WithMany(c => c.Trainingen)
               .Map(cs =>
               {
                   cs.MapLeftKey("TrainingRefId");
                   cs.MapRightKey("CampusRefId");
                   cs.ToTable("TrainingCampus");
               });
            modelBuilder.Entity<Student>()
                .HasMany<Campus>(s => s.PrefCampus)
                .WithMany(c => c.Studenten)
                .Map(cs =>
                {
                    cs.MapLeftKey("StudentRefId");
                    cs.MapRightKey("CampusRefId");
                    cs.ToTable("StudentCampus");
                });
            modelBuilder.Entity<Student>()
                .HasMany<Training>(s => s.PrefTraining)
                .WithMany(c => c.Studenten)
                .Map(cs =>
                {
                    cs.MapLeftKey("StudentRefId");
                    cs.MapRightKey("TrainingRefId");
                    cs.ToTable("StudentTraining");
                });*/
        }

        public System.Data.Entity.DbSet<RondleidingAPI.Models.Domain.Post> Posts { get; set; }
    }
}
