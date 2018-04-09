using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Ace_of_Spades_Pizza.Data.Model;

namespace The_Ace_of_Spades_Pizza.Data.Repository
{
    public class PizzaRepository
    {
        public IEnumerable<Pizza> GetAll()
        {
            using (var context = new DatabaseContext())
            {
                return context.Pizzas.ToList();
            }
        }
    }
}
