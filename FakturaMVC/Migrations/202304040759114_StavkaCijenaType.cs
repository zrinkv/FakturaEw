namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StavkaCijenaType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stavke", "Cijena", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stavke", "Cijena", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
