namespace The_Ace_of_Spades_Pizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        PizzaId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderCreateddDateTime = c.DateTime(nullable: false),
                        DeliveryArrivalDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.PizzaId })
                .ForeignKey("dbo.Pizzas", t => t.PizzaId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.PizzaId);
            
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        PizzaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PizzaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "PizzaId", "dbo.Pizzas");
            DropIndex("dbo.Orders", new[] { "PizzaId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropTable("dbo.Pizzas");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
