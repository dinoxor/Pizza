using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Ace_of_Spades_Pizza.Data.Model;
using The_Ace_of_Spades_Pizza.Attributes;

namespace The_Ace_of_Spades_Pizza.Site.Models
{
    public class OrderCreateModel
    {
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Please select a pizza")]
        public string PizzaId { get; set; }

        [Required(ErrorMessage = "Please enter the number of desired pizza")]
        [GreaterThanOrEqualTo(1, ErrorMessage = "Aren't you hungry?!?!")]
        [LessThanOrEqualTo(100, ErrorMessage = "We can't make that much pizza :(")]
        public int Quantity { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [MaxNumberOfDays(1, ErrorMessage = "We can only deliver up to 24 hours")]
        [MinDate(ErrorMessage = "Please select a future date")]
        public DateTime DeliveryArrivalDateTime { get; set; }

        public bool isSuccessful { get; set; } = false;
        public string ConfirmationMessage { get; set; }

        public IEnumerable<SelectListItem> Pizzas { get; set; }
    }
}