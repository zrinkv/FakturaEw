namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StavkaCijenaType2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stavke", "Cijena", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stavke", "Cijena", c => c.Double(nullable: false));
        }
    }
}
