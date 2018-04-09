using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ace_of_Spades_Pizza.Data.Model;

namespace The_Ace_of_Spades_Pizza.Data.Repository
{
    public class CustomerRepository
    {
        public IEnumerable<Customer> GetAll()
        {
            using (var context = new DatabaseContext())
            {
                return context.Customers.ToList();
            }
        }

        public void Create(Customer newCustomer)
        {
            using (var context = new DatabaseContext())
            {
                context.Customers.Add(newCustomer);
                context.SaveChanges();
            }
        }
    }
}
