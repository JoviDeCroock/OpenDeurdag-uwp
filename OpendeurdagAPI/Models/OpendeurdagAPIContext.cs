using OpendeurdagAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OpendeurdagAPI.Models
{
    public class OpendeurdagAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public OpendeurdagAPIContext() : base("name=OpendeurdagAPIContext")
        {
        }

        public System.Data.Entity.DbSet<OpendeurdagAPI.Models.Domain.Admin> Admins { get; set; }
        public System.Data.Entity.DbSet<OpendeurdagAPI.Models.Domain.Training> Trainings { get; set; }
        public System.Data.Entity.DbSet<OpendeurdagAPI.Models.Domain.Campus> Campus { get; set; }
        public System.Data.Entity.DbSet<OpendeurdagAPI.Models.Domain.Post> Posts { get; set; }
        public System.Data.Entity.DbSet<OpendeurdagAPI.Models.Domain.Student> Student { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Training>()
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
                });
        }
    }
}
