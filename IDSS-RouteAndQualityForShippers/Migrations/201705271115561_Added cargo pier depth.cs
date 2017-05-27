namespace IDSS_RouteAndQualityForShippers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcargopierdepth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ports", "CargoDepth", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ports", "CargoDepth");
        }
    }
}
