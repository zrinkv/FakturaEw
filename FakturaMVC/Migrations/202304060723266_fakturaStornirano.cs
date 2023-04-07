namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fakturaStornirano : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fakture", "Stornirano", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fakture", "Stornirano");
        }
    }
}
