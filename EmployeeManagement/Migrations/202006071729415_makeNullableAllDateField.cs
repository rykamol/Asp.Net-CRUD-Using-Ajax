namespace EmployeeManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeNullableAllDateField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "JoinDate", c => c.DateTime());
            AlterColumn("dbo.Employees", "NextReviewDate", c => c.DateTime());
            AlterColumn("dbo.Employees", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "NextReviewDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "JoinDate", c => c.DateTime(nullable: false));
        }
    }
}
