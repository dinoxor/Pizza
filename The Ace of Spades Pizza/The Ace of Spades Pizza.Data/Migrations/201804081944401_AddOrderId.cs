namespace The_Ace_of_Spades_Pizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "PizzaId", "dbo.Pizzas");
            DropPrimaryKey("dbo.Orders");
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "OrderId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Orders", "PizzaId", "dbo.Pizzas", "PizzaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "PizzaId", "dbo.Pizzas");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropPrimaryKey("dbo.Orders");
            DropColumn("dbo.Orders", "OrderId");
            AddPrimaryKey("dbo.Orders", new[] { "CustomerId", "PizzaId" });
            AddForeignKey("dbo.Orders", "PizzaId", "dbo.Pizzas", "PizzaId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
    }
}
