using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ace_of_Spades_Pizza.Data.Model;

namespace The_Ace_of_Spades_Pizza.Data.Repository
{
    public class OrderRepository
    {
        public IEnumerable<Order> GetAll()
        {
           using (var context = new DatabaseContext())
            {
                var orders = context.Orders.Where(x => x.OrderId >= 1)
                    .Include(o => o.Customer)
                    .Include(x => x.Pizza)
                    .ToList();

                return orders;
            }            
        }

        public void Create (Order order)
        {
            using (var context = new DatabaseContext())
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }

        public void Update (Order order)
        {
            using (var context = new DatabaseContext())
            {
                context.Orders.AddOrUpdate(order);
                context.SaveChanges();
            }
        }
    }
}
