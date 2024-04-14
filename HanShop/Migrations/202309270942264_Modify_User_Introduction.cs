namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_User_Introduction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Introduction", c => c.String());
            DropColumn("dbo.Users", "Indroduction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Indroduction", c => c.String());
            DropColumn("dbo.Users", "Introduction");
        }
    }
}
