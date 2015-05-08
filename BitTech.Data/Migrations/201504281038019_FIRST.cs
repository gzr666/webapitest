namespace BitTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FIRST : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Duration = c.Double(nullable: false),
                        Description = c.String(maxLength: 1000),
                        Subject_Id = c.Int(),
                        SubjectID = c.Int(nullable: false),
                        TutorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .ForeignKey("dbo.Tutors", t => t.TutorID, cascadeDelete: true)
                .Index(t => t.Subject_Id)
                .Index(t => t.SubjectID)
                .Index(t => t.TutorID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tutors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 255),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnrollmentDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        CourseID = c.Int(),
                        StudentID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 255),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        RegistrationDate = c.DateTime(storeType: "smalldatetime"),
                        LastLoginDate = c.DateTime(storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "TutorID", "dbo.Tutors");
            DropForeignKey("dbo.Courses", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Courses", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Enrollments", new[] { "StudentID" });
            DropIndex("dbo.Enrollments", new[] { "CourseID" });
            DropIndex("dbo.Courses", new[] { "TutorID" });
            DropIndex("dbo.Courses", new[] { "SubjectID" });
            DropIndex("dbo.Courses", new[] { "Subject_Id" });
            DropTable("dbo.Students");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Tutors");
            DropTable("dbo.Subjects");
            DropTable("dbo.Courses");
        }
    }
}
