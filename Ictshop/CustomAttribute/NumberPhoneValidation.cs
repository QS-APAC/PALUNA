using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PALUNA.CustomAttribute
{
    public class NumberPhoneValidation : ValidationAttribute
    {
        public NumberPhoneValidation() : base()
        {

        }

        public NumberPhoneValidation(string errorMessage) : base(errorMessage)
        {

        }

        private readonly string _numberphoneRegex = @"^(03|05|07|08|09)([0-9]{8})$";
        public override bool IsValid(object value)
        {
            try
            {

                return Regex.IsMatch(value?.ToString() ?? string.Empty, _numberphoneRegex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}