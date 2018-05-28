using ERPFramework.Data;
using System;
using System.Windows.Forms;

namespace ERPFramework
{
    public static class ExtendEnum
    {
        public static int Int(this Enum enums)
        {
            return (int)Convert.ChangeType(enums, typeof(int));
        }
        public static long Long(this Enum enums)
        {
            return (long)Convert.ChangeType(enums, typeof(long));
        }

        public static string Translate(this Enum enums, System.Resources.ResourceManager resource)
        {
            string name = System.Enum.GetName(enums.GetType(), enums);
            return resource.GetString(name);
        }
    }

    public static class ExtendDate
    {
        public static DateTime StartYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime EndYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        public static DateTime EmptyDate(this DateTime date)
        {
            return new DateTime(0001, 01, 01);
        }

        public static bool Between(this DateTime date, DateTime dFrom, DateTime dTo)
        {
            return date >= dFrom && date <= dTo;
        }

        public static bool IsEmpty(this DateTime date)
        {
            return date == EmptyDate(date);
        }
    }

    public static class ExtendedString
    {
        public static string CommaConcat(this string strings, string value)
        {
            return strings.Length == 0
                ? value
                : string.Concat(strings, ",", value);
        }

        public static string SeparConcat(this string strings, string value, string separ)
        {
            return strings.Length == 0
                ? value
                : string.Concat(strings, separ, value);
        }

        public static bool IsEmpty(this string strings)
        {
            return string.IsNullOrEmpty(strings);
        }

        public static int Int(this string strings)
        {
            return int.Parse(strings);
        }

        public static string TrimAll(this string strings)
        {
            return strings.TrimStart().TrimEnd();
        }

        public static string TrimAll(this string strings, char[] trimChars)
        {
            return strings.TrimStart(trimChars).TrimEnd(trimChars);
        }

        public static string Subsr(this string text, int start, int len)
        {
            return (start + len > text.Length)
                    ? text.Substring(start)
                    : text.Substring(start, len);
        }

        public static string Right(this string s, int length)
        {
            length = Math.Max(length, 0);

            if (s.Length > length)
            {
                return s.Substring(s.Length - length, length);
            }
            else
            {
                return s;
            }
        }
    }

    public static class ExtendedBool
    {
        public static int Int(this bool boolean)
        {
            return boolean ? 1 : 0;
        }
    }

    public static class ExtenderControl
    {
        public static Forms.IDocument FindDocument(this Control control)
        {
            Control ctrl = control;
            while (ctrl != null && !(ctrl is Forms.IDocument))
                ctrl = ctrl.Parent;

            return ctrl as Forms.IDocument;
        }
    }

    public static class ExtenderDataRow
    {
        public static void SetValue<T>(this System.Data.DataRow row, IColumn col, T value)
        {
            SetValue<T>(row, col.Name, value);
        }

        public static void SetValue<T>(this System.Data.DataRow row, string col, T value)
        {
            row[col] = value;
        }

        public static T GetValue<T>(this System.Data.DataRow row, IColumn col, System.Data.DataRowVersion version = System.Data.DataRowVersion.Current)
        {
            return GetValue<T>(row, col.Name, version);
        }

        public static T GetValue<T>(this System.Data.DataRow row, string col, System.Data.DataRowVersion version = System.Data.DataRowVersion.Current)
        {
            if (typeof(T).BaseType == typeof(Enum))
            {
                if (row[col,version] != System.DBNull.Value)
                    return (T)Enum.ToObject(typeof(T), row[col, version]);
                else
                    return (T)Enum.ToObject(typeof(T), default(T));
            }
            else
            {
                if (row[col, version] != System.DBNull.Value && row[col, version] != null)
                    return (T)Convert.ChangeType(row[col, version], typeof(T));
                else
                    return default(T);
            }
        }

    }

    public static class ExtenderDataGridViewRow
    {
        public static void SetValue<T>(DataGridViewRow row, IColumn col, T value) => row.Cells[col.Name].Value = value;

        public static T GetValue<T>(DataGridViewRow row, IColumn col) => GetValue<T>(row, col.Name);

        public static T GetValue<T>(DataGridViewRow row, string col) =>
            (row.Cells[col].Value != System.DBNull.Value && row.Cells[col].Value != null && row.Cells[col].Value.ToString() != string.Empty)
                ? (T)Convert.ChangeType(row.Cells[col].Value, typeof(T))
                : default(T);
    }
}
