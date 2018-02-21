using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFramework.CounterManager
{
    public class CounterProperties
    {
        public bool PrefixVisible { get; set; } = false;
        public bool PrefixReadOnly { get; set; } = false;
        public int PrefixLen { get; set; } = 0;
        public PrefixSuffixType PrefixType { get; set; } = PrefixSuffixType.Custom;
        public string PrefixSeparator { get; set; } = string.Empty;
        public string PrefixDefault { get; set; } = string.Empty;
        public string PrefixValue { get; set; } = string.Empty;

        public bool SuffixVisible { get; set; } = false;
        public bool SuffixReadOnly { get; set; } = false;
        public int SuffixLen { get; set; } = 0;
        public PrefixSuffixType SuffixType { get; set; } = PrefixSuffixType.ApplicationYearShort;
        public string SuffixSeparator { get; set; } = string.Empty;
        public string SuffixDefault { get; set; } = string.Empty;
        public string SuffixValue { get; set; } = string.Empty;

        public int CounterLen { get; set; } = 0;

        public int counterValue;
    }

    public class CounterText
    {
        private System.Windows.Forms.Control parent;
        private CounterProperties Properties;
        private CounterFormatHelper counterHelper;

        public CounterText(System.Windows.Forms.Control parent)
        {
            this.parent = parent;
            this.parent.LostFocus += Parent_LostFocus;

            counterHelper = new CounterFormatHelper();
        }

        private void Parent_LostFocus(object sender, EventArgs e)
        {
            LostFocus(e);
        }

        public void SetProperties(CounterProperties properties)
        {
            Properties = properties;
            counterHelper.SetProperties(properties);
        }

        public void LostFocus(EventArgs e)
        {
            parent.Text = counterHelper.FormatText(parent.Text);
        }
    }

    public class CounterFormatHelper
    {
        private CounterProperties Properties;
        private StringBuilder sb = new StringBuilder();

        public void SetProperties(CounterProperties properties)
        {
            Properties = properties;
        }

        public string FormatText(string text)
        {
            if (Properties == null)
                return text;

            int pos = 0;
            if (Properties.PrefixVisible)
                pos = GetPrefix(text, pos);
            pos = GetCounter(text, pos);
            if (Properties.SuffixVisible)
                pos = GetSuffix(text, pos);

            sb.Clear();
            if (Properties.PrefixVisible)
            {
                sb.Append(Properties.PrefixValue.IsEmpty() ? Properties.PrefixDefault : Properties.PrefixValue);
                sb.Append(Properties.PrefixSeparator);
            }

            string format = string.Format("D{0}", Properties.CounterLen);
            sb.AppendFormat(Properties.counterValue.ToString(format));

            if (Properties.SuffixVisible)
            {
                sb.Append(Properties.SuffixSeparator);
                sb.Append(Properties.SuffixValue.IsEmpty() ? Properties.SuffixDefault : Properties.SuffixValue);
            }

            return sb.ToString();
        }

        private int GetPrefix(string text, int pos)
        {
            if (pos >= text.Length)
                return pos;

            int prefixPos = text.IndexOf(Properties.PrefixSeparator, pos);
            if (prefixPos == -1)
                return pos;

            Properties.PrefixValue = text.Subsr(pos, Properties.PrefixLen);
            pos += Properties.PrefixValue.Length;

            if (!Properties.PrefixSeparator.IsEmpty())
            {
                if (text[pos] == Properties.PrefixSeparator[0])
                    pos++;
            }

            return pos;
        }

        private int GetCounter(string text, int pos)
        {
            if (pos >= text.Length)
                return pos;

            int suffixpos = (text.IndexOf(Properties.SuffixSeparator[0], pos));
            int len = 0;
            if (suffixpos > 0)
                len = suffixpos - pos;
            else
                len = Properties.CounterLen;

            bool ok = int.TryParse(text.Subsr(pos, len), out Properties.counterValue);
            if (ok)
                pos += Properties.CounterLen;
            if (pos > text.Length)
                pos = text.Length - 1;

            return pos;
        }

        private int GetSuffix(string text, int pos)
        {
            if (pos >= text.Length)
                return pos;

            int suffixPos = text.IndexOf(Properties.SuffixSeparator, pos);
            if (suffixPos == -1)
                return pos;

            if (!Properties.SuffixSeparator.IsEmpty())
            {
                if (text[pos] == Properties.SuffixSeparator[0])
                    pos++;
            }

            Properties.SuffixValue = text.Subsr(pos, Properties.SuffixLen);
            pos += Properties.SuffixValue.Length;

            return pos;
        }
    }
}
