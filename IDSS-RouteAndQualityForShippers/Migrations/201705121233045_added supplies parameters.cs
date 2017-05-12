namespace IDSS_RouteAndQualityForShippers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsuppliesparameters : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ports", "Provisions", c => c.String());
            AddColumn("dbo.Ports", "Water", c => c.String());
            AddColumn("dbo.Ports", "FuelOil", c => c.String());
            AddColumn("dbo.Ports", "DieselOil", c => c.String());
            AddColumn("dbo.Ports", "Deck", c => c.String());
            AddColumn("dbo.Ports", "Engine", c => c.String());
            AddColumn("dbo.Ports", "ElecRepair", c => c.String());
            AddColumn("dbo.Ports", "NavigEquip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ports", "NavigEquip");
            DropColumn("dbo.Ports", "ElecRepair");
            DropColumn("dbo.Ports", "Engine");
            DropColumn("dbo.Ports", "Deck");
            DropColumn("dbo.Ports", "DieselOil");
            DropColumn("dbo.Ports", "FuelOil");
            DropColumn("dbo.Ports", "Water");
            DropColumn("dbo.Ports", "Provisions");
        }
    }
}
