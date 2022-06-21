using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.CustomValidationAttributes
{
    public class BirthDateAttribute : RangeAttribute
    {
        public BirthDateAttribute() : base(typeof(DateTime),
            DateTime.Now.AddYears(-110).ToString(), DateTime.Now.AddYears(-18).ToString())
        {
            ErrorMessage = $"Date should be between {DateTime.Now.AddYears(-110)} and {DateTime.Now.AddYears(-18)}";
        }
    }
}
