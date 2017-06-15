namespace WeedHackers_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Timestamp", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AddColumn("dbo.Users", "Timestamp", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AddColumn("dbo.Customers", "Timestamp", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Timestamp");
            DropColumn("dbo.Users", "Timestamp");
            DropColumn("dbo.Employees", "Timestamp");
        }
    }
}
