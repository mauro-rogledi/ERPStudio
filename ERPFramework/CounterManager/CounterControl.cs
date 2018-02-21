using ERPFramework.Controls;
using ERPFramework.Data;
using MetroFramework.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERPFramework.CounterManager
{
    [Designer(typeof(MySnapLinesDesigner))]
    public partial class CounterControl : MetroUserControl, IFindable, ISnapLineControl, ICounterManager
    {
        private MetroLabel prefixSep = null;
        private MetroLabel prefixMetroLabel = null;
        private MetroTextBox prefixMasked = null;
        private MetroLabel suffixSep;
        private MetroLabel suffixMetroLabel = null;
        private MetroTextBox suffixMasked = null;
        private NumericTextBox numericCounterCtrl = null;
        private MetroTextBox textCounterCtrl = null;
        private Graphics graphics = null;
        private MetroButton btnEdit = null;
        private string prefixValue = "";
        private string suffixValue = "";
        private string defaultPrefix = "";
        private string defaultSuffix = "";
        private int counterNumericValue = 0;
        private string counterStringValue = "";
        private string counterTotal = "";
        private DateTime curDate;
        private bool isAutomatic = true;

        public bool EditButtonVisible
        {
            set
            {
                if (value != btnEdit.Visible)
                {
                    if (value)
                        this.Width += btnEdit.Width;
                    else
                        this.Width -= btnEdit.Width;
                }
                btnEdit.Visible = value;
            }
        }

        public bool IsAutomatic
        {
            set
            {
                ChangeButtonImage();
                isAutomatic = value;
            }
            get { return isAutomatic; }
        }

        private MetroTextBox designText = null;

        public Control textBoxValue { get { return designText; } }

        public Control labelValue { get { return null; } }

        private CounterManager counterUpdater = null;
        private RRCounter rRCounter = null;

        #region Design Layout

        private void AddDesignModeControl()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
                return;

            if (designText == null)
            {
                designText = new MetroTextBox();
                designText.Text = "Text";
                designText.Location = new Point(0, 0);
                this.Controls.Add(designText);
            }
        }

        #endregion

        #region Public Attributes

        [System.ComponentModel.Bindable(true, BindingDirection.TwoWay)]
        public override string Text
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

        public override string ToString()
        {
            return ToString(InvertedSuffixPrefix);
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

        [Category("Counter")]
        [Browsable(true)]
        public bool PrefixVisible { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        public bool PrefixReadOnly { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        public int PrefixLength { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        [DefaultValue("")]
        public string PrefixMask { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        [DefaultValue("")]
        public string PrefixValue
        {
            get
            {
                if (prefixMasked != null)
                    return prefixMasked.Text;
                else
                    return prefixValue;
            }
            set
            {
                if (prefixMasked != null)
                    prefixMasked.Text = value;
                if (prefixMetroLabel != null)
                    prefixMetroLabel.Text = value;
                prefixValue = value;
            }
        }

        [Category("Counter")]
        [Browsable(true)]
        [DefaultValue("")]
        public string PrefixSeparator { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        public PrefixSuffixType PrefixMode { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        public bool SuffixReadOnly { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        public int SuffixLength { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        public bool SuffixVisible { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        [DefaultValue("")]
        public string SuffixSeparator { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        [DefaultValue("")]
        public string SuffixMask { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        [DefaultValue("")]
        public string SuffixValue
        {
            get
            {
                if (suffixMasked != null)
                    return suffixMasked.Text;
                else
                    return suffixValue;
            }
            set
            {
                if (suffixMasked != null)
                    suffixMasked.Text = value;
                if (suffixMetroLabel != null)
                    suffixMetroLabel.Text = value;
                suffixValue = value;
            }
        }

        [Category("Counter")]
        [Browsable(true)]
        public bool InvertedSuffixPrefix { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        public CounterTypes CounterType { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        [DefaultValue("")]
        public string CounterMask { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        public PrefixSuffixType SuffixMode { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        [DefaultValue(0)]
        public int CounterLen { get; set; }

        [Category("Counter")]
        [Browsable(true)]
        public int CounterNumericValue
        {
            get
            {
                if (numericCounterCtrl != null)
                {
                    if (int.TryParse(numericCounterCtrl.Text, out counterNumericValue))
                        return counterNumericValue;
                    else
                        return 0;
                }
                else
                    return counterNumericValue;
            }
            set
            {
                if (numericCounterCtrl != null)
                    numericCounterCtrl.Text = value.ToString();
                else
                    counterNumericValue = value;
            }
        }

        [Category("Counter")]
        [Browsable(true)]
        public string CounterStringValue
        {
            get
            {
                if (textCounterCtrl != null)
                    return textCounterCtrl.Text;
                else
                    return counterStringValue;
            }
            set
            {
                if (textCounterCtrl != null)
                    textCounterCtrl.Text = value;
                else
                    counterStringValue = value;
            }
        }

        #endregion

        #region Public Methods

        public void GetNewValue()
        {
            if (counterUpdater == null)
                return;

            if (numericCounterCtrl != null)
                numericCounterCtrl.Int = counterUpdater.GetNewIntValue();
            else if (textCounterCtrl != null)
                textCounterCtrl.Text = counterUpdater.GetNewStringValue();

            if (suffixMasked != null)
                suffixMasked.Text = GetSuffixValue();
            if (suffixMetroLabel != null)
                suffixMetroLabel.Text = GetSuffixValue();
        }

        public void UpdateValue(DBOperation operation)
        {
            switch (operation)
            {
                case DBOperation.Get:
                    GetNewValue();
                    break;

                case DBOperation.New:
                    if (numericCounterCtrl != null)
                    {
                        if (IsAutomatic)
                            GetNewValue();

                        counterUpdater.SetValue(numericCounterCtrl.Int);
                        this.DataBindings["Text"].WriteValue();
                    }
                    else if (textCounterCtrl != null)
                        counterUpdater.SetValue(textCounterCtrl.Text);
                    break;

                case DBOperation.Delete:
                    if (numericCounterCtrl != null)
                        counterUpdater.DeleteValue(numericCounterCtrl.Int);
                    break;
            }
        }

        public virtual void AttachCounterType(int key, DateTime curDate, SqlABTransaction transaction)
        {
            System.Diagnostics.Debug.Assert(counterUpdater == null);

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
            //this.PrefixLength = rRCounter.GetValue<int>(AM_Counter.PrefixLen);
            //this.PrefixMask = rRCounter.GetValue<string>(AM_Counter.PrefixMask);
            this.PrefixMode = (PrefixSuffixType)rRCounter.GetValue<int>(AM_Counter.PrefixType);
            this.PrefixSeparator = rRCounter.GetValue<string>(AM_Counter.PrefixSep);
            this.defaultPrefix = rRCounter.GetValue<string>(AM_Counter.PrefixValue);
            this.PrefixValue = GetPrefixValue();

            //this.CounterType = (CounterTypes)rRCounter.GetValue<int>(AM_Counter.CodeType);
            this.CounterLen = rRCounter.GetValue<int>(AM_Counter.CodeLen);
            this.CounterNumericValue = 0;

            this.SuffixVisible = rRCounter.GetValue<bool>(AM_Counter.HasSuffix);
            this.SuffixReadOnly = rRCounter.GetValue<bool>(AM_Counter.SuffixRO);
            //this.SuffixLength = rRCounter.GetValue<int>(AM_Counter.SuffixLen);
            //this.SuffixMask = rRCounter.GetValue<string>(AM_Counter.SuffixMask);
            this.SuffixMode = (PrefixSuffixType)rRCounter.GetValue<int>(AM_Counter.SuffixType);
            this.SuffixSeparator = rRCounter.GetValue<string>(AM_Counter.SuffixSep);
            this.defaultSuffix = rRCounter.GetValue<string>(AM_Counter.SuffixValue);
            this.SuffixValue = GetSuffixValue();

            //this.InvertedSuffixPrefix = rRCounter.GetValue<bool>(AM_Counter.InvertSufPref);

            counterUpdater = new CounterManager(key, curDate.Year, rRCounter.GetValue<string>(AM_Counter.Description));
            counterUpdater.Transaction = transaction;
            this.Refresh();
        }

        #endregion

        public CounterControl()
        {
            InitializeComponent();
            AddDesignModeControl();
        }

        protected override void OnLoad(EventArgs e)
        {
            graphics = Graphics.FromHwnd(this.Handle);
            int newctrlpos = 0;

            if (PrefixVisible)
                newctrlpos = AddPrefix(newctrlpos);

            if (CounterType == CounterTypes.Numeric)
                newctrlpos = AddNumericCounter(newctrlpos);
            else
                newctrlpos = AddTextCounter(newctrlpos);

            if (SuffixVisible)
                newctrlpos = AddSuffix(newctrlpos);
            this.Width = newctrlpos + 2;

            newctrlpos = AddButton(newctrlpos);
            ChangeButtonImage();

            this.Width = newctrlpos + 2;

            base.OnLoad(e);
        }

        public override void Refresh()
        {
            if (prefixMetroLabel != null && this.Controls.Contains(prefixMetroLabel))
                this.Controls.Remove(prefixMetroLabel);

            if (prefixMasked != null && this.Controls.Contains(prefixMasked))
                this.Controls.Remove(prefixMasked);

            if (prefixSep != null && this.Controls.Contains(prefixSep))
                this.Controls.Remove(prefixSep);

            if (suffixMetroLabel != null && this.Controls.Contains(suffixMetroLabel))
                this.Controls.Remove(suffixMetroLabel);

            if (suffixMasked != null && this.Controls.Contains(suffixMasked))
                this.Controls.Remove(suffixMasked);

            if (suffixSep != null && this.Controls.Contains(suffixSep))
                this.Controls.Remove(suffixSep);

            if (numericCounterCtrl != null && this.Controls.Contains(numericCounterCtrl))
                this.Controls.Remove(numericCounterCtrl);

            if (textCounterCtrl != null && this.Controls.Contains(textCounterCtrl))
                this.Controls.Remove(textCounterCtrl);

            if (btnEdit != null && this.Controls.Contains(btnEdit))
                this.Controls.Remove(btnEdit);

            prefixMetroLabel = null;
            prefixMasked = null;
            prefixSep = null;

            suffixMetroLabel = null;
            suffixMasked = null;
            suffixSep = null;

            OnLoad(new EventArgs());

            base.Refresh();
        }

        public bool IsEmpty
        {
            get
            {
                if (PrefixVisible && !PrefixReadOnly && PrefixValue != defaultPrefix)
                    return false;

                if (CounterType == CounterTypes.Text && CounterStringValue.Length > 0)
                    return false;
                else
                    if (CounterNumericValue != 0)
                        return false;

                if (SuffixVisible && !SuffixReadOnly && SuffixValue != defaultSuffix)
                    return false;

                return true;
            }
        }

        //public string Findable
        //{
        //    get { return ""; }
        //}

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

        private int AddPrefix(int newctrlpos)
        {
            int width;

            //if (PrefixReadOnly)
            //{
            //    prefixMetroLabel = new MetroLabel();
            //    prefixMetroLabel.Location = new Point(0, 0);
            //    prefixMetroLabel.Text = GetPrefixValue();
            //    width = (int)Math.Floor(graphics.MeasureString(prefixMetroLabel.Text, this.Font).Width + 4);

            //    prefixMetroLabel.Size = new Size(width, 23);
            //    prefixMetroLabel.TextAlign = ContentAlignment.MiddleCenter;

            //    this.Controls.Add(prefixMetroLabel);
            //}
            //else
            //{
            prefixMasked = new MetroTextBox();
            prefixMasked.Location = new Point(0, 0);
            //prefixMasked.Mask = PrefixMask;
            prefixMasked.Text = prefixValue;
            width = (int)Math.Floor(graphics.MeasureString(prefixMasked.Text != "" ? prefixMasked.Text : prefixMasked.Text, this.Font).Width + 4);

            prefixMasked.Size = new Size(width, 23);
            prefixMasked.Enabled = !PrefixReadOnly;
            //prefixMasked.BorderStyle = PrefixReadOnly
            //                                ? BorderStyle.None
            //                                : BorderStyle.Fixed3D;
            this.Controls.Add(prefixMasked);

            //}
            newctrlpos += width;

            if (PrefixSeparator != null && PrefixSeparator.Length > 0)
            {
                prefixSep = new MetroLabel();
                prefixSep.Location = new Point(newctrlpos, 0);
                prefixSep.Text = PrefixSeparator;
                width = (int)Math.Floor(graphics.MeasureString(prefixSep.Text, this.Font).Width + 4);
                prefixSep.Size = new Size(width, 23);
                prefixSep.TextAlign = ContentAlignment.MiddleCenter;

                this.Controls.Add(prefixSep);
                newctrlpos += width;
            }
            return newctrlpos;
        }

        private int AddNumericCounter(int newctrlpos)
        {
            int width = 0;
            string text = "0123456789";
            numericCounterCtrl = new NumericTextBox();
            numericCounterCtrl.Location = new Point(newctrlpos, 0);
            numericCounterCtrl.Text = GetStartValue();
            width = (int)Math.Floor(graphics.MeasureString(text.Substring(1, CounterLen), this.Font).Width + 4);

            numericCounterCtrl.Size = new Size(width, 23);
            numericCounterCtrl.MaxDecimalPlaces = 0;
            numericCounterCtrl.MaxWholeDigits = CounterLen;
            numericCounterCtrl.AllowNegative = false;
            numericCounterCtrl.TextChanged += new EventHandler(numericCounterCtrl_TextChanged);

            this.Controls.Add(numericCounterCtrl);
            newctrlpos += width;
            return newctrlpos;
        }

        private void numericCounterCtrl_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }

        private void FiscalNoControl_counterChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        private int AddTextCounter(int newctrlpos)
        {
            string text = "MMMMMMMMMMMMMMMMMMMM";
            textCounterCtrl = new MetroTextBox();
            textCounterCtrl.Location = new Point(newctrlpos, 0);
            textCounterCtrl.Text = GetStartValue();
            textCounterCtrl.CharacterCasing = CharacterCasing.Upper;
            int width = (int)Math.Floor(graphics.MeasureString(text.Substring(1, CounterLen), this.Font).Width + 4);

            textCounterCtrl.Size = new Size(width, 23);
            textCounterCtrl.MaxLength = CounterLen;
            textCounterCtrl.CharacterCasing = CharacterCasing.Upper;

            this.Controls.Add(textCounterCtrl);
            newctrlpos += width;
            return newctrlpos;
        }

        private int AddSuffix(int newctrlpos)
        {
            int width;
            if (SuffixSeparator != null && SuffixSeparator.Length > 0)
            {
                suffixSep = new MetroLabel();
                suffixSep.Location = new Point(newctrlpos, 0);
                suffixSep.Text = SuffixSeparator;
                width = (int)Math.Floor(graphics.MeasureString(suffixSep.Text, this.Font).Width + 4);
                suffixSep.Size = new Size(width, 23);
                suffixSep.TextAlign = ContentAlignment.MiddleCenter;

                this.Controls.Add(suffixSep);

                newctrlpos += width;
            }

            if (SuffixMask != null || GetSuffixValue() != "")
            {
                //if (SuffixReadOnly)
                //{
                //    suffixMetroLabel = new MetroLabel();
                //    suffixMetroLabel.Location = new Point(newctrlpos, 0);
                //    suffixMetroLabel.Text = GetSuffixValue();
                //    width = (int)Math.Floor(graphics.MeasureString(suffixMetroLabel.Text, this.Font).Width + 4);

                //    suffixMetroLabel.Size = new Size(width, 23);
                //    suffixMetroLabel.TextAlign = ContentAlignment.MiddleCenter;

                //    this.Controls.Add(suffixMetroLabel);
                //}
                //else
                //{
                suffixMasked = new MetroTextBox();
                suffixMasked.Location = new Point(newctrlpos, 0);
                //suffixMasked.Mask = PrefixMask;
                suffixMasked.Text = suffixValue;
                width = (int)Math.Floor(graphics.MeasureString(suffixMasked.Text != "" ? suffixMasked.Text : suffixMasked.Text, this.Font).Width + 4);

                suffixMasked.Size = new Size(width, 23);
                suffixMasked.Enabled = !SuffixReadOnly;
                //suffixMasked.BorderStyle = SuffixReadOnly
                //                                ? BorderStyle.None
                //                                : BorderStyle.Fixed3D;
                this.Controls.Add(suffixMasked);

                //}
                newctrlpos += width;
            }
            return newctrlpos;
        }

        private int AddButton(int newctrlpos)
        {
            int width;
            newctrlpos += 4;
            btnEdit = new MetroButton();
            if (designText != null)
                newctrlpos += designText.Width;

            btnEdit.Location = new Point(newctrlpos, 0);
            btnEdit.BackgroundImage = Properties.Resources.Lightning;
            btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btnEdit.Size = new System.Drawing.Size(22, 21);
            btnEdit.Click += new EventHandler(btnLighting_Click);
            btnEdit.FlatStyle = FlatStyle.Popup;
            width = btnEdit.Width;
            this.Controls.Add(btnEdit);

            //}
            newctrlpos += width;

            return newctrlpos;
        }

        private void btnLighting_Click(object sender, EventArgs e)
        {
            IsAutomatic = !IsAutomatic;
            if (IsAutomatic)
                UpdateValue(DBOperation.Get);

            ChangeButtonImage();
            if (IsAutomatic)
                GetNewValue();
        }

        private void ChangeButtonImage()
        {
            if (btnEdit != null)
                btnEdit.Image = IsAutomatic
                                        ? Properties.Resources.Lightning
                                        : Properties.Resources.Pen;

            if (prefixMasked != null && this.Controls.Contains(prefixMasked))
                prefixMasked.Enabled = !IsAutomatic;
            if (suffixMasked != null && this.Controls.Contains(suffixMasked))
                suffixMasked.Enabled = !IsAutomatic;
            if (numericCounterCtrl != null && this.Controls.Contains(numericCounterCtrl))
                numericCounterCtrl.Enabled = !IsAutomatic;
            if (textCounterCtrl != null && this.Controls.Contains(textCounterCtrl))
                textCounterCtrl.Enabled = !IsAutomatic;
        }

        private string GetStartValue()
        {
            return (CounterType == CounterTypes.Text)
                        ? string.Empty
                        : CounterNumericValue.ToString();
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
                default:
                    return defaultPrefix;
            }
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
                default:
                    return defaultSuffix;
            }
        }

        public string EmptyCode
        {
            get
            {
                string bufString = CounterStringValue;
                int bufInt = CounterNumericValue;
                string bufPref = prefixValue;
                string bufSuff = SuffixValue;

                SuffixValue = defaultSuffix;
                prefixValue = defaultPrefix;

                CounterStringValue = "";
                CounterNumericValue = 0;

                string val = ToString();

                CounterStringValue = bufString;
                CounterNumericValue = bufInt;
                prefixValue = bufPref;
                SuffixValue = bufSuff;

                return val;
            }
        }

        #region Findable

        public void Clean()
        {
            Text = EmptyCode;
        }

        #endregion
    }
}