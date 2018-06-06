using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using ERPFramework.Controls;
using ERPFramework.Data;
using MetroFramework;
using MetroFramework.Extender;
using ERPFramework.Forms;

namespace ERPFramework.CounterManager
{
    public class MetroCounterTextBox : MetroTextBoxButtonState, ICounterManager, IFindable, IClickable
    {
        #region Fields
        private int year;

        private CounterManager counterUpdater;
        private RRCounter rRCounter;
        private CounterText counterText;
        private CounterProperties counterProperties;
        private Type iradarform;
        private IRadarForm radarForm;
        object[] args;

        public object[] RadarArgs { get { return args; } }

        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DisplayName("Description Control")]
        public Control DescriptionControl
        {
            get; set;
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        public bool MustExistData { get; set; } = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        public bool EnableAddOnFly { get; set; } = false;
        
        [Browsable(false)]
        public Type AttachRadarType
        {
            set { iradarform = value; CustomButton.Image = Properties.Resources.Search16; ShowButton = value != null; }
        }

        [Browsable(false)]
        public bool HasRadar { get { return iradarform != null; } }

        [Browsable(false)]
        public bool IsUpdatable { get; set; } = true;

        [Browsable(false)]
        public void AttachRadar<T>()
        {
            iradarform = typeof(T);
            ShowButton = true;
            CustomButton.Image = Properties.Resources.Search16;
        }

        [Browsable(false)]
        public void AttachRadar<T>(params object[] args)
        {
            iradarform = typeof(T);
            this.args = new object[args.Length];
            for (int t = 0; t < args.Length; t++)
                this.args[t] = args[t];
            CustomButton.Image = Properties.Resources.Search16;
            ShowButton = true;
        }

        [Browsable(false)]
        public string Description { get; private set; }

        [Browsable(false)]
        public int Value {
            get
            {
                return counterText.Value;
            }
        }

        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return Text.IsEmpty() || counterText.Empty == Text;
            }
        }

        [Browsable(false)]
        public bool WasEmpty
        {
            get
            {
                return OldText.IsEmpty() || counterText.Empty == OldText;
            }
        }

        #endregion

        #region Constructor
        public MetroCounterTextBox()
        {
            counterText = new CounterText(this);
            Validating += MetroCounterTextBox_Validating;
            Validated += MetroCounterTextBox_Validated;
        }

        private void MetroCounterTextBox_Validated(object sender, EventArgs e)
        {
            if (radarForm == null || DescriptionControl == null) return;

            DescriptionControl.Text = Description;
        }

        private void MetroCounterTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (iradarform == null) return;
            string cnt = Text;

            radarForm = Activator.CreateInstance(iradarform, args) as IRadarForm;

            IRadarParameters iParam = radarForm.GetRadarParameters(cnt);

            if (!IsEmpty && !radarForm.Find(iParam))
            {
                if (EnableAddOnFly && radarForm.CanOpenNew)
                {
                    DialogResult result = MetroMessageBox.Show(GlobalInfo.MainForm, Properties.Resources.Msg_InsertNewCode,
                                                            Properties.Resources.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        radarForm.OpenNew(iParam);
                        Focus();
                    }
                }
                else if (MustExistData)
                {
                    MessageBox.Show(GlobalInfo.MainForm, Properties.Resources.Msg_CodeNotFound,
                                                            Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Focus();
                }
                e.Cancel = MustExistData;
            }
            else
            {
                if (cnt == string.Empty)
                {
                    Description = string.Empty;
                    return;
                }
            }
            Description = radarForm.Description;
        }

        protected override void OnButtonClick(EventArgs e)
        {
            if (IsStatesButton)
            {
                if (!StatesButton)
                    GetNewValue(true);
            }
            else
                Search();
        }

        private void MetroCounterTextBox_ButtonClick(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (iradarform != null)
            {
                radarForm = Activator.CreateInstance(iradarform, args) as IRadarForm;
                ERPFramework.GlobalInfo.StyleManager.Clone(radarForm as RadarForm);

                radarForm.RadarFormRowSelected += new RadarForm.RadarFormRowSelectedEventHandler(RadarForm_RadarFormRowSelected);
                radarForm.Seed = Text;
                if (radarForm.ShowDialog(GlobalInfo.MainForm) == DialogResult.OK)
                {
                    Focus();
                }
                radarForm.Dispose();
            }
        }

        private void RadarForm_RadarFormRowSelected(object sender, RadarFormRowSelectedArgs pe)
        {
            Text = pe.parameters.GetValue<string>(EF_Counter.Year);
            UpdateDescription();

            Description = radarForm.Description;
        }

        #endregion

        public bool OpenDocument(string val = "")
        {
            if (iradarform == null || Text.IsEmpty())
                return false;

            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            radarForm = Activator.CreateInstance(iradarform, args) as IRadarForm;
            IRadarParameters iParam = radarForm.GetRadarParameters(Text);
            radarForm.OpenBrowse(iParam);

            this.Cursor = Cursors.Default;

            return false;
        }

        public void UpdateDescription()
        {
            if (DescriptionControl == null)
                return;

            if (iradarform != null)
               radarForm = Activator.CreateInstance(iradarform, args) as IRadarForm;

                IRadarParameters iParam = radarForm.GetRadarParameters(Text);
                DescriptionControl.Text = (Text != string.Empty && radarForm.Find(iParam))
                                        ? radarForm.Description
                                        : string.Empty;

            radarForm.Dispose();
        }

        /// <summary>
        /// Attach Counter
        /// </summary>
        /// <param name="key"></param>
        /// <param name="curDate"></param>
        /// <param name="transaction"></param>
        public virtual void AttachCounterType(int key, DateTime curDate, IDocumentBase documentBase)
        {
            year = curDate.Year;

            rRCounter = new RRCounter(null);
            if (!rRCounter.Find(curDate.Year, key))
            {
                MetroFramework.MetroMessageBox.Show(GlobalInfo.MainForm, Properties.Resources.Msg_MissingCounter,
                Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            counterProperties = new CounterProperties();
            counterProperties.PrefixVisible = rRCounter.GetValue<bool>(EF_Counter.HasPrefix);
            counterProperties.PrefixReadOnly = rRCounter.GetValue<bool>(EF_Counter.PrefixRO);
            counterProperties.PrefixType = rRCounter.GetValue<PrefixSuffixType>(EF_Counter.PrefixType);
            counterProperties.PrefixSeparator = rRCounter.GetValue<string>(EF_Counter.PrefixSep);
            counterProperties.PrefixDefault = GetPrefixSuffixValue(counterProperties.PrefixType, year, rRCounter.GetValue<string>(EF_Counter.PrefixValue));
            counterProperties.PrefixValue = "";
            counterProperties.PrefixLen = counterProperties.PrefixDefault.Length;

            counterProperties.CounterLen = rRCounter.GetValue<int>(EF_Counter.CodeLen);

            counterProperties.SuffixVisible = rRCounter.GetValue<bool>(EF_Counter.HasSuffix);
            counterProperties.SuffixReadOnly = rRCounter.GetValue<bool>(EF_Counter.SuffixRO);
            counterProperties.SuffixType = rRCounter.GetValue<PrefixSuffixType>(EF_Counter.SuffixType);
            counterProperties.SuffixSeparator = rRCounter.GetValue<string>(EF_Counter.SuffixSep);
            counterProperties.SuffixDefault = GetPrefixSuffixValue(counterProperties.SuffixType, year, rRCounter.GetValue<string>(EF_Counter.SuffixValue));
            counterProperties.SuffixValue = "";
            counterProperties.SuffixLen = counterProperties.SuffixDefault.Length;

            counterText.SetProperties(counterProperties);

            counterUpdater = new CounterManager(key, curDate.Year, rRCounter.GetValue<string>(EF_Counter.Description), documentBase);

            ShowButton = true;
            ImageOn = Properties.Resources.Automatic16;
            ImageOff = Properties.Resources.Manual16;

            this.Refresh();
        }

        private string GetPrefixSuffixValue(PrefixSuffixType type, int year, string suffix)
        {
            switch (type)
            {
                case PrefixSuffixType.ApplicationYear:
                    return year != 0
                                ? year.ToString()
                                : DateTime.Today.Year.ToString();
                case PrefixSuffixType.ApplicationYearShort:
                    return year != 0
                                ? year.ToString().Substring(2)
                                : DateTime.Today.Year.ToString().Substring(2);
                default:
                    return suffix;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (counterProperties!=null)
            {
                counterProperties.CounterValue = counterText.Value;
            }
        }

        #region ICounterManager
        public void GetNewValue(bool reset = false)
        {
            if (counterUpdater == null)
                return;

            counterProperties.CounterValue = counterUpdater.GetNewIntValue();
            counterText.FormatText(reset);

        }
        public void UpdateValue(DBOperation operation)
        {
            switch (operation)
            {
                case DBOperation.Get:
                    GetNewValue();
                    break;

                case DBOperation.New:
                    if (!StatesButton)
                        GetNewValue();
                    counterUpdater.SetValue(counterProperties.CounterValue);
                    if (DataBindings["Text"] != null)
                        DataBindings["Text"].WriteValue();
                    break;

                case DBOperation.Delete:
                    counterUpdater.DeleteValue(counterProperties.CounterValue);
                    break;
            }
        }
        #endregion

        #region IFindable
        [Browsable(false)]
        public void Clean()
        {
            Text = string.Empty;
        }
        #endregion
    }

    internal class CounterProperties
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
        public int CounterValue;
    }

    internal class CounterText
    {
        private System.Windows.Forms.Control parent;
        private CounterProperties Properties;
        private CounterFormatHelper counterHelper;

        public int Value
        {
            get
            {
                counterHelper.DecodeText(parent.Text);
                return Properties.CounterValue;
            }
        }

        public CounterText(System.Windows.Forms.Control parent)
        {
            this.parent = parent;
            this.parent.Validating += Parent_Validating;

            counterHelper = new CounterFormatHelper();
        }

        public string Empty { get { return counterHelper.DecodeAndFormatText("", true); } }

        private void Parent_Validating(object sender, EventArgs e)
        {
            Validating(e);
        }

        public void SetProperties(CounterProperties properties)
        {
            Properties = properties;
            counterHelper.SetProperties(properties);
        }

        private void Validating(EventArgs e)
        {
            DecodeAndFormatText();
        }

        public void DecodeAndFormatText()
        {
            parent.Text = counterHelper.DecodeAndFormatText(parent.Text);
        }
        public void FormatText(bool reset = false)
        {
            parent.Text =  counterHelper.FormatText(reset);
        }
    }

    internal class CounterFormatHelper
    {
        private CounterProperties Properties;
        private StringBuilder sb = new StringBuilder();


        public void SetProperties(CounterProperties properties)
        {
            Properties = properties;
        }

        public string DecodeAndFormatText(string text, bool reset = false)
        {
            if (Properties == null)
                return text;
            DecodeText(text);

            return FormatText(reset);
        }

        public string FormatText(bool reset = false)
        {
            sb.Clear();
            if (Properties.CounterValue == 0 && !reset)
                return sb.ToString();

            if (Properties.PrefixVisible)
            {
                sb.Append(Properties.PrefixValue.IsEmpty() || reset ? Properties.PrefixDefault : Properties.PrefixValue);
                sb.Append(Properties.PrefixSeparator);
            }

            string format = Properties.PrefixVisible
                                ? string.Format("D{0}", Properties.CounterLen)
                                : "D";
            sb.AppendFormat(Properties.CounterValue.ToString(format));

            if (Properties.SuffixVisible)
            {
                sb.Append(Properties.SuffixSeparator);
                sb.Append(Properties.SuffixValue.IsEmpty() || reset ? Properties.SuffixDefault : Properties.SuffixValue);
            }

            return sb.ToString();
        }

        public void DecodeText(string text)
        {
            if (Properties == null)
                return;
            Properties.PrefixValue = "";
            Properties.SuffixValue = "";
            Properties.CounterValue = 0;

            int pos = 0;
            if (Properties.PrefixVisible)
                pos = GetPrefix(text, pos);
            pos = GetCounter(text, pos);
            if (Properties.SuffixVisible)
                pos = GetSuffix(text, pos);
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


            int suffixpos = Properties.SuffixSeparator.IsEmpty()
                                ? -1
                                : (text.IndexOf(Properties.SuffixSeparator[0], pos));
            int len = 0;
            if (suffixpos > 0)
                len = suffixpos - pos;
            else
                len = Properties.CounterLen;

            bool ok = int.TryParse(text.Subsr(pos, len), out Properties.CounterValue);
            if (ok)
                pos += len;
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
