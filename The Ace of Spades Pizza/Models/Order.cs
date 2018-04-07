using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace The_Ace_of_Spades_Pizza.Models
{
    public class Order
    {
        [Key]
        [Column(Order=0)]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Pizza")]
        public int PizzaId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;

        public DateTime OrderCreateddDateTime { get; set; } = DateTime.Now;

        //[CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime DeliveryArrivalDateTime { get; set; } = DateTime.Now;

        public virtual Customer Customer { get; set; }
        public virtual Pizza Pizza { get; set; }

    }
}