using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Ace_of_Spades_Pizza.Attributes;

namespace The_Ace_of_Spades_Pizza.Models
{
    public class OrderCreateModel
    {
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please select a pizza")]
        public string PizzaId { get; set; }

        [Required(ErrorMessage = "Please enter the number of desired pizza")]
        [GreaterThanOrEqualTo(1,ErrorMessage ="Aren't you hungry?!?!")]
        public int Quantity { get; set; }

        [Required]
        [CurrentDate(ErrorMessage ="Please select a time in the future")]
        public DateTime DeliveryArrivalDateTime { get; set; }

        public bool isSuccessful { get; set; } = false;
        public string ConfirmationMessage { get; set; }

        public IEnumerable<SelectListItem> Pizzas {get; set; }
    }
}