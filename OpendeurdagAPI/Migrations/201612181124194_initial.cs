namespace OpendeurdagAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        Name = c.String(),
                        Email = c.String(),
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
                        Feed = c.String(),
                    })
                .PrimaryKey(t => t.CampusId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Training",
                c => new
                    {
                        TrainingId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Feed = c.String(),
                    })
                .PrimaryKey(t => t.TrainingId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.StudentCampus",
                c => new
                    {
                        StudentRefId = c.Int(nullable: false),
                        CampusRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentRefId, t.CampusRefId })
                .ForeignKey("dbo.Student", t => t.StudentRefId, cascadeDelete: true)
                .ForeignKey("dbo.Campus", t => t.CampusRefId, cascadeDelete: true)
                .Index(t => t.StudentRefId)
                .Index(t => t.CampusRefId);
            
            CreateTable(
                "dbo.TrainingCampus",
                c => new
                    {
                        TrainingRefId = c.Int(nullable: false),
                        CampusRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainingRefId, t.CampusRefId })
                .ForeignKey("dbo.Training", t => t.TrainingRefId, cascadeDelete: true)
                .ForeignKey("dbo.Campus", t => t.CampusRefId, cascadeDelete: true)
                .Index(t => t.TrainingRefId)
                .Index(t => t.CampusRefId);
            
            CreateTable(
                "dbo.StudentTraining",
                c => new
                    {
                        StudentRefId = c.Int(nullable: false),
                        TrainingRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentRefId, t.TrainingRefId })
                .ForeignKey("dbo.Student", t => t.StudentRefId, cascadeDelete: true)
                .ForeignKey("dbo.Training", t => t.TrainingRefId, cascadeDelete: true)
                .Index(t => t.StudentRefId)
                .Index(t => t.TrainingRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentTraining", "TrainingRefId", "dbo.Training");
            DropForeignKey("dbo.StudentTraining", "StudentRefId", "dbo.Student");
            DropForeignKey("dbo.TrainingCampus", "CampusRefId", "dbo.Campus");
            DropForeignKey("dbo.TrainingCampus", "TrainingRefId", "dbo.Training");
            DropForeignKey("dbo.StudentCampus", "CampusRefId", "dbo.Campus");
            DropForeignKey("dbo.StudentCampus", "StudentRefId", "dbo.Student");
            DropIndex("dbo.StudentTraining", new[] { "TrainingRefId" });
            DropIndex("dbo.StudentTraining", new[] { "StudentRefId" });
            DropIndex("dbo.TrainingCampus", new[] { "CampusRefId" });
            DropIndex("dbo.TrainingCampus", new[] { "TrainingRefId" });
            DropIndex("dbo.StudentCampus", new[] { "CampusRefId" });
            DropIndex("dbo.StudentCampus", new[] { "StudentRefId" });
            DropTable("dbo.StudentTraining");
            DropTable("dbo.TrainingCampus");
            DropTable("dbo.StudentCampus");
            DropTable("dbo.Post");
            DropTable("dbo.Training");
            DropTable("dbo.Student");
            DropTable("dbo.Campus");
            DropTable("dbo.Admin");
        }
    }
}
