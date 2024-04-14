namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_imgC_Category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ImgC", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ImgC");
        }
    }
}
