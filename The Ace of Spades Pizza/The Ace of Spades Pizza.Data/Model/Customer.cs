using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Ace_of_Spades_Pizza.Data.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter a 10 digit phone number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter digits only")]
        public string PhoneNumber { get; set; }
                
        public virtual ICollection<Order> Orders { get; set; }
    }
}
