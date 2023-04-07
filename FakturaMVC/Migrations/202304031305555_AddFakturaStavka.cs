namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFakturaStavka : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FakturaStavke",
                c => new
                    {
                        FakturaStavkaId = c.Int(nullable: false, identity: true),
                        FakturaId = c.Int(nullable: false),
                        StavkaId = c.Int(nullable: false),
                        Opis = c.String(),
                        Cijena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Kolicina = c.Int(nullable: false),
                        Porez = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UkupnaCijenaBezPoreza = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.FakturaStavkaId)
                .ForeignKey("dbo.Fakture", t => t.FakturaId, cascadeDelete: true)
                .ForeignKey("dbo.Stavke", t => t.StavkaId, cascadeDelete: true)
                .Index(t => t.FakturaId)
                .Index(t => t.StavkaId);
            
            CreateTable(
                "dbo.Stavke",
                c => new
                    {
                        StavkaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Cijena = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StavkaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FakturaStavke", "StavkaId", "dbo.Stavke");
            DropForeignKey("dbo.FakturaStavke", "FakturaId", "dbo.Fakture");
            DropIndex("dbo.FakturaStavke", new[] { "StavkaId" });
            DropIndex("dbo.FakturaStavke", new[] { "FakturaId" });
            DropTable("dbo.Stavke");
            DropTable("dbo.FakturaStavke");
        }
    }
}
