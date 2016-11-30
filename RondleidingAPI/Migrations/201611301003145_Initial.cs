namespace RondleidingAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        wachtwoord = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Campus",
                c => new
                    {
                        CampusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        HouseNumber = c.String(),
                        Street = c.String(),
                        Telephone = c.String(),
                        Student_StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.CampusId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .Index(t => t.Student_StudentId);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        TrainingId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Student_StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.TrainingId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .Index(t => t.Student_StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.TrainingCampus",
                c => new
                    {
                        TrainingRefId = c.Int(nullable: false),
                        CampusRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainingRefId, t.CampusRefId })
                .ForeignKey("dbo.Trainings", t => t.TrainingRefId, cascadeDelete: true)
                .ForeignKey("dbo.Campus", t => t.CampusRefId, cascadeDelete: true)
                .Index(t => t.TrainingRefId)
                .Index(t => t.CampusRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainings", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.Campus", "Student_StudentId", "dbo.Students");
            DropForeignKey("dbo.TrainingCampus", "CampusRefId", "dbo.Campus");
            DropForeignKey("dbo.TrainingCampus", "TrainingRefId", "dbo.Trainings");
            DropIndex("dbo.TrainingCampus", new[] { "CampusRefId" });
            DropIndex("dbo.TrainingCampus", new[] { "TrainingRefId" });
            DropIndex("dbo.Trainings", new[] { "Student_StudentId" });
            DropIndex("dbo.Campus", new[] { "Student_StudentId" });
            DropTable("dbo.TrainingCampus");
            DropTable("dbo.Students");
            DropTable("dbo.Trainings");
            DropTable("dbo.Campus");
            DropTable("dbo.Admins");
        }
    }
}
