using DAL.Entities;
using System;

namespace BLL.Validation
{
    public static class ItemValidation
    {
        public static void ThrowArgumentExceptionIfItemIsNull(Item item)
        {
            if (item == null)
                throw new ArgumentException("Wrong Id");
        }
    }
}
