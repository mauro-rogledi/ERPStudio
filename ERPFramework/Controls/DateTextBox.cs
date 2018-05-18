using System;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms.Design;
using System.Collections;
using System.Reflection;
using System.Drawing;

namespace ERPFramework.Controls
{
    [Designer(typeof(DateTextBox.Designer))]
    public class DateTextBox : MetroTextBox
    {
        private enum InputType { Day, Month, Year, Over }

        #region Private field

        private DateTime oldValue = new DateTime().EmptyDate();
        private char _groupSeparator = DateTimeFormatInfo.CurrentInfo.DateSeparator[0];
        bool _MonthBeforeDay = DateTimeFormatInfo.CurrentInfo.ShortDatePattern.StartsWith("M");
        string _dateFormat { get { return _MonthBeforeDay ? "MM/dd/yyyy" : "dd/MM/yyyy"; } }              

        private DateTime _minRange = new DateTime().EmptyDate();
        private DateTime _maxRange = new DateTime(2100, 12, 31);
        #endregion

        #region Public Properties
        /// <summary>
        ///   Gets or sets the character to use for the group separator. </summary>
        [Browsable(false)]
        public char GroupSeparator
        {
            get { return _groupSeparator; }
            set { _groupSeparator = value; }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(typeof(DateTime), "1753/01/01")]
        public DateTime OldValue { get { return oldValue; } }

        /// <summary>
        ///   Gets or sets the minimum value allowed. </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.RangeMin">NumericBehavior.RangeMin</see>. </remarks>
        /// <seealso cref="NumericBehavior.RangeMin" />
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("The minimum value allowed.")]
        [DefaultValue(typeof(DateTime), "0001/01/01")]
        public DateTime RangeMin
        {
            get { return _minRange; }
            set { _minRange = value; }
        }

