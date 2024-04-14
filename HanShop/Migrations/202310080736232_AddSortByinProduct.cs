namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSortByinProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SortBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SortBy");
        }
    }
}
