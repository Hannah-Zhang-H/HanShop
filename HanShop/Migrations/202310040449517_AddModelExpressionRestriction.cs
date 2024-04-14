namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelExpressionRestriction : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Admins", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "Power", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Products", "SalePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ProductCode", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Reviews", "ReviewContent", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Nickname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserAddresses", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.UserAddresses", "ReceivingName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserAddresses", "Phone", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.UserAddresses", "Notes", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAddresses", "Notes", c => c.String());
            AlterColumn("dbo.UserAddresses", "Phone", c => c.String());
            AlterColumn("dbo.UserAddresses", "ReceivingName", c => c.String());
            AlterColumn("dbo.UserAddresses", "Address", c => c.String());
            AlterColumn("dbo.Users", "Nickname", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Reviews", "ReviewContent", c => c.String());
            AlterColumn("dbo.Products", "ProductCode", c => c.String());
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "SalePrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Admins", "Power", c => c.String());
            AlterColumn("dbo.Admins", "Password", c => c.String());
            AlterColumn("dbo.Admins", "Name", c => c.String());
        }
    }
}
