using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ace_of_Spades_Pizza.Data.Model
{
    public class Order
    {
        public int OrderId { get; set; }       
        
        public int CustomerId { get; set; }        
        public int PizzaId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;

        public DateTime OrderCreateddDateTime { get; set; } = DateTime.Now;
               
        public DateTime DeliveryArrivalDateTime { get; set; } = DateTime.Now;
                
        public virtual Customer Customer { get; set; }              
        public virtual Pizza Pizza { get; set; }
    }
}
