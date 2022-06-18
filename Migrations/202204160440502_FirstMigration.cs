namespace _1265125_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.EmploymentInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Designation = c.String(),
                        Salary = c.Double(nullable: false),
                        ReferenceName = c.String(),
                        ReferencePhone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.PersonalInfo", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.PersonalInfo",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DoB = c.DateTime(nullable: false),
                        Gender = c.String(),
                        Phone = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        DivisionId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.District", t => t.DistrictId)
                .ForeignKey("dbo.Division", t => t.DivisionId)
                .Index(t => t.DivisionId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        DistrictId = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistrictId)
                .ForeignKey("dbo.Division", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.Division",
                c => new
                    {
                        DivisionId = c.Int(nullable: false, identity: true),
                        DivName = c.String(),
                    })
                .PrimaryKey(t => t.DivisionId);
            
            CreateTable(
                "dbo.EducationalInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        HighestDegree = c.String(),
                        Subject = c.String(),
                        PassingYear = c.Int(nullable: false),
                        CGPA = c.Int(nullable: false),
                        Training = c.String(),
                        Duration = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalInfo", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmploymentInfo", "EmployeeId", "dbo.PersonalInfo");
            DropForeignKey("dbo.EducationalInfo", "EmployeeId", "dbo.PersonalInfo");
            DropForeignKey("dbo.PersonalInfo", "DivisionId", "dbo.Division");
            DropForeignKey("dbo.PersonalInfo", "DistrictId", "dbo.District");
            DropForeignKey("dbo.District", "DivisionId", "dbo.Division");
            DropForeignKey("dbo.EmploymentInfo", "DepartmentId", "dbo.Department");
            DropIndex("dbo.EducationalInfo", new[] { "EmployeeId" });
            DropIndex("dbo.District", new[] { "DivisionId" });
            DropIndex("dbo.PersonalInfo", new[] { "DistrictId" });
            DropIndex("dbo.PersonalInfo", new[] { "DivisionId" });
            DropIndex("dbo.EmploymentInfo", new[] { "DepartmentId" });
            DropIndex("dbo.EmploymentInfo", new[] { "EmployeeId" });
            DropTable("dbo.EducationalInfo");
            DropTable("dbo.Division");
            DropTable("dbo.District");
            DropTable("dbo.PersonalInfo");
            DropTable("dbo.EmploymentInfo");
            DropTable("dbo.Department");
        }
    }
}
