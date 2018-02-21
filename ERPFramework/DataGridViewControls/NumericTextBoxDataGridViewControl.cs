using System;
using System.ComponentModel;
using System.Windows.Forms;
using MetroFramework.Extender;

namespace ERPFramework.DataGridViewControls
{
    #region NumericTextBoxDataGridViewControl

    internal class NumericTextBoxDataGridViewControl : MetroTextBoxNumeric, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;
        private bool valueChanged;
        private int rowIndex;

        public NumericTextBoxDataGridViewControl()
        {
            UseStyleColors = true;
            UseCustomForeColor = true;
            Theme = GlobalInfo.StyleManager.Theme;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            // Let the DataGridView know about the value change
            //NotifyDataGridViewOfValueChange();
        }

        //  Notify DataGridView that the value has changed.
        protected virtual void NotifyDataGridViewOfValueChange()
        {
            this.valueChanged = true;
            if (this.dataGridView != null)
            {
                this.dataGridView.NotifyCurrentCellDirty(true);
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            // Let the DataGridView know about the value change
            NotifyDataGridViewOfValueChange();
        }

        #region IDataGridViewEditingControl Members

        //  Indicates the cursor that should be shown when the user hovers their
        //  mouse over this cell when the editing control is shown.
        public Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.Cross;
            }
        }

        ////  Returns or sets the parent DataGridView.
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return this.dataGridView;
            }

            set
            {
                this.dataGridView = value;
            }
        }

        ////  Sets/Gets the formatted value contents of this cell.
        public object EditingControlFormattedValue
        {
            set
            {
                this.Text = value.ToString();
                NotifyDataGridViewOfValueChange();
            }
            get
            {
                //return this.NumericText;
                return this.Text;
            }
        }

        ////   Get the value of the editing control for formatting.
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.NumericText;
            //return this.Text;
        }

        ////  Process input key and determine if the key should be used for the editing control
        ////  or allowed to be processed by the grid. Handle cursor movement keys for the MaskedTextBox
        ////  control; otherwise if the DataGridView doesn't want the input key then let the editing control handle it.
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:

                    //
                    // If the end of the selection is at the end of the string
                    // let the DataGridView treat the key message
                    //
                    if (!(this.SelectionLength == 0
                          && this.SelectionStart == this.ToString().Length))
                    {
                        return true;
                    }
                    break;

                case Keys.Up:
                    if (!(this.SelectionLength == 0
                           && this.SelectionStart == this.ToString().Length))
                    {
                        return false;
                    }
                    break;

                case Keys.Left:

                    //
                    // If the end of the selection is at the begining of the
                    // string or if the entire text is selected send this character
                    // to the dataGridView; else process the key event.
                    //
                    if (!(this.SelectionLength == 0
                          && this.SelectionStart == 0))
                    {
                        return true;
                    }
                    break;

                case Keys.Home:
                case Keys.End:
                    if (this.SelectionLength != this.ToString().Length)
                    {
                        return true;
                    }
                    break;

                case Keys.Prior:
                case Keys.Next:
                    if (this.valueChanged)
                    {
                        return true;
                    }
                    break;

                case Keys.Delete:
                    if (this.SelectionLength > 0 || this.SelectionStart < this.ToString().Length)
                    {
                        return true;
                    }
                    break;
            }

            //
            // defer to the DataGridView and see if it wants it.
            //
            return !dataGridViewWantsInputKey;
        }

        ////  Prepare the editing control for edit.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                SelectAll();

                //this.SelectionStart = this.ToString().Length;
            }
            else
            {
                //
                // Do not select all the text, but position the caret at the
                // end of the text.
                //
                this.SelectionStart = this.ToString().Length;
            }
        }

        ////  Indicates whether or not the parent DataGridView control should
        ////  reposition the editing control every time value change is indicated.
        ////  There is no need to do this for the MaskedTextBox.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return true;
            }
        }

        ////  Indicates the row index of this cell.  This is often -1 for the
        ////  template cell, but for other cells, might actually have a value
        ////  greater than or equal to zero.
        public int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }

            set
            {
                this.rowIndex = value;
            }
        }

        ////  Make the MaskedTextBox control match the style and colors of
        ////  the host DataGridView control and other editing controls
        ////  before showing the editing control.
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;

            //this.TextAlign = translateAlignment(dataGridViewCellStyle.Alignment);
        }

        ////  Gets or sets our flag indicating whether the value has changed.
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }

            set
            {
                this.valueChanged = value;
            }
        }

        #endregion // IDataGridViewEditingControl.

        private void InitializeComponent()
        {
            this.SuspendLayout();

            //
            // NumericTextBoxDataGridViewControl
            //
            this.Name = "NumericTextBoxDataGridViewControl";
            this.Size = new System.Drawing.Size(102, 20);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

    #endregion

    #region NumericTextBoxDataGridViewCell

    public class NumericTextBoxDataGridViewCell : DataGridViewTextBoxCell
    {
        private int _MaxWholeDigits;
        private int _MaxDecimalPlaces;
        private bool _AllowNegative;
        private bool _AllowGroupSeparator;
        private double _RangeMax;
        private double _RangeMin;
        private string _Prefix;
        private bool _ShowPrefix;
        private NumericFormatHelper numFormat = new NumericFormatHelper();

        public NumericTextBoxDataGridViewCell()
            : base()
        {
            this.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        #region Public Methods

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("The maximum number of digits allowed left of the decimal point.")]
        public int MaxWholeDigits
        {
            get
            {
                return _MaxWholeDigits;
            }
            set
            {
                _MaxWholeDigits = value;
            }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("The maximum number of digits allowed right of the decimal point.")]
        public int MaxDecimalPlaces
        {
            get
            {
                return _MaxDecimalPlaces;
            }
            set
            {
                _MaxDecimalPlaces = value;
            }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("Determines whether the value is allowed to be negative or not.")]
        public bool AllowNegative
        {
            get
            {
                return _AllowNegative;
            }
            set
            {
                _AllowNegative = value;
            }
        }


        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("Determines whether the value is allowed to be negative or not.")]
        public bool AllowGroupSeparator
        {
            get
            {
                return _AllowGroupSeparator;
            }
            set
            {
                _AllowGroupSeparator = value;
            }
        }

        [Category("Behavior")]
        [Description("The text to automatically insert in front of the number, such as a currency symbol.")]
        public String Prefix
        {
            get
            {
                return _Prefix;
            }
            set
            {
                _Prefix = value;
            }
        }

        /// <summary>
        ///   Gets or sets if show the prefix simbol </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.AllowNegative">NumericBehavior.AllowNegative</see>. </remarks>
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("Determines whether the value is allowed to be negative or not.")]
        [DefaultValue(false)]
        public bool ShowPrefix
        {
            get { return _ShowPrefix; }
            set { _ShowPrefix = value; }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("The minimum value allowed.")]
        public double RangeMin
        {
            get
            {
                return _RangeMin;
            }
            set
            {
                _RangeMin = value;
            }
        }

        [Category("Behavior")]
        [Description("The maximum value allowed.")]
        public double RangeMax
        {
            get
            {
                return _RangeMax;
            }
            set
            {
                _RangeMax = value;
            }
        }

        #endregion Public Methods

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.

            NumericTextBoxDataGridViewControl ctl =
                DataGridView.EditingControl as NumericTextBoxDataGridViewControl;
            DataGridViewColumn dgvc = this.OwningColumn;
            if (dgvc is NumericTextBoxDataGridViewColumn)
            {
                NumericTextBoxDataGridViewColumn rtvc = dgvc as NumericTextBoxDataGridViewColumn;
                ctl.MaxWholeDigits = rtvc.MaxWholeDigits;
                ctl.MaxDecimalPlaces = rtvc.MaxDecimalPlaces;
                ctl.AllowNegative = rtvc.AllowNegative;
                ctl.AllowGroupSeparator = rtvc.AllowGroupSeparator;
                ctl.RangeMax = rtvc.RangeMax;
                ctl.RangeMin = rtvc.RangeMin;
                ctl.Prefix = rtvc.Prefix;
                ctl.ShowPrefix = rtvc.ShowPrefix;
            }
            ctl.Text = initialFormattedValue as string;

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            object myvalue = value;
            double result;
            if (value != null && Double.TryParse(value.ToString(), out result))
            {
                numFormat.MaxWholeDigits = MaxWholeDigits;
                numFormat.AllowGroupSeparator = AllowGroupSeparator;
                numFormat.Prefix = Prefix != null ? Prefix : "";
                numFormat.ShowPrefix = ShowPrefix;
                myvalue = numFormat.GetFormatText(value.ToString());
            }
            return base.GetFormattedValue(myvalue, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(NumericTextBoxDataGridViewControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that Cell contains.
                return typeof(double);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return "";
            }
        }

        public override object Clone()
        {
            NumericTextBoxDataGridViewCell ctl = (NumericTextBoxDataGridViewCell)base.Clone();
            ctl._MaxWholeDigits = _MaxWholeDigits;
            ctl._MaxDecimalPlaces = _MaxDecimalPlaces;
            ctl._AllowNegative = _AllowNegative;
            ctl._AllowGroupSeparator = _AllowGroupSeparator;
            ctl._RangeMax = _RangeMax;
            ctl._RangeMin = _RangeMin;
            ctl._Prefix = _Prefix;
            ctl._ShowPrefix = _ShowPrefix;
            return ctl;
        }
    }

    #endregion

    #region NumericTextBoxDataGridViewColumn

    public class NumericTextBoxDataGridViewColumn : DataGridViewColumn
    {
        private int _MaxWholeDigits;
        private int _MaxDecimalPlaces = 4;
        private bool _AllowNegative = true;
        private bool _AllowGroupSeparator = true;
        private double _RangeMax = double.MaxValue;
        private double _RangeMin = double.MinValue;
        private string _Prefix = string.Empty;
        private bool _ShowPrefix;

        public NumericTextBoxDataGridViewColumn()
            : base(new NumericTextBoxDataGridViewCell())
        {
        }

        #region Public Methods

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("The maximum number of digits allowed left of the decimal point.")]
        public int MaxWholeDigits
        {
            get
            {
                return _MaxWholeDigits;
            }
            set
            {
                _MaxWholeDigits = value;
                ((NumericTextBoxDataGridViewCell)this.CellTemplate).MaxWholeDigits = value;
            }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("The maximum number of digits allowed right of the decimal point.")]
        public int MaxDecimalPlaces
        {
            get
            {
                return _MaxDecimalPlaces;
            }
            set
            {
                _MaxDecimalPlaces = value;
                ((NumericTextBoxDataGridViewCell)this.CellTemplate).MaxDecimalPlaces = value;
            }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("Determines whether the value is allowed to be negative or not.")]
        public bool AllowNegative
        {
            get
            {
                return _AllowNegative;
            }
            set
            {
                _AllowNegative = value;
                ((NumericTextBoxDataGridViewCell)this.CellTemplate).AllowNegative = value;
            }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("Determines whether the value is allowed to be negative or not.")]
        public bool AllowGroupSeparator
        {
            get
            {
                return _AllowGroupSeparator;
            }
            set
            {
                _AllowGroupSeparator = value;
                ((NumericTextBoxDataGridViewCell)this.CellTemplate).AllowGroupSeparator = value;
            }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Localizable(true)]
        [Description("The text to automatically insert in front of the number, such as a currency symbol.")]
        public String Prefix
        {
            get
            {
                return _Prefix;
            }
            set
            {
                _Prefix = value;
                ((NumericTextBoxDataGridViewCell)this.CellTemplate).Prefix = value;
            }
        }

        /// <summary>
        ///   Gets or sets if show the prefix simbol </summary>
        /// <remarks>
        ///   This property delegates to <see cref="NumericBehavior.AllowNegative">NumericBehavior.AllowNegative</see>. </remarks>
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("Determines whether the value is allowed to be negative or not.")]
        [DefaultValue(false)]
        public bool ShowPrefix
        {
            get { return _ShowPrefix; }
            set { _ShowPrefix = value;}
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("The minimum value allowed.")]
        public double RangeMin
        {
            get
            {
                return _RangeMin;
            }
            set
            {
                _RangeMin = value;
                ((NumericTextBoxDataGridViewCell)this.CellTemplate).RangeMin = value;
            }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("The maximum value allowed.")]
        public double RangeMax
        {
            get
            {
                return _RangeMax;
            }
            set
            {
                _RangeMax = value;
                ((NumericTextBoxDataGridViewCell)this.CellTemplate).RangeMax = value;
            }
        }

        #endregion Public Methods

        public override object Clone()
        {
            NumericTextBoxDataGridViewColumn column = (NumericTextBoxDataGridViewColumn)base.Clone();

            column._MaxWholeDigits = _MaxWholeDigits;
            column._MaxDecimalPlaces = _MaxDecimalPlaces;
            column._AllowNegative = _AllowNegative;
            column._AllowGroupSeparator = _AllowGroupSeparator;
            column._RangeMax = _RangeMax;
            column._RangeMin = _RangeMin;
            column._Prefix = _Prefix;
            column._ShowPrefix = ShowPrefix;
            return column;
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(NumericTextBoxDataGridViewCell)))
                {
                    throw new InvalidCastException("Must be a NumericTextBoxDataGridViewCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    #endregion
}