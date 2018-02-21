using System;
using System.ComponentModel;
using System.Windows.Forms;
using ERPFramework.CounterManager;
using ERPFramework.Data;
using ERPFramework.Forms;

namespace ERPFramework.Controls
{
    #region RadarCounterDataGridViewControl

    internal class RadarCounterDataGridViewControl : MetroCounterTextBox, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;
        private bool valueChanged;
        private int rowIndex;
        public bool trimResult;
        public IColumn DescriptionColumn;
        public object[] args;

        public RadarCounterDataGridViewControl()
        {
            //this.silentmode = true;
            //this.Format = DateTimePickerFormat.Short;
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
            NotifyDataGridViewOfValueChange();
        }

        protected override void OnValidated(EventArgs e)
        {
            if (dataGridView != null && dataGridView is ExtendedDataGridView)
            {
                ExtendedDataGridView edgw = dataGridView as ExtendedDataGridView;
                if (edgw.CurrentCell != null)
                {
                    edgw.ControlValidate(edgw.CurrentCell.OwningColumn.Name, this.Description);
                    if (DescriptionColumn != null &&
                        edgw.CurrentRow.Cells[DescriptionColumn.Name] != null)
                    {
                        edgw.CurrentRow.Cells[DescriptionColumn.Name].Value = this.Description;
                    }
                }
            }
            base.OnValidated(e);
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
                return (trimResult) ? this.Text.Trim() : this.Text;
            }
        }

        ////   Get the value of the editing control for formatting.
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            string result = (trimResult) ? Text.Trim() : Text;
            return result;
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
                //SelectAll();
                this.SelectionStart = this.ToString().Length;
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
            // RadarCounterDataGridViewControl
            //
            this.Name = "RadarCounterDataGridViewControl";
            this.Size = new System.Drawing.Size(102, 20);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

    #endregion

    #region RadarCounterDataGridViewCell

    public class RadarCounterDataGridViewCell : DataGridViewTextBoxCell
    {
        object[] args;
        public CharacterCasing CharacterCasing = CharacterCasing.Normal;
        public System.Drawing.Image image;
        public Type iradarForm;
        public bool TrimResult;
        public Control descriptionColumn;
        public bool mustExistData;
        public bool enableAddOnFly;
        public bool onlyDigits;

        public RadarCounterDataGridViewCell()
            : base()
        {
            // Use the short date format.
            //this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            RadarCounterDataGridViewControl ctl =
                DataGridView.EditingControl as RadarCounterDataGridViewControl;

            //ctl.Text = (this.Value is System.DBNull) ? string.Empty : (string)this.Value;
            ctl.Text = (initialFormattedValue is System.DBNull) ? string.Empty : (string)initialFormattedValue;
            DataGridViewColumn dgvc = this.OwningColumn;
            if (dgvc is RadarCounterDataGridViewColumn)
            {
                RadarCounterDataGridViewColumn rtvc = dgvc as RadarCounterDataGridViewColumn;
                ctl.AttachRadarType = rtvc.iradarform;
                ctl.DescriptionColumn = rtvc.DescriptionColumn;
                ctl.CharacterCasing = rtvc.CharacterCasing;
                ctl.trimResult = rtvc.TrimResult;
                ctl.OnlyDigits = rtvc.OnlyDigits;
                ctl.MustExistData = rtvc.MustExistData;
                ctl.AttachCounterType(rtvc.CounterType, GlobalInfo.CurrentDate, null);

                ctl.args = new object[rtvc.args.Length];
                for (int t = 0; t < rtvc.args.Length; t++)
                    ctl.args[t] = rtvc.args[t];
            }

            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(RadarCounterDataGridViewControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that Cell contains.
                return typeof(string);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return string.Empty;
            }
        }

        public override object Clone()
        {
            RadarCounterDataGridViewCell ctl = (RadarCounterDataGridViewCell)base.Clone();
            ctl.iradarForm = iradarForm;
            ctl.descriptionColumn = descriptionColumn;
            ctl.CharacterCasing = CharacterCasing;
            ctl.TrimResult = TrimResult;
            ctl.image = image;
            ctl.mustExistData = mustExistData;
            ctl.enableAddOnFly = enableAddOnFly;
            ctl.onlyDigits = onlyDigits;

            if (args != null)
            {
                this.args = new object[args.Length];
                for (int t = 0; t < args.Length; t++)
                    this.args[t] = args[t];
            }

            return ctl;
        }
    }

    #endregion

    #region RadarCounterDataGridViewColumn

    public class RadarCounterDataGridViewColumn : DataGridViewColumn, IClickable
    {
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing = CharacterCasing.Normal;

        [Browsable(false)]
        public object[] args;

        [Browsable(false)]
        public int CounterType;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Type iradarform;

        private bool trim;
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool TrimResult
        {
            get { return trim; }
            set { trim = value; }
        }

        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DisplayName("Description Column")]
        [DefaultValue(null)]
        public IColumn DescriptionColumn { get; set; } = null;

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        public bool MustExistData { get; set; } = false;
        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        public bool EnableAddOnFly { get; set; } = false;

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        [Description("Accept only numerics")]
        [DefaultValue(false)]
        public bool OnlyDigits { get; set; } = false;

        [Browsable(false)]
        public void AttachRadar<T>()
        {
            iradarform = typeof(T);
        }

        [Browsable(false)]
        public void AttachRadar<T>(params object[] args)
        {
            iradarform = typeof(T);
            this.args = new object[args.Length];
            for (int t = 0; t < args.Length; t++)
                this.args[t] = args[t];
        }

        [Browsable(false)]
        public bool HasRadar { get { return iradarform != null; } }

        public bool OpenDocument(string val = "")
        {
            if (iradarform == null || val.IsEmpty())
                return false;

            Application.UseWaitCursor = true;
            Application.DoEvents();
            IRadarForm radarForm = Activator.CreateInstance(iradarform, args) as IRadarForm;
            IRadarParameters iParam = radarForm.GetRadarParameters(val);
            radarForm.OpenBrowse(iParam);

            Application.UseWaitCursor = false;

            return false;
        }

        public RadarCounterDataGridViewColumn()
            : base(new RadarCounterDataGridViewCell())
        {
        }

        public void AttachCounterType(int key, DateTime curDate, IDocumentBase documentBase)
        {
            CounterType = key;
        }

        public override object Clone()
        {
            RadarCounterDataGridViewColumn column = (RadarCounterDataGridViewColumn)base.Clone();
            column.iradarform = iradarform;
            column.DescriptionColumn = DescriptionColumn;
            column.CharacterCasing = CharacterCasing;
            column.TrimResult = TrimResult;
            column.OnlyDigits = OnlyDigits;
            if (args != null)
            {
                column.args = new object[args.Length];
                for (int t = 0; t < args.Length; t++)
                    column.args[t] = args[t];
            }

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
                    !value.GetType().IsAssignableFrom(typeof(RadarCounterDataGridViewCell)))
                {
                    throw new InvalidCastException("Must be a RadarCounterDataGridViewCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    #endregion
}