namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminPowercanbenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "Power", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "Power", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
