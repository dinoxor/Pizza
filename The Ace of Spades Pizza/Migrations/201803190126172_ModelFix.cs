namespace The_Ace_of_Spades_Pizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelFix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Orders");
            AddPrimaryKey("dbo.Orders", new[] { "CustomerId", "PizzaId" });
            DropColumn("dbo.Orders", "OrderId");
            DropColumn("dbo.Orders", "EstimatedDeliverDatetime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "EstimatedDeliverDatetime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Orders");
            AddPrimaryKey("dbo.Orders", "OrderId");
        }
    }
}
