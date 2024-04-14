namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserCentreController : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        ReceivingName = c.String(),
                        Phone = c.String(),
                        Notes = c.String(),
                        CreatingTime = c.DateTime(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAddresses", "UserId", "dbo.Users");
            DropIndex("dbo.UserAddresses", new[] { "UserId" });
            DropTable("dbo.UserAddresses");
        }
    }
}
