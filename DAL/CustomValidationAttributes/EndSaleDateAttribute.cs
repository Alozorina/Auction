using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.CustomValidationAttributes
{
    public class EndSaleDateAttribute : RangeAttribute
    {
        public EndSaleDateAttribute() : base(typeof(DateTime),
            DateTime.Now.AddHours(1).ToString(), DateTime.Now.AddDays(180).ToString())
        {
            ErrorMessage = $"Date should be between {DateTime.Now.AddHours(1)} and {DateTime.Now.AddDays(180)}";
        }
    }
}
