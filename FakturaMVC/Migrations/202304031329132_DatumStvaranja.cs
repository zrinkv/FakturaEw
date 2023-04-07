﻿namespace FakturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatumStvaranja : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fakture", "DatumStvaranja", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fakture", "DatumStvaranja", c => c.DateTime(nullable: false));
        }
    }
}
