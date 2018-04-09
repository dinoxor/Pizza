using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ace_of_Spades_Pizza.Data.Model;

namespace The_Ace_of_Spades_Pizza.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext() : base("PizzaConnectionString")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                 .HasRequired(x => x.Customer)
                 .WithMany(x => x.Orders)
                 .HasForeignKey(x => x.CustomerId)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                 .HasRequired(x => x.Pizza)
                 .WithMany(x => x.Orders)
                 .HasForeignKey(x => x.PizzaId)
                 .WillCascadeOnDelete(false);
        }
    }
}
