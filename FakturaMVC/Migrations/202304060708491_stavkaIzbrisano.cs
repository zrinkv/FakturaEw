namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stavkaIzbrisano : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stavke", "Izbrisano", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stavke", "Izbrisano");
        }
    }
}
