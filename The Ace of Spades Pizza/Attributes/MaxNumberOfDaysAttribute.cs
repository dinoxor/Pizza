using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace The_Ace_of_Spades_Pizza.Attributes
{
    public class MaxNumberOfDaysAttribute : ValidationAttribute
    {
        private readonly DateTime _maxDate;

        public MaxNumberOfDaysAttribute(int maxDays)
        {
            _maxDate = DateTime.Now.AddDays(maxDays);
        }

        public override bool IsValid(object value)
        {
            var datetime = (DateTime) value;
            if (datetime <= _maxDate)
            {
                return true;
            }

            return false;
        }
    }
}