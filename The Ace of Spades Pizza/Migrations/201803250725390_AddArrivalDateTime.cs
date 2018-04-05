namespace The_Ace_of_Spades_Pizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArrivalDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DeliveryArrivalDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DeliveryArrivalDateTime");
        }
    }
}
