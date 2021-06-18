namespace EmployeeManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DeptId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeptCode = c.String(),
                    })
                .PrimaryKey(t => t.DeptId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Designation = c.String(),
                        JoinDate = c.DateTime(),
                        CurrentSalary = c.Double(nullable: false),
                        Department = c.String(),
                        NextReviewDate = c.DateTime(),
                        DateOfBirth = c.DateTime(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
