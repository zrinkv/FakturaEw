namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stavkaObaveznaPolja : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stavke", "Naziv", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stavke", "Naziv", c => c.String());
        }
    }
}
