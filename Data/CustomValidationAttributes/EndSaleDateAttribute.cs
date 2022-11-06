using System;
using System.ComponentModel.DataAnnotations;

namespace Data.CustomValidationAttributes
{
    public class EndSaleDateAttribute : RangeAttribute
    {
        public EndSaleDateAttribute() : base(typeof(DateTime),
            DateTime.Now.AddDays(1).ToString(), DateTime.Now.AddDays(180).ToString())
        {
            ErrorMessage = $"Date should be between {DateTime.Now.AddDays(1)} and {DateTime.Now.AddDays(180)}";
        }
    }
}
