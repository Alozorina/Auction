using DAL.Entities;
using System;

namespace BLL.Validation
{
    public static class ItemValidation
    {
        public static void ThrowArgumentExceptionIfItemWasntFound(Item item, string message = "Wrong Id")
        {
            if (item is null)
                throw new ArgumentException(message);
        }
    }
}
