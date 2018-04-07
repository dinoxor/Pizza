namespace The_Ace_of_Spades_Pizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                "dbo.Pizzas",
                c => new
                    {
                        PizzaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.PizzaId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .Index(t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderCreateddDateTime = c.DateTime(nullable: false),
                        EstimatedDeliverDatetime = c.DateTime(nullable: false),
                        Pizzas_PizzaId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Pizzas", t => t.Pizzas_PizzaId)
                .Index(t => t.CustomerId)
                .Index(t => t.Pizzas_PizzaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Pizzas_PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Pizzas", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Pizzas_PizzaId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Pizzas", new[] { "Customer_CustomerId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Pizzas");
            DropTable("dbo.Customers");
        }
    }
}
