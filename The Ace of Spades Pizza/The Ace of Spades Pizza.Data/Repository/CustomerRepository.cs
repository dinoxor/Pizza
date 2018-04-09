using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ace_of_Spades_Pizza.Data.Model;

namespace The_Ace_of_Spades_Pizza.Data.Repository
{
    public class CustomerRepository: IRepository<Customer>
    {
        private DatabaseContext context = new DatabaseContext();

        public IEnumerable<Customer> GetAll()
        {
            using (context)
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

        public void Update(Customer model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
