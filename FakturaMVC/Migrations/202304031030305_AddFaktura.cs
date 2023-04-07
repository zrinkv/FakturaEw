namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFaktura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faktura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrojFakture = c.String(),
                        DatumStvaranja = c.DateTime(nullable: false),
                        DatumDospijeca = c.DateTime(nullable: false),
                        UkupnaCijenaBezPoreza = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UkupnaCijenaSPorezom = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NazivPrimateljaRacuna = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Faktura");
        }
    }
}
