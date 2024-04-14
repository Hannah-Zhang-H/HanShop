namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCheckingUserModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Username", c => c.String());
        }
    }
}
