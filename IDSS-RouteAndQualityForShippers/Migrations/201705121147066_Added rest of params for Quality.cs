namespace IDSS_RouteAndQualityForShippers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedrestofparamsforQuality : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ports", "ChannelDepth", c => c.String());
            AddColumn("dbo.Ports", "GoodHoldingGround", c => c.String());
            AddColumn("dbo.Ports", "PilotageAvailable", c => c.String());
            AddColumn("dbo.Ports", "TugsAssist", c => c.String());
            AddColumn("dbo.Ports", "MedicalFacilities", c => c.String());
            AddColumn("dbo.Ports", "GarbageDisposal", c => c.String());
            AddColumn("dbo.Ports", "Lift", c => c.String());
            AddColumn("dbo.Ports", "Repair", c => c.String());
            AddColumn("dbo.Ports", "DryDock", c => c.String());
            AddColumn("dbo.Ports", "QualityScore", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ports", "QualityScore");
            DropColumn("dbo.Ports", "DryDock");
            DropColumn("dbo.Ports", "Repair");
            DropColumn("dbo.Ports", "Lift");
            DropColumn("dbo.Ports", "GarbageDisposal");
            DropColumn("dbo.Ports", "MedicalFacilities");
            DropColumn("dbo.Ports", "TugsAssist");
            DropColumn("dbo.Ports", "PilotageAvailable");
            DropColumn("dbo.Ports", "GoodHoldingGround");
            DropColumn("dbo.Ports", "ChannelDepth");
        }
    }
}
