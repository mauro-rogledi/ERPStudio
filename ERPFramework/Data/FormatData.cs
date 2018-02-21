using System;

namespace ERPFramework.Data
{
    public static class FormatData
    {
        public static string GetFormatData<T>(T value)
        {
            if (typeof(T).Equals(typeof(string)))
                return value.ToString();
            else if (typeof(T).Equals(typeof(DateTime)))
                return string.Format("{0:d}", value);
            else if (typeof(T).Equals(typeof(float)) || typeof(T).Equals(typeof(double)))
                return string.Format("{0:C}", value);

            return value.ToString();
        }
    }
}