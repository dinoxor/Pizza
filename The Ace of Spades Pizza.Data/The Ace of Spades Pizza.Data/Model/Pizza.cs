using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ace_of_Spades_Pizza.Data.Model
{
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [InverseProperty("Pizza")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
