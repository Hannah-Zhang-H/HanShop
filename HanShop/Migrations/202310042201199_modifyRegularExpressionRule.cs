namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyRegularExpressionRule : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductCode", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
