namespace The_Ace_of_Spades_Pizza.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using The_Ace_of_Spades_Pizza.Data.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<The_Ace_of_Spades_Pizza.Data.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(The_Ace_of_Spades_Pizza.Data.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var pizzas = new List<Pizza>();

            var pizza1 = new Pizza { Name = "The One", Description = "Delicious pizza with pepperoni and pineapple" };
            var pizza2 = new Pizza { Name = "Hawaiian", Description = "Pineappls and onion toppings" };
            var pizza3 = new Pizza { Name = "Deluxe", Description = "Bell peppers, onion, olive, mushroom and beef toppings" };
            var pizza4 = new Pizza { Name = "Meat Lover", Description = "Pepperoni, ham, and beef toppings" };
            var pizza5 = new Pizza { Name = "Veggie Lover", Description = "Bell peppers, onion, olive, spinach, brocolli, and mushroom toppings" };

            pizzas.Add(pizza1);
            pizzas.Add(pizza2);
            pizzas.Add(pizza3);
            pizzas.Add(pizza4);
            pizzas.Add(pizza5);

            pizzas.ForEach(x => context.Pizzas.AddOrUpdate(y => y.Name, x));
            //context.Pizzas.AddRange(pizzas);

            var customer1 = new Customer { FirstName = "First", LastName = "Guy", PhoneNumber = "1234567890" };
            var customer2 = new Customer { FirstName = "Second", LastName = "Guy", PhoneNumber = "2222222222" };

            var customers = new List<Customer>();
            customers.Add(customer1);
            customers.Add(customer2);

            customers.ForEach(x => context.Customers.AddOrUpdate(y => y.FirstName, x));
            //context.Customers.AddRange(customers);

            var orders = new List<Order>
            {
                new Order {Customer = customer1, OrderCreateddDateTime = DateTime.Now, DeliveryArrivalDateTime = DateTime.Now.AddHours(1), Pizza = pizza1, Quantity = 2},
                new Order {Customer = customer1, OrderCreateddDateTime = DateTime.Now, DeliveryArrivalDateTime = DateTime.Now.AddHours(1), Pizza = pizza2},
                new Order {Customer = customer2, OrderCreateddDateTime = DateTime.Now, DeliveryArrivalDateTime = DateTime.Now.AddHours(1), Pizza = pizza3},
                new Order {Customer = customer2, OrderCreateddDateTime = DateTime.Now, DeliveryArrivalDateTime = DateTime.Now.AddHours(1), Pizza = pizza4},
            };

            context.Orders.AddRange(orders);
        }
    }
}
