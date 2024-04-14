namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductIntroductionRegularExpressionRule : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAddresses", "Notes", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAddresses", "Notes", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