        /// <summary>
        ///   Gets or sets the maximum value allowed. </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.RangeMax">NumericBehavior.RangeMax</see>. </remarks>
        /// <seealso cref="NumericBehavior.RangeMax" />
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("The maximum value allowed.")]
        [DefaultValue(typeof(DateTime), "2100/12/31")]
        public DateTime RangeMax
        {
            get { return _maxRange; }
            set { _maxRange = value; }
        }

        /// <summary>
        ///   Gets or sets the textbox's Text as a long. </summary>
        /// <remarks>
        ///   If the text empty or cannot be converted to an long, a 0 is returned. </remarks>
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Browsable(false)]
        [DefaultValue(typeof(DateTime), "1753/01/01")]
        public DateTime Today
        {
            get
            {
                try
                {
                    return Text.IsEmpty()
                        ? new DateTime().EmptyDate()
                        : Convert.ToDateTime(Text);
                }
                catch
                {
                    return new DateTime().EmptyDate();
                }
            }
            set
            {
                Text = value.IsEmpty()
                    ? string.Empty
                    : value.ToString(_dateFormat);
            }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        [Description("Occurs when the value has changed from gotfocus event to validate event")]
        public event EventHandler ChangeValue;
        #endregion

        #region Constructor
        public DateTextBox() : base()
        {
            KeyPress += DateTextBox_KeyPress;
            KeyDown += DateTextBox_KeyDown;
            TextChanged += DateTextBox_TextChanged;
            Today = DateTime.Today;
            CustomButton.Image = Properties.Resources.Calendar16;
            CustomButton.Location = new System.Drawing.Point(139, 1);
            CustomButton.TabStop = false;
            ButtonClick += DateTextBox_ButtonClick;
            GotFocus += DateTextBox_GotFocus;
            Validated += DateTextBox_Validated;

            ShowButton = true;
        }

        private void DateTextBox_Validated(object sender, EventArgs e)
        {
            if (Today != oldValue && ChangeValue != null)
                ChangeValue(sender, e);
        }

        private void DateTextBox_GotFocus(object sender, EventArgs e)
        {
            oldValue = Today;
        }

        private void DateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!Focused)
                oldValue = Today;
        }

        #endregion

        #region Calendar Popup

        private void DateTextBox_ButtonClick(object sender, EventArgs e)
        {
            PopUpCalendar popup = new PopUpCalendar(Today);
            popup.AfterDateSelectEvent += new PopUpCalendar.AfterDateSelectedSelectEventHandler(popup_AfterDateSelectEvent);
            popup.Closed += new EventHandler(popup_Closed);
            popup.Location = ((Control)sender).PointToScreen(Point.Empty);
            popup.Show();
        }

        private void popup_AfterDateSelectEvent(object sender, DateTime SelectedDate)
        {
            Focus();
            Today = SelectedDate;
        }

        private void popup_Closed(object sender, EventArgs e)
        {
            Focus();
        }

        private Point location()
        {
            int X = PointToScreen(Point.Empty).X;
            int Y = PointToScreen(Location).Y;

            return new Point(X, Y);
        }
        #endregion

        #region Events

        private void DateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && SelectionStart + SelectionLength + 1 < Text.Length)
                e.Handled = true;
        }

        private void DateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SelectionLength != 0 && SelectionStart + SelectionLength == Text.Length)
            {
                int selectionStart = SelectionStart;
                Text = Text.Remove(SelectionStart, SelectionLength);
                SelectionStart = selectionStart;
            }

            if (Char.IsDigit(e.KeyChar))
                e.Handled = IsDigitPress(e.KeyChar);
            else if (e.KeyChar == _groupSeparator)
                e.Handled = IsGroupPress();
            else if (Char.IsControl(e.KeyChar))
                e.Handled = IsControlPressed(e.KeyChar);
            else
                e.Handled = true;
        }

        #endregion

        #region Private Methods

        private bool IsControlPressed(char keyChar)
        {
            if (keyChar == (char)Keys.Back)
            {
                if (SelectionStart != Text.Length && SelectionStart > 0)
                {
                    SelectionStart--;
                    return true;
                }
                if (SelectionStart > 0)
                {
                    Replace(SelectionStart - 1, 1, "");
                    return true;
                }
            }
            return true;
        }

        private bool IsGroupPress()
        {
            int l = Text.Length;
            switch (SelectionStart)
            {
                case 0:
                    if (l > 2)
                        SelectionStart = 3;
                    return true;
                case 1:
                    if (l == 1)
                        Replace(0, 1, $"{int.Parse(Text[0].ToString()),2:00}{_groupSeparator}");
                    else if (l == 2)
                        Add(_groupSeparator.ToString());
                    else if (l > 2)
                        SelectionStart = 3;
                    return true;
                case 2:
                    if (l == 2)
                        Add(_groupSeparator.ToString());
                    else if (l > 2)
                        SelectionStart = 3;
                    return true;
                case 3:
                    if (l > 5)
                        SelectionStart = 6;
                    return true;
                case 4:
                    if (l == 4)
                        Replace(3, 1, $"{int.Parse(Text[3].ToString()),2:00}{_groupSeparator}");
                    else if (l == 5)
                        Add(_groupSeparator.ToString());
                    else if (l > 5)
                        SelectionStart = 6;
                    return true;
                case 5:
                    if (l == 5)
                        Add(_groupSeparator.ToString());
                    else if (l > 5)
                        SelectionStart = 6;
                    return true;
                default:
                    return true;
            }
        }

        private bool IsDigitPress(char keyChar)
        {
            InputType iType = GetInputType();
            switch (iType)
            {
                case InputType.Day:
                    return InputDay(keyChar);
                case InputType.Month:
                    return InputMonth(keyChar);
                case InputType.Year:
                    return InputYear(keyChar);
            }

            return true;
        }

        private bool InputDay(char keyChar)
        {
            string sDay = GetMaxDay().ToString();

            int p = SelectionStart - GetDayRange().Start;
            // First digit of day
            if (p == 0)
            {
                if (SelectionStart == 2 && _MonthBeforeDay && Text.Length == 2)
                {
                    Add(_groupSeparator.ToString());
                }

                if (keyChar > sDay[0])
                {
                    Add($"{keyChar.Int(),2:00}");
                }
                else if (keyChar < sDay[0])
                {
                    Add(keyChar.ToString());
                }
                else
                {
                    if (p + GetDayRange().Len <= Text.Length && Text[p + 1] > sDay[1])
                    {
                        Add($"{keyChar.Int(),2:00}");
                        return true;
                    }
                    Add(keyChar.ToString());
                }
            }
            // Second digit of day
            else
            {
                string val = GetDay();
                string max = GetMaxDay();
                if (val[0] < max[0] || keyChar <= max[1])
                {
                    Add(keyChar.ToString());
                }
            }
            return true;
        }

        private bool InputMonth(char keyChar)
        {
            int p = SelectionStart - GetMonthRange().Start;
            int l = Text.Length;

            // First digit of Month
            if (p <= 0)
            {
                if (SelectionStart == 2 && !_MonthBeforeDay)
                {
                    if (l == 2)
                    {
                        Add(_groupSeparator.ToString());
                    }
                    else SelectionStart++;
                }

                if (keyChar > '1')
                {
                    Add($"{keyChar.Int(),2:00}");
                }
                else
                {
                    Add(keyChar.ToString());
                    if (int.Parse(GetMonth()) > 12)
                        Replace(SelectionStart - 1, 2, $"{keyChar.Int(),2:00}");

                }
            }
            else
            {
                if (Text[SelectionStart - 1] == '1' && keyChar > '2')
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    return true;
                }
                else
                    Add(keyChar.ToString());
            }
            return true;
        }

        private bool InputYear(char keyChar)
        {
            int p = SelectionStart - GetYearRange().Start;
            int l = Text.Length;


            // First digit of Month
            if (p <= 0)
            {
                if (SelectionStart == 5)
                {
                    if (l == 5)
                    {
                        Add(_groupSeparator.ToString());
                    }
                    else SelectionStart++;
                }
                p = SelectionStart - GetYearRange().Start;
            }

            int minYear = int.Parse(GetMinYear().Substring(0, p + 1));
            int maxYear = int.Parse(GetMaxYear().Substring(0, p + 1));

            int curYear = int.Parse(Text.Midstring(GetYearRange().Start, p) + keyChar);

            if (curYear <= maxYear && curYear >= minYear)
                Add(keyChar.ToString());

            return true;
        }

        private string GetMaxDay()
        {
            int year = GetYear() == "" ? DateTime.Today.Year : int.Parse(GetYear());

            if (GetMonth() != "")
                return DateTime.DaysInMonth(year, int.Parse(GetMonth())).ToString();
            else
                return "31";
        }

        private string GetDay()
        {
            string txt = Text.Midstring(GetDayRange().Start, 2);
            if (string.IsNullOrEmpty(txt))
                return GetMaxDay();
            return txt;
        }

        private string GetMonth()
        {
            string txt = Text.Midstring(GetMonthRange().Start, 2);

            if (string.IsNullOrEmpty(txt))
                return "12";
            return txt;
        }

        private string GetYear()
        {
            if (Text.Length >= GetYearRange().Item1)
            {
                return Text.Substring(GetYearRange().Start);
            }
            return "";
        }

        private string GetMaxYear()
        {
            return RangeMax.Year.ToString();
        }

        private string GetMinYear()
        {
            return RangeMin.Year.ToString();
        }

        private void Add(string add)
        {
            var sb = new StringBuilder(Text);
            int l = add.Length;

            // se sono in fondo allora aggiungo
            if (Text.Length == SelectionStart)
                sb.Append(add);
            else
            {
                // altrimenti sostituisco
                sb.Remove(SelectionStart, l);
                sb.Insert(SelectionStart, add);
            }

            int p = SelectionStart;
            Text = sb.ToString();
            SelectionStart = p + l;
        }

        private void Replace(int pos, int numchar, string replace)
        {
            var sb = new StringBuilder(Text);
            sb.Remove(pos, numchar);
            sb.Insert(pos, replace);
            Text = sb.ToString();
            SelectionStart = pos + replace.Length;
        }

        // 0123456789
        // 31/12/95
        // 31/12/1995
        private Range GetDayRange()
        {
            return _MonthBeforeDay
                        ? new Range(3, 2)
                        : new Range(0, 2);

        }

        private Range GetMonthRange()
        {
            return _MonthBeforeDay
                        ? new Range(0, 2)
                        : new Range(3, 2);
        }

        private Range GetYearRange()
        {
            return new Range(6, 4);
        }

        private InputType GetInputType()
        {
            if (_MonthBeforeDay)
            {
                if (SelectionStart < 2)
                    return InputType.Month;
                else if (SelectionStart < 5)
                    return InputType.Day;
            }
            else
            {
                if (SelectionStart < 2)
                    return InputType.Day;
                else if (SelectionStart < 5)
                    return InputType.Month;
            }
            if (SelectionStart == 10)
                return InputType.Over;
            return InputType.Year;
        }
        #endregion

        #region Properties Designer
        internal class Designer : ControlDesigner
        {
            protected override void PostFilterProperties(IDictionary attributes)
            {
                attributes.Remove("TextAlign");
                attributes.Remove("ToUpper");
                attributes.Remove("Lines");
                attributes.Remove("Value");
                base.PostFilterProperties(attributes);
            }
        }


        #endregion

        private class Range : Tuple<int, int>
        {
            public Range(int start, int len)
             : base(start, len)
            { }


            public int Start { get { return Item1; } }
            public int Len { get { return Item2; } }
        }

        internal class DateFormatConverter : EnumConverter
        {
            public DateFormatConverter(Type type) : base(type) { }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(Enum[]);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {

                foreach (var val in Enum.GetValues(this.EnumType))
                {
                    FieldInfo fi = EnumType.GetField(val.ToString());

                    DescriptionAttribute[] attributes =
                        (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute),
                        false);
                    string desc;

                    if (attributes.Length > 0)
                        desc = attributes[0].Description;
                    else
                        desc = value.ToString();

                    if (desc == value.ToString())
                        return val;
                }
                return "";
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {

                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

                if (attributes != null &&
                    attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
        }
    }

    internal static class StringExtender
    {
        public static string Midstring(this string text, int start, int len)
        {
            if (start > text.Length)
                return "";

            return (text.Length >= start + len)
                ? text.Substring(start, len)
                : text.Substring(start);
        }
    }

    internal static class CharExtender
    {
        public static int Int(this char text)
        {
            return int.Parse(text.ToString());
        }
    }
}
