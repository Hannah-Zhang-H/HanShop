namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserAddressControllerNotes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAddresses", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAddresses", "Notes", c => c.String(maxLength: 100));
        }
    }
}
