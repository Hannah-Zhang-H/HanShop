namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Admin_AddTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AddingTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "AddingTime");
        }
    }
}
