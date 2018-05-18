using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms.Design;
using System.Collections;

namespace MetroFramework.Extender
{
    [Designer(typeof(MetroTextBoxNumeric.Designer))]
    public class MetroTextBoxNumeric : MetroTextBoxButtonState
    {
        #region Private field
        private char _decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
        private char _groupSeparator = NumberFormatInfo.CurrentInfo.NumberGroupSeparator[0];
        private readonly char _negativeSign = NumberFormatInfo.CurrentInfo.NegativeSign[0];
        private string _currencySimbol = NumberFormatInfo.CurrentInfo.CurrencySymbol;
        private int _prefixLen;
        private bool _showPrefix = false;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the maximum number of digits allowed left of the decimal point.
        /// </summary>
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [Description("The maximum number of digits allowed left of the decimal point.")]
        public int MaxWholeDigits { get; set; }

        /// <summary>
        ///   Gets or sets the maximum number of digits allowed right of the decimal point. </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.MaxDecimalPlaces">NumericBehavior.MaxDecimalPlaces</see>. </remarks>
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [Description("The maximum number of digits allowed right of the decimal point.")]
        public int MaxDecimalPlaces { get; set; }

        /// <summary>
        ///   Gets or sets whether the value is allowed to be negative or not. </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.AllowNegative">NumericBehavior.AllowNegative</see>. </remarks>
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [Description("Determines whether the value is allowed to be negative or not.")]
        public bool AllowNegative { get; set; }

        /// <summary>
        ///   Gets or sets if show the prefix simbol </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.AllowNegative">NumericBehavior.AllowNegative</see>. </remarks>
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [Description("Determines whether the value is allowed to be negative or not.")]
        [DefaultValue(false)]
        public bool ShowPrefix
        {
            get { return _showPrefix; }
            set { _showPrefix = value; _prefixLen = _showPrefix ? _currencySimbol.Length : 0; }
        }

        /// <summary>
        ///   Gets or sets the character to use for the decimal point. </summary>
        [Browsable(false)]
        public char DecimalPoint
        {
            get { return _decimalSeparator; }
            set { _decimalSeparator = value; }
        }

        /// <summary>
        ///   Gets or sets the character to use for the group separator. </summary>
        [Browsable(false)]
        public char GroupSeparator
        {
            get { return _groupSeparator; }
            set { _groupSeparator = value; }
        }

        /// <summary>
        ///   Allow use of the group separator. </summary>
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [Description("Allow insert of group separator in number")]
        public bool AllowGroupSeparator { get; set; }

        /// <summary>
        ///   Gets or sets the text to automatically insert in front of the number, such as a currency symbol. </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.Prefix">NumericBehavior.Prefix</see>. </remarks>
        /// <seealso cref="NumericBehavior.Prefix" />
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [Description("The text to automatically insert in front of the number, such as a currency symbol.")]
        public String Prefix
        {
            get { return _currencySimbol; }
            set { _currencySimbol = value; _prefixLen = _showPrefix ? value.Length : 0; }
        }

        /// <summary>
        ///   Gets or sets the minimum value allowed. </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.RangeMin">NumericBehavior.RangeMin</see>. </remarks>
        /// <seealso cref="NumericBehavior.RangeMin" />
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [Description("The minimum value allowed.")]
        [DefaultValue(Double.MinValue)]
        public double RangeMin { get; set; }

        /// <summary>
        ///   Gets or sets the maximum value allowed. </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.RangeMax">NumericBehavior.RangeMax</see>. </remarks>
        /// <seealso cref="NumericBehavior.RangeMax" />
        [Category(ExtenderDefaults.PropertyCategory.Behaviour)]
        [Description("The maximum value allowed.")]
        [DefaultValue(Double.MaxValue)]
        public double RangeMax { get; set; }

