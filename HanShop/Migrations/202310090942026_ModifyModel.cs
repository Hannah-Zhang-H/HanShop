﻿namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "SortBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "SortBy", c => c.String());
        }
    }
}
