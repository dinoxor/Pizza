using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace The_Ace_of_Spades_Pizza.Attributes
{
    public class MinDateAttribute: ValidationAttribute
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