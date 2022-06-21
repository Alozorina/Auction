using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.CustomValidationAttributes
{
    public class StartSaleDateAttribute : RangeAttribute
    {
        public StartSaleDateAttribute() : base(typeof(DateTime),
            DateTime.Now.ToString(), DateTime.Now.AddDays(180).ToString())
        {
            ErrorMessage = $"Date should be between {DateTime.Now} and {DateTime.Now.AddDays(180)}";
        }
    }
}
