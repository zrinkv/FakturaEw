namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFakturaRenameV2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Faktura", newName: "Fakture");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Fakture", newName: "Faktura");
        }
    }
}
