using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Ace_of_Spades_Pizza.Attributes
{
    public class CurrentDateAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var datetime = (DateTime) value;
            if (datetime >= DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
}