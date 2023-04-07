namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFakturaRename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Fakturas", newName: "Faktura");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Faktura", newName: "Fakturas");
        }
    }
}
