using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace PALUNA.CustomAttribute
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        public EmailValidationAttribute(string message) : base((message)) { }
        private readonly string _emailRegex =
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public override bool IsValid(object value)
        {
            try
            {
                if (value is string emailString)
                {
                    var mail = new MailAddress(emailString);
                    return true;
                };
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }


        }
    }
}