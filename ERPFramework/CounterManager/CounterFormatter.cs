using ERPFramework.Data;
using System;
using System.Windows.Forms;

namespace ERPFramework.CounterManager
{
    public class CounterFormatter
    {
        private DateTime curDate;
        private RRCounter rRCounter = null;
        private string counterTotal = "";
        private string prefixValue = "";
        private string suffixValue = "";

        #region Public Attributes

        public string Text
        {
            get
            {
                return this.ToString();
            }
            set
            {
                counterTotal = value;
                if (counterTotal != null && counterTotal.Length > 0)
                    Decode();
            }
        }

        public string ToString(bool inverted)
        {
            string text = "";
            if (inverted)
            {
                if (SuffixVisible)
                    text += SuffixValue + SuffixSeparator;
            }
            else
                if (PrefixVisible)
                    text += PrefixValue + PrefixSeparator;

            if (CounterType == CounterTypes.Text)
                text += CounterStringValue;
            else
            {
                string fmt = "{0," + CounterLen.ToString() + "}";
                text += string.Format(fmt, CounterNumericValue);
            }

            if (inverted)
            {
                if (PrefixVisible)
                    text += PrefixSeparator + PrefixValue;
            }
            else
                if (SuffixVisible)
                    text += SuffixSeparator + SuffixValue;

            return text;
        }

        public override string ToString()
        {
            string text = "";
            if (InvertedSuffixPrefix)
            {
                if (SuffixVisible)
                    text += SuffixValue + SuffixSeparator;
            }
            else
                if (PrefixVisible)
                    text += PrefixValue + PrefixSeparator;

            if (CounterType == CounterTypes.Text)
                text += CounterStringValue;
            else
            {
                string fmt = "{0," + CounterLen.ToString() + "}";
                text += string.Format(fmt, CounterNumericValue);
            }

            if (InvertedSuffixPrefix)
            {
                if (PrefixVisible)
                    text += PrefixSeparator + PrefixValue;
            }
            else
                if (SuffixVisible)
                    text += SuffixSeparator + SuffixValue;

            return text;
        }

        private void Decode()
        {
            int curpos = 0, sufpos = 0;
            if (InvertedSuffixPrefix)
            {
                if (SuffixVisible)
                {
                    curpos = SuffixLength;
                    SuffixValue = counterTotal.Substring(0, SuffixLength);
                    curpos++;
                }
            }
            else
                if (PrefixVisible)
                {
                    curpos = PrefixLength;
                    PrefixValue = counterTotal.Substring(0, PrefixLength);
                    curpos++;
                }

            if (InvertedSuffixPrefix)
            {
                if (PrefixVisible)
                {
                    sufpos = CounterLen +
                        (SuffixVisible ? SuffixLength + SuffixSeparator.Length : 0);

                    sufpos += PrefixSeparator.Length;

                    PrefixValue = counterTotal.Substring(sufpos);
                }
            }
            else
            {
                if (SuffixVisible)
                {
                    sufpos = CounterLen +
                        (PrefixVisible ? PrefixLength + PrefixSeparator.Length : 0);

                    sufpos += SuffixSeparator.Length;

                    SuffixValue = counterTotal.Substring(sufpos);
                }
            }

            if (CounterType == CounterTypes.Text)
            {
                CounterStringValue = sufpos > 0
                    ? counterTotal.Substring(curpos, sufpos - curpos - 1)
                    : counterTotal.Substring(curpos);
            }
            else
            {
                string numeric = sufpos > 0
                    ? counterTotal.Substring(curpos, sufpos - curpos - 1)
                    : counterTotal.Substring(curpos);
                int dummy = -1;
                if (int.TryParse(numeric, out dummy))
                    CounterNumericValue = dummy;
            }
        }

        public bool PrefixVisible { get; set; }

        public bool PrefixReadOnly { get; set; }

        public int PrefixLength { get; set; }

        public string PrefixMask { get; set; }

        public string PrefixValue { get; set; }

        public string PrefixSeparator { get; set; }

        public PrefixSuffixType PrefixMode { get; set; }

        public bool SuffixReadOnly { get; set; }

        public int SuffixLength { get; set; }

        public bool SuffixVisible { get; set; }

        public string SuffixSeparator { get; set; }

        public string SuffixMask { get; set; }

        public string SuffixValue { get; set; }

        public bool InvertedSuffixPrefix { get; set; }

        public CounterTypes CounterType { get; set; }

        public string CounterMask { get; set; }

        public PrefixSuffixType SuffixMode { get; set; }

        public int CounterLen { get; set; }

        public int CounterNumericValue { get; set; }

        public string CounterStringValue { get; set; }

        #endregion

        public void AttachCounterType(int key, DateTime curDate)
        {
            this.curDate = curDate;

            this.rRCounter = new RRCounter(false);
            if (!rRCounter.Find(curDate.Year, key))
            {
                MessageBox.Show(Properties.Resources.Msg_MissingCounter,
                Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.PrefixVisible = rRCounter.GetValue<bool>(AM_Counter.HasPrefix);
            this.PrefixReadOnly = rRCounter.GetValue<bool>(AM_Counter.PrefixRO);
            this.PrefixMode = (PrefixSuffixType)rRCounter.GetValue<int>(AM_Counter.PrefixType);
            this.PrefixSeparator = rRCounter.GetValue<string>(AM_Counter.PrefixSep);
            this.PrefixValue = rRCounter.GetValue<string>(AM_Counter.PrefixValue);

            this.CounterLen = rRCounter.GetValue<int>(AM_Counter.CodeLen);
            this.CounterNumericValue = 0;

            this.SuffixVisible = rRCounter.GetValue<bool>(AM_Counter.HasSuffix);
            this.SuffixReadOnly = rRCounter.GetValue<bool>(AM_Counter.SuffixRO);
            this.SuffixMode = (PrefixSuffixType)rRCounter.GetValue<int>(AM_Counter.SuffixType);
            this.SuffixSeparator = rRCounter.GetValue<string>(AM_Counter.SuffixSep);
            this.SuffixValue = rRCounter.GetValue<string>(AM_Counter.SuffixValue);
            this.SuffixValue = GetSuffixValue();

        }

        private string GetPrefixValue()
        {
            switch (PrefixMode)
            {
                case PrefixSuffixType.ApplicationYear:
                    return curDate != DateTime.MinValue
                                ? curDate.Year.ToString()
                                : DateTime.Today.Year.ToString();
                case PrefixSuffixType.ApplicationYearShort:
                    return curDate != DateTime.MinValue
                                ? curDate.Year.ToString().Substring(2)
                                : DateTime.Today.Year.ToString().Substring(2);
            }
            return prefixValue;
        }

        private string GetSuffixValue()
        {
            switch (SuffixMode)
            {
                case PrefixSuffixType.ApplicationYear:
                    return curDate != DateTime.MinValue
                                ? curDate.Year.ToString()
                                : DateTime.Today.Year.ToString();
                case PrefixSuffixType.ApplicationYearShort:
                    return curDate != DateTime.MinValue
                                ? curDate.Year.ToString().Substring(2)
                                : DateTime.Today.Year.ToString().Substring(2);
            }
            return suffixValue;
        }
    }
}