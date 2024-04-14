namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteCategoryModelImg : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "ImgC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ImgC", c => c.String());
        }
    }
}
