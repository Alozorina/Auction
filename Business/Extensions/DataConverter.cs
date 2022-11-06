using System;

namespace Business.Extensions
{
    public static class DataConverter
    {
        public static DateTime? ConvertDateFromClient(string data)
        {
            if (data == null)
                return null;

            var stringDate = data.Split('-', '.', '/');
            int[] intDate = Array.ConvertAll(stringDate, int.Parse);
            return new DateTime(intDate[0], intDate[1], intDate[2]);
        }
    }
}
