namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductViewModel_ID", "dbo.Products");
            DropIndex("dbo.Products", new[] { "ProductViewModel_ID" });
            DropColumn("dbo.Products", "Discriminator");
            DropColumn("dbo.Products", "ProductViewModel_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductViewModel_ID", c => c.Int());
            AddColumn("dbo.Products", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Products", "ProductViewModel_ID");
            AddForeignKey("dbo.Products", "ProductViewModel_ID", "dbo.Products", "ID");
        }
    }
}
