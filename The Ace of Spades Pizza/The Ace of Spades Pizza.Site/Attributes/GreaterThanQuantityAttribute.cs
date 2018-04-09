using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Ace_of_Spades_Pizza.Attributes
{
    public class GreaterThanOrEqualToAttribute : ValidationAttribute
    {
        public int _minValue { get; set; }

        public GreaterThanOrEqualToAttribute(int minValue)
        {
            _minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            var quantity = (int)value;
            if (quantity >= _minValue)
            {
                return true;
            }

            return false;
        }
    }
}