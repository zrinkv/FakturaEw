namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFakturaIdStvaratelj : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Fakture");
            AddColumn("dbo.Fakture", "FakturaId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Fakture", "StvarateljRacuna", c => c.String());
            AddPrimaryKey("dbo.Fakture", "FakturaId");
            //DropColumn("dbo.Fakture", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fakture", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Fakture");
            DropColumn("dbo.Fakture", "StvarateljRacuna");
            DropColumn("dbo.Fakture", "FakturaId");
            AddPrimaryKey("dbo.Fakture", "Id");
        }
    }
}
