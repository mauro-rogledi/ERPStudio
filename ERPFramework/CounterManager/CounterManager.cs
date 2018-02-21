using ERPFramework.Data;
using ERPFramework.Forms;
using System;

namespace ERPFramework.CounterManager
{
    public enum PrefixSuffixType { Custom = 0, ApplicationYear=1, ApplicationYearShort=2 };

    public enum FiscalKey { None=0, Empty=1, Prefix=2, Suffix=3, PrefixAndSuffix=4 };

    public class CounterManager
    {
        private int counterKey = -1;
        private readonly DUCounterValue tuCounterValue;
        private readonly RRCounter trCounter;
        private readonly int currentYear = DateTime.Today.Year;
        private string currentCode = string.Empty;
        private string description = string.Empty;

        public CounterManager(int key, int Year, string description, IDocumentBase iDocumentBase)
        {
            counterKey = key;
            this.description = description;
            trCounter = new RRCounter(iDocumentBase);
            tuCounterValue = new DUCounterValue(iDocumentBase);
            currentYear = Year;
            trCounter.Find(Year, key);
            currentCode = MakeCode();
        }

        public int GetNewIntValue()
        {
            // Leggo il valore corrente
            tuCounterValue.Find(counterKey, currentCode);
            return GetCounterIntValue(1);
        }

        public void SetValue(int val)
        {
            if (!tuCounterValue.Find(counterKey, currentCode))
                tuCounterValue.AddRecord();

            if (tuCounterValue.GetValue<int>(AM_CounterValue.NumericValue) < val)
            {
                tuCounterValue.SetValue<int>(AM_CounterValue.NumericValue, val);
                tuCounterValue.SetValue<string>(AM_Counter.Description, description);
                tuCounterValue.Update();
            }
        }

        public void DeleteValue(int val)
        {
            tuCounterValue.Find(counterKey, currentCode);
            if (tuCounterValue.GetValue<int>(AM_CounterValue.NumericValue) == val && val > 0)
            {
                tuCounterValue.SetValue<int>(AM_CounterValue.NumericValue, val - 1);
                tuCounterValue.SetValue<string>(AM_Counter.Description, description);
                tuCounterValue.Update();
            }
        }

        private int GetCounterIntValue(int sum)
        {
            tuCounterValue.Find(counterKey, currentCode);
            return tuCounterValue.GetValue<int>(AM_CounterValue.NumericValue) + sum;
        }

        private string GetCounterStringValue(int sum)
        {
            var numCounter = tuCounterValue.GetValue<int>(AM_CounterValue.NumericValue) + sum;
            var formatNum = " ".Substring(1, trCounter.GetValue<int>(AM_Counter.CodeLen));

            return numCounter.ToString(formatNum);
        }

        public string CreateCounterFormatted(int sum)
        {
            var counter = string.Empty;

            // Prendo il prefisso
            if (trCounter.GetValue<bool>(AM_Counter.HasPrefix))
            {
                counter = GetPrefixText((PrefixSuffixType)trCounter.GetValue<int>(AM_Counter.PrefixType),
                                                            trCounter.GetValue<string>(AM_Counter.PrefixValue));
                if (trCounter.GetValue<string>(AM_Counter.PrefixSep).Length > 0)
                    counter += trCounter.GetValue<string>(AM_Counter.PrefixSep).ToString();
            }

            var codelen = trCounter.GetValue<int>(AM_Counter.CodeLen);
            var formatNum = "###############".Substring(1, codelen);
            var numCounter = tuCounterValue.GetValue<int>(AM_CounterValue.NumericValue) + sum;
            var formatter = string.Concat("{0,", codelen, "}");
            counter += string.Format(formatter, numCounter.ToString(formatNum));

            if (trCounter.GetValue<bool>(AM_Counter.HasSuffix))
            {
                counter = GetPrefixText((PrefixSuffixType)trCounter.GetValue<int>(AM_Counter.SuffixType),
                                                            trCounter.GetValue<string>(AM_Counter.SuffixValue));
                if (trCounter.GetValue<string>(AM_Counter.SuffixSep).Length > 0)
                    counter += trCounter.GetValue<string>(AM_Counter.SuffixSep).ToString();
            }

            return counter;
        }

        private string MakeCode()
        {
            string key, suffix, prefix;
            key = suffix = prefix = string.Empty;

            var fiscalKey = (FiscalKey)trCounter.GetValue<int>(AM_Counter.CodeKey);
            if (trCounter.GetValue<bool>(AM_Counter.HasPrefix))
                prefix = GetPrefixText((PrefixSuffixType)trCounter.GetValue<int>(AM_Counter.PrefixType),
                                                            trCounter.GetValue<string>(AM_Counter.PrefixValue));

            if (trCounter.GetValue<bool>(AM_Counter.HasSuffix))
                suffix = GetPrefixText((PrefixSuffixType)trCounter.GetValue<int>(AM_Counter.SuffixType),
                                                            trCounter.GetValue<string>(AM_Counter.SuffixValue));
            switch (fiscalKey)
            {
                case FiscalKey.Prefix:
                    key = prefix;
                    break;

                case FiscalKey.Suffix:
                    key = suffix;
                    break;

                default:
                    key = prefix + suffix;
                    break;
            }
            return key;
        }

        private string GetPrefixText(PrefixSuffixType PrefixMode, string custom)
        {
            switch (PrefixMode)
            {
                case PrefixSuffixType.ApplicationYear:
                    return currentYear.ToString();
                case PrefixSuffixType.ApplicationYearShort:
                    return currentYear.ToString().Substring(2);
                case PrefixSuffixType.Custom:
                    return custom;
            }
            return "";
        }
    }
}