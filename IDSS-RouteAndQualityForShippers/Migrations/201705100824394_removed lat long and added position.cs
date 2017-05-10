namespace IDSS_RouteAndQualityForShippers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedlatlongandaddedposition : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ports", "LatDegrees");
            DropColumn("dbo.Ports", "LatMinutes");
            DropColumn("dbo.Ports", "LatHemisphere");
            DropColumn("dbo.Ports", "LongDegrees");
            DropColumn("dbo.Ports", "LongMinutes");
            DropColumn("dbo.Ports", "LongHemisphere");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ports", "LongHemisphere", c => c.String());
            AddColumn("dbo.Ports", "LongMinutes", c => c.Int(nullable: false));
            AddColumn("dbo.Ports", "LongDegrees", c => c.Int(nullable: false));
            AddColumn("dbo.Ports", "LatHemisphere", c => c.String());
            AddColumn("dbo.Ports", "LatMinutes", c => c.Int(nullable: false));
            AddColumn("dbo.Ports", "LatDegrees", c => c.Int(nullable: false));
        }
    }
}
