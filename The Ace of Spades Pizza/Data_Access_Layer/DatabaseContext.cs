using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using The_Ace_of_Spades_Pizza.Models;

namespace The_Ace_of_Spades_Pizza.Data_Access_Layer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("PizzaConnectionString")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }
    }
}