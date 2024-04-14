namespace HanShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Order_OrderDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        OrderNumber = c.String(),
                        SumPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(),
                        CreatingTime = c.DateTime(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        OrderState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.Orders");
        }
    }
}
