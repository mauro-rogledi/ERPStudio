using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ERPFramework.Controls
{
    public class MetroGridTotalControl : MetroFramework.Controls.MetroLabel
    {
        #region Private field
        private char _decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
        private char _groupSeparator = NumberFormatInfo.CurrentInfo.NumberGroupSeparator[0];
        private readonly char _negativeSign = NumberFormatInfo.CurrentInfo.NegativeSign[0];
        private string _currencySimbol = NumberFormatInfo.CurrentInfo.CurrencySymbol;
        private string _currentDateFormat = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
        private string _prefix = string.Empty;
        private int _prefixLen;
        private bool _showPrefix = false;

        #endregion

        [Category("MetroFramework Extender Numeric")]
        [Description("The maximum number of digits allowed left of the decimal point.")]
        public int MaxWholeDigits { get; set; }

        [Category("MetroFramework Extender Numeric")]
        [Description("The maximum number of digits allowed right of the decimal point.")]
        public int MaxDecimalPlaces { get; set; }

        [Category("MetroFramework Extender Numeric")]
        [Description("Determines whether the value is allowed to be negative or not.")]
        public bool AllowNegative { get; set; }

        [Category("MetroFramework Extender Numeric")]
        [Description("Determines whether the value is allowed to be negative or not.")]
        [DefaultValue(false)]
        public bool ShowPrefix
        {
            get { return _showPrefix; }
            set { _showPrefix = value; _prefixLen = _showPrefix ? _currencySimbol.Length : 0; }
        }

        [Category("MetroFramework Extender Numeric")]
        [Description("Determines whether the value is allowed to be negative or not.")]
        [DefaultValue(false)]
        public string CurrencySimbol
        {
            get { return _currencySimbol; }
            set { _currencySimbol = value; _prefixLen = _currencySimbol.Length; }
        }

        [Category("MetroFramework Extender Numeric")]
        [Description("Allow insert of group separator in number")]
        public bool AllowGroupSeparator { get; set; }

        public double Double
        {
            set { Text = ConvertInDouble(value); }
        }

        public int Int
        { 
            set { Text = ConvertInDouble((double)value); }
        }

        public DateTime DateTime
        {
            set { Text = ConvertInDate(value); }
        }

        private string ConvertInDate(DateTime value)
        {
            return value.ToString(_currentDateFormat);
        }

        private string ConvertInDouble(double value)
        {
            var numericText = value.ToString();
            var posDecimalSeparator = numericText.IndexOf(_decimalSeparator);
            var integerText = posDecimalSeparator >= 0 ? numericText.Substring(0, posDecimalSeparator) : numericText;

            if ((integerText.Length - (posDecimalSeparator >= 0 ? 1 : 0)) > MaxWholeDigits)
                numericText = numericText.Substring(0, MaxWholeDigits) + (posDecimalSeparator >= 0 ? _decimalSeparator.ToString() : "");

            var separatedText = numericText;

            var cursorPos = 0;
            var groupCount = value.ToString().Count(f => f == _groupSeparator);

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
                if (_prefixLen > 0 && _showPrefix)
                {
                    separatedText = _currencySimbol + separatedText;
                    if (!value.ToString().StartsWith(_currencySimbol))
                        cursorPos += _prefixLen;
                }

                if (decimalPos >= 0)
                    separatedText += numericText.Substring(decimalPos);
            }

            return separatedText;
        }
    }
}
