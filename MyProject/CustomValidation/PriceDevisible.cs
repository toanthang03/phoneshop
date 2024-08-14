using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyProject.CustomValidation
{
    public class PriceDevisible : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (int)value % 1000 == 0;
        }
    }
}