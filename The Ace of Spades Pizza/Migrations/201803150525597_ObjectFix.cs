namespace The_Ace_of_Spades_Pizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ObjectFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pizzas", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Pizzas_PizzaId", "dbo.Pizzas");
            DropIndex("dbo.Pizzas", new[] { "Customer_CustomerId" });
            DropIndex("dbo.Orders", new[] { "Pizzas_PizzaId" });
            RenameColumn(table: "dbo.Orders", name: "Pizzas_PizzaId", newName: "PizzaId");
            AlterColumn("dbo.Orders", "PizzaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "PizzaId");
            AddForeignKey("dbo.Orders", "PizzaId", "dbo.Pizzas", "PizzaId", cascadeDelete: true);
            DropColumn("dbo.Pizzas", "Customer_CustomerId");
            DropColumn("dbo.Orders", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "StudentId", c => c.Int(nullable: false));
            AddColumn("dbo.Pizzas", "Customer_CustomerId", c => c.Int());
            DropForeignKey("dbo.Orders", "PizzaId", "dbo.Pizzas");
            DropIndex("dbo.Orders", new[] { "PizzaId" });
            AlterColumn("dbo.Orders", "PizzaId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "PizzaId", newName: "Pizzas_PizzaId");
            CreateIndex("dbo.Orders", "Pizzas_PizzaId");
            CreateIndex("dbo.Pizzas", "Customer_CustomerId");
            AddForeignKey("dbo.Orders", "Pizzas_PizzaId", "dbo.Pizzas", "PizzaId");
            AddForeignKey("dbo.Pizzas", "Customer_CustomerId", "dbo.Customers", "CustomerId");
        }
    }
}
