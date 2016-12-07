namespace RondleidingAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentCampus", "StudentRefId", "dbo.Students");
            DropForeignKey("dbo.StudentCampus", "CampusRefId", "dbo.Campus");
            DropForeignKey("dbo.TrainingCampus", "TrainingRefId", "dbo.Trainings");
            DropForeignKey("dbo.TrainingCampus", "CampusRefId", "dbo.Campus");
            DropForeignKey("dbo.StudentTraining", "StudentRefId", "dbo.Students");
            DropForeignKey("dbo.StudentTraining", "TrainingRefId", "dbo.Trainings");
            DropIndex("dbo.StudentCampus", new[] { "StudentRefId" });
            DropIndex("dbo.StudentCampus", new[] { "CampusRefId" });
            DropIndex("dbo.TrainingCampus", new[] { "TrainingRefId" });
            DropIndex("dbo.TrainingCampus", new[] { "CampusRefId" });
            DropIndex("dbo.StudentTraining", new[] { "StudentRefId" });
            DropIndex("dbo.StudentTraining", new[] { "TrainingRefId" });
            CreateTable(
                "dbo.StudentCampus",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        CampusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.CampusId })
                .ForeignKey("dbo.Campus", t => t.CampusId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CampusId);
            
            CreateTable(
                "dbo.StudentTrainings",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        TrainingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.TrainingId })
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.TrainingId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.TrainingId);
            
            CreateTable(
                "dbo.TrainingCampus",
                c => new
                    {
                        TrainingsId = c.Int(nullable: false),
                        CampusId = c.Int(nullable: false),
                        Training_TrainingId = c.Int(),
                    })
                .PrimaryKey(t => new { t.TrainingsId, t.CampusId })
                .ForeignKey("dbo.Campus", t => t.CampusId, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.Training_TrainingId)
                .Index(t => t.CampusId)
                .Index(t => t.Training_TrainingId);
            
            DropTable("dbo.StudentCampus");
            DropTable("dbo.TrainingCampus");
            DropTable("dbo.StudentTraining");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudentTraining",
                c => new
                    {
                        StudentRefId = c.Int(nullable: false),
                        TrainingRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentRefId, t.TrainingRefId });
            
            CreateTable(
                "dbo.TrainingCampus",
                c => new
                    {
                        TrainingRefId = c.Int(nullable: false),
                        CampusRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainingRefId, t.CampusRefId });
            
            CreateTable(
                "dbo.StudentCampus",
                c => new
                    {
                        StudentRefId = c.Int(nullable: false),
                        CampusRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentRefId, t.CampusRefId });
            
            DropForeignKey("dbo.StudentTrainings", "TrainingId", "dbo.Trainings");
            DropForeignKey("dbo.TrainingCampus", "Training_TrainingId", "dbo.Trainings");
            DropForeignKey("dbo.TrainingCampus", "CampusId", "dbo.Campus");
            DropForeignKey("dbo.StudentTrainings", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCampus", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCampus", "CampusId", "dbo.Campus");
            DropIndex("dbo.TrainingCampus", new[] { "Training_TrainingId" });
            DropIndex("dbo.TrainingCampus", new[] { "CampusId" });
            DropIndex("dbo.StudentTrainings", new[] { "TrainingId" });
            DropIndex("dbo.StudentTrainings", new[] { "StudentId" });
            DropIndex("dbo.StudentCampus", new[] { "CampusId" });
            DropIndex("dbo.StudentCampus", new[] { "StudentId" });
            DropTable("dbo.TrainingCampus");
            DropTable("dbo.StudentTrainings");
            DropTable("dbo.StudentCampus");
            CreateIndex("dbo.StudentTraining", "TrainingRefId");
            CreateIndex("dbo.StudentTraining", "StudentRefId");
            CreateIndex("dbo.TrainingCampus", "CampusRefId");
            CreateIndex("dbo.TrainingCampus", "TrainingRefId");
            CreateIndex("dbo.StudentCampus", "CampusRefId");
            CreateIndex("dbo.StudentCampus", "StudentRefId");
            AddForeignKey("dbo.StudentTraining", "TrainingRefId", "dbo.Trainings", "TrainingId", cascadeDelete: true);
            AddForeignKey("dbo.StudentTraining", "StudentRefId", "dbo.Students", "StudentId", cascadeDelete: true);
            AddForeignKey("dbo.TrainingCampus", "CampusRefId", "dbo.Campus", "CampusId", cascadeDelete: true);
            AddForeignKey("dbo.TrainingCampus", "TrainingRefId", "dbo.Trainings", "TrainingId", cascadeDelete: true);
            AddForeignKey("dbo.StudentCampus", "CampusRefId", "dbo.Campus", "CampusId", cascadeDelete: true);
            AddForeignKey("dbo.StudentCampus", "StudentRefId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
    }
}