        /// <summary>
        ///   Gets or sets the textbox's Text as a double. </summary>
        /// <remarks>
        ///   If the text is empty or cannot be converted to a double, a 0 is returned. </remarks>
        /// <seealso cref="Long" />
        /// <seealso cref="Int" />
        [Browsable(false)]
        public Decimal Decimal
        {
            get
            {
                try
                {
                    return Convert.ToDecimal(NumericText);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                Text = value.ToString();
            }
        }

        /// <summary>
        ///   Gets or sets the textbox's Text as a double. </summary>
        /// <remarks>
        ///   If the text is empty or cannot be converted to a double, a 0 is returned. </remarks>
        /// <seealso cref="Long" />
        /// <seealso cref="Int" />
        [Browsable(false)]
        public double Double
        {
            get
            {
                try
                {
                    return Convert.ToDouble(NumericText);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                Text = value.ToString();
            }
        }

        /// <summary>
        ///   Gets or sets the textbox's Text as an int. </summary>
        /// <remarks>
        ///   If the text empty or cannot be converted to an int, a 0 is returned. </remarks>
        /// <seealso cref="Long" />
        /// <seealso cref="Double" />
        [Browsable(false)]
        public int Int
        {
            get
            {
                try
                {
                    return Convert.ToInt32(NumericText);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                Text = value.ToString();
            }
        }

        /// <summary>
        ///   Gets or sets the textbox's Text as a long. </summary>
        /// <remarks>
        ///   If the text empty or cannot be converted to an long, a 0 is returned. </remarks>
        /// <seealso cref="Int" />
        /// <seealso cref="Double" />
        [Browsable(false)]
        public long Long
        {
            get
            {
                try
                {
                    return Convert.ToInt64(NumericText);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                Text = value.ToString();
            }
        }

        /// <summary>
        ///   Retrieves the textbox's value without any non-numeric characters. </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.NumericText">NumericBehavior.NumericText</see>. </remarks>
        [Browsable(false)]
        public string NumericText
        {
            get
            {
                return GetNumericText();
            }
        }


        #endregion

        #region Constructor
        public MetroTextBoxNumeric()
        {
            TextAlign = HorizontalAlignment.Right;
            KeyPress += NumericTextBox_KeyPress;
            KeyDown += NumericTextBox_KeyDown;
            TextChanged += NumericTextBox_TextChanged;
        } 
        #endregion

        #region Events
        private void NumericTextBox_TextChanged(object sender, EventArgs e)
        {
            FormatText();
        }

        private void NumericTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && SelectionStart != Text.Length && SelectionLength == 0)
            {
                if (Text[SelectionStart] == _decimalSeparator)
                {
                    var selectionStart = SelectionStart;
                    Text = Text.Substring(0, SelectionStart);
                    SelectionStart = selectionStart;
                }
            }
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SelectionLength != 0)
            {
                var selectionStart = SelectionStart;
                Text = Text.Remove(SelectionStart, SelectionLength);
                SelectionStart = selectionStart;
            }

            if (Char.IsDigit(e.KeyChar))
                e.Handled = IsDigitPress(e.KeyChar);
            else if (e.KeyChar == _negativeSign)
                e.Handled = IsNegativePress();
            else if (e.KeyChar == _decimalSeparator || e.KeyChar == _groupSeparator)
            {
                //MessageBox.Show(((int)e.KeyChar).ToString());
                if (e.KeyChar == _groupSeparator)
                {
                    //MessageBox.Show(((int)_decimalSeparator).ToString());
                    e.KeyChar = _decimalSeparator;
                }
                e.Handled = IsDecimalPress();
            }
            else if (Char.IsControl(e.KeyChar))
                e.Handled = IsControlPressed(e.KeyChar);
            else
                e.Handled = true;
        }

        #endregion

        #region Private Methods
        private void FormatText()
        {
            var numericText = GetNumericText();
            var posDecimalSeparator = numericText.IndexOf(_decimalSeparator);
            var integerText = posDecimalSeparator >= 0 ? numericText.Substring(0, posDecimalSeparator) : numericText;

            if ((integerText.Length - (posDecimalSeparator >= 0 ? 1 : 0)) > MaxWholeDigits)
                numericText = numericText.Substring(0, MaxWholeDigits) + (posDecimalSeparator >= 0 ? _decimalSeparator.ToString() : "");

            var separatedText = numericText;

            var cursorPos = SelectionStart;
            var groupCount = Text.Count(f => f == _groupSeparator);

            // Retrieve the number without the decimal point
            var decimalPos = numericText.IndexOf(_decimalSeparator);
            if (decimalPos >= 0)
                separatedText = separatedText.Substring(0, decimalPos);

            if (AllowGroupSeparator)
            {
                var length = separatedText.Length;
                var isNegative = (separatedText != "" && separatedText[0] == _negativeSign);

                // Loop in reverse and stick the separator every m_digitsInGroup digits.
                for (int iPos = length - 4; iPos >= (isNegative ? 1 : 0); iPos -= 3)
                    separatedText = separatedText.Substring(0, iPos + 1) + _groupSeparator + separatedText.Substring(iPos + 1);
            }

            // Prepend the prefix, if the number is not empty.
            if (separatedText != "" || decimalPos >= 0)
            {
                if (_showPrefix&& _prefixLen > 0)
                {
                    separatedText = _currencySimbol + separatedText;
                    if (!Text.StartsWith(_currencySimbol))
                        cursorPos += _prefixLen;
                }

                if (decimalPos >= 0)
                    separatedText += numericText.Substring(decimalPos);
            }

            Text = separatedText;
            var newCount = Text.Count(f => f == _groupSeparator);
            var newpos = cursorPos + newCount - groupCount;
            SelectionStart = newpos >= 0 ? newpos : 0;
            System.Diagnostics.Debug.WriteLine(SelectionStart);
        }

        private string GetNumericText()
        {
            var firstdigit = _showPrefix && _prefixLen > 0 && Text.StartsWith(_currencySimbol) ? _prefixLen : 0;
            var lastdigit = Text.Length;

            var sb = new StringBuilder();

            for (int t = firstdigit; t < lastdigit; t++)
                if (char.IsDigit(Text[t]) || Text[t] == _negativeSign || Text[t] == _decimalSeparator)
                    sb.Append(Text[t]);

            return sb.ToString();
        }

        private bool IsControlPressed(char keyChar)
        {
            if (keyChar == (char)Keys.Back)
            {
                if (SelectionStart > 0)
                    if ((Text[SelectionStart - 1] == _groupSeparator) ||
                        Text.Contains(_currencySimbol) && _showPrefix && _prefixLen > 0 && SelectionStart <= _prefixLen)
                    {
                        SelectionStart--;
                        return true;
                    }
            }
            return false;
        }

        private bool IsDecimalPress()
        {
            if (MaxDecimalPlaces == 0)
                return true;

            var p = Text.IndexOf(_decimalSeparator);
            if (p >= 0)
            {
                SelectionStart = p + 1;
                return true;
            }
            else
            {
                var digits = Text.Length - SelectionStart;
                if (digits > MaxDecimalPlaces)
                {
                    var selectionStart = SelectionStart;
                    var groupCount = Text.Substring(0, SelectionStart + MaxDecimalPlaces).Count(f => f == _groupSeparator);
                    var text = Text.Substring(0, SelectionStart + MaxDecimalPlaces + groupCount).Insert(SelectionStart, _decimalSeparator.ToString());
                    Text = text;
                    SelectionStart = selectionStart + 1;
                    return true;
                }
                if (SelectionStart < _prefixLen + 1)
                    SelectionStart = _prefixLen + 1;
            }

            return false;
        }

        private bool IsNegativePress()
        {
            if (!AllowNegative)
                return true;

            var p = Text.IndexOf(_negativeSign);
            var selectionStart = SelectionStart;
            if (p >= 0)
            {
                Text = Text.Remove(p, 1);
                SelectionStart = selectionStart > 0 ? selectionStart - 1 : 0;
            }
            else
            {
                Text = Text.Insert(_prefixLen, _negativeSign.ToString());
                SelectionStart = selectionStart + 1;
            }

            return true;
        }

        private bool IsDigitPress(char keyChar)
        {
            if (_showPrefix && _prefixLen > 0 && Text.StartsWith(_currencySimbol) && SelectionStart < _currencySimbol.Length + 1)
                SelectionStart = _currencySimbol.Length;

            var p = Text.IndexOf(_decimalSeparator);
            if (MaxWholeDigits > 0 && (p < 0 || SelectionStart <= p))
            {
                var hasprefix = Text.StartsWith(_currencySimbol);

                var numdigit = (p >= 0 ? p : Text.Length) - (hasprefix ? _prefixLen : 0) - Text.Count(f => f == _groupSeparator);

                if (numdigit >= MaxWholeDigits)
                {
                    if (SelectionStart < Text.Length)
                    {
                        if (Text[SelectionStart] == _groupSeparator || Text[SelectionStart] == _decimalSeparator || Text[SelectionStart] == _negativeSign)
                            SelectionStart++;

                        var selectionStart = SelectionStart;
                        var sb = new StringBuilder(Text);
                        sb[SelectionStart] = keyChar;
                        Text = sb.ToString();
                        if (SelectionStart < Text.Length)
                            SelectionStart = selectionStart + 1;
                    }
                    return true;

                    //char nextChar = SelectionStart = 
                    //
                    //return true;
                }
            }
            if (p > 0 && MaxDecimalPlaces > 0 && SelectionStart > p)
            {
                var numdecimal = Text.Length - p;
                if (numdecimal > MaxDecimalPlaces)
                {
                    if (SelectionStart < Text.Length)
                    {
                        var selectionStart = SelectionStart;
                        var sb = new StringBuilder(Text);
                        sb[SelectionStart] = keyChar;
                        Text = sb.ToString();
                        if (SelectionStart < Text.Length)
                            SelectionStart = selectionStart + 1;
                    }
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Properties Designer
        internal class Designer : ControlDesigner
        {
            protected override void PostFilterProperties(IDictionary attributes)
            {
                attributes.Remove("TextAlign");
                attributes.Remove("ToUpper");
                base.PostFilterProperties(attributes);
            }
        } 
        #endregion

    }
        public class NumericFormatHelper
        {

            #region Private field
            private readonly char _decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
            private readonly char _groupSeparator = NumberFormatInfo.CurrentInfo.NumberGroupSeparator[0];
            private readonly char _negativeSign = NumberFormatInfo.CurrentInfo.NegativeSign[0];
            private string _prefix = string.Empty;
            private int _prefixLen;
            private bool _showPrefix = false;
            #endregion


        #region Public properties
        public bool AllowGroupSeparator { get; set; }
            public int MaxWholeDigits { get; set; }
            public String Prefix
            {
                get { return _prefix; }
                set { _prefix = value; _prefixLen =_showPrefix ? value.Length : 0; }
            }

        public bool ShowPrefix
        {
            get { return _showPrefix; }
            set { _showPrefix = value; _prefixLen = _prefix.Length; }
        }
            #endregion

            public string GetFormatText(string text)
            {
                var numericText = GetNumericText(text);
            var posDecimalSeparator = numericText.IndexOf(_decimalSeparator);
            var integerText = posDecimalSeparator >= 0 ? numericText.Substring(0, posDecimalSeparator) : numericText;

            if ((integerText.Length - (posDecimalSeparator >= 0 ? 1 : 0)) > MaxWholeDigits)
                    numericText = numericText.Substring(0, MaxWholeDigits) + (posDecimalSeparator >= 0 ? _decimalSeparator.ToString() : "");

                var separatedText = numericText;

            var cursorPos = 0;
            var groupCount = text.Count(f => f == _groupSeparator);

            // Retrieve the number without the decimal point
            var decimalPos = numericText.IndexOf(_decimalSeparator);
            if (decimalPos >= 0)
                    separatedText = separatedText.Substring(0, decimalPos);

                if (AllowGroupSeparator)
                {
                    var length = separatedText.Length;
                var isNegative = (separatedText != "" && separatedText[0] == _negativeSign);

                // Loop in reverse and stick the separator every m_digitsInGroup digits.
                for (int iPos = length - 4; iPos >= (isNegative ? 1 : 0); iPos -= 3)
                        separatedText = separatedText.Substring(0, iPos + 1) + _groupSeparator + separatedText.Substring(iPos + 1);
                }

                // Prepend the prefix, if the number is not empty.
                if (separatedText != "" || decimalPos >= 0)
                {
                    if (_prefixLen > 0)
                    {
                        separatedText = _prefix + separatedText;
                        if (!text.StartsWith(_prefix))
                            cursorPos += _prefixLen;
                    }

                    if (decimalPos >= 0)
                        separatedText += numericText.Substring(decimalPos);
                }

                return separatedText;

            }

            private string GetNumericText(string text)
            {
                var firstdigit = _prefixLen > 0 && text.StartsWith(_prefix) ? _prefixLen : 0;
            int lastdigit = text.Length;

                var sb = new StringBuilder();

            for (int t = firstdigit; t < lastdigit; t++)
                    if (char.IsDigit(text[t]) || text[t] == _negativeSign || text[t] == _decimalSeparator)
                        sb.Append(text[t]);

                return sb.ToString();
            }
        }
}
