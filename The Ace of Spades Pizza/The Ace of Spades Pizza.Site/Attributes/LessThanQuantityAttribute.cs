using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Ace_of_Spades_Pizza.Attributes
{
    public class LessThanOrEqualToAttribute : ValidationAttribute
    {
        private readonly int _maxValue;

        public LessThanOrEqualToAttribute(int maxValue)
        {
            _maxValue = maxValue;
        }

        public override bool IsValid(object value)
        {
            var quantity = (int)value;
            if (quantity <= _maxValue)
            {
                return true;
            }

            return false;
        }
    }
}