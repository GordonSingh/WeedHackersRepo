namespace WeedHackers_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTotalToServiceRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "Total", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "Total");
        }
    }
}
