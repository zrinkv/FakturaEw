namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFakturaStavkaAnotacija : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fakture", "BrojFakture", c => c.String(maxLength: 15));
            AlterColumn("dbo.Fakture", "DatumStvaranja", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fakture", "DatumStvaranja", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Fakture", "BrojFakture", c => c.String());
        }
    }
}
