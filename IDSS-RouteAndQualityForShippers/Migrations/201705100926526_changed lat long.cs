namespace IDSS_RouteAndQualityForShippers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class changedlatlong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ports", "Latitude", c => c.String());
            AddColumn("dbo.Ports", "Longitude", c => c.String());
            DropColumn("dbo.Ports", "Position");
        }

        public override void Down()
        {
            AddColumn("dbo.Ports", "Position", c => c.String());
            DropColumn("dbo.Ports", "Longitude");
            DropColumn("dbo.Ports", "Latitude");
        }
    }
}
