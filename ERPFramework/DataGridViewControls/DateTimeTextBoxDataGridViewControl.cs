using System;
using System.Windows.Forms;
using ERPFramework.Controls;
using System.ComponentModel;

namespace ERPFramework.DataGridViewControls
{
    #region DateTimeTextBoxDataGridViewControl

    internal class DateTimeTextBoxDataGridViewControl : DateTimePicker, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;
        private bool valueChanged;
        private int rowIndex;

        public DateTimeTextBoxDataGridViewControl()
        {
            //this.Format = DateTimePickerFormat.Custom;
            //this.CustomFormat = "HH:mm:ss";
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

        protected override void OnValidated(EventArgs e)
        {
            if (dataGridView is ExtendedDataGridView)
            {
                ExtendedDataGridView edgw = dataGridView as ExtendedDataGridView;

                // edgw.ControlValidate(SL[TextBox1.Text]);
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
                return this.Text;
            }
        }

        ////   Get the value of the editing control for formatting.
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Text;
        }

        ////  Process input key and determine if the key should be used for the editing control
        ////  or allowed to be processed by the grid. Handle cursor movement keys for the MaskedTextBox
        ////  control; otherwise if the DataGridView doesn't want the input key then let the editing control handle it.
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            //switch (keyData & Keys.KeyCode)
            //{
            //    case Keys.Right:

            //        //
            //        // If the end of the selection is at the end of the string
            //        // let the DataGridView treat the key message
            //        //
            //        if (!(this.SelectionLength == 0
            //              && this.SelectionStart == this.ToString().Length))
            //        {
            //            return true;
            //        }
            //        break;

            //    case Keys.Up:
            //        if (!(this.SelectionLength == 0
            //               && this.SelectionStart == this.ToString().Length))
            //        {
            //            return false;
            //        }
            //        break;

            //    case Keys.Left:

            //        //
            //        // If the end of the selection is at the begining of the
            //        // string or if the entire text is selected send this character
            //        // to the dataGridView; else process the key event.
            //        //
            //        if (!(this.SelectionLength == 0
            //              && this.SelectionStart == 0))
            //        {
            //            return true;
            //        }
            //        break;

            //    case Keys.Home:
            //    case Keys.End:
            //        if (this.SelectionLength != this.ToString().Length)
            //        {
            //            return true;
            //        }
            //        break;

            //    case Keys.Prior:
            //    case Keys.Next:
            //        if (this.valueChanged)
            //        {
            //            return true;
            //        }
            //        break;

            //    case Keys.Delete:
            //        if (this.SelectionLength > 0 || this.SelectionStart < this.ToString().Length)
            //        {
            //            return true;
            //        }
            //        break;
            //}

            //
            // defer to the DataGridView and see if it wants it.
            //
            return !dataGridViewWantsInputKey;
        }

        ////  Prepare the editing control for edit.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            //if (selectAll)
            //{
            //    //SelectAll();
            //    this.SelectionStart = this.ToString().Length;
            //}
            //else
            //{
            //    //
            //    // Do not select all the text, but position the caret at the
            //    // end of the text.
            //    //
            //    this.SelectionStart = this.ToString().Length;
            //}
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
            // DateTimeTextBoxDataGridViewControl
            //
            this.Name = "DateTimeTextBoxDataGridViewControl";
            this.Size = new System.Drawing.Size(102, 20);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

    #endregion

    #region DateTimeTextBoxDataGridViewCell

    public class DateTimeTextBoxDataGridViewCell : DataGridViewTextBoxCell
    {
        public DateTimePickerFormat Format { get; set; }
        public string CustomFormat { get; set; }

        public DateTimeTextBoxDataGridViewCell()
            : base()
        {
            this.Style.Format = "dd/MM/yyyy HH:mm:ss";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            DateTimeTextBoxDataGridViewControl ctl =
                DataGridView.EditingControl as DateTimeTextBoxDataGridViewControl;

            ctl.Text = (initialFormattedValue is System.DBNull) ? string.Empty : (string)initialFormattedValue;
            DataGridViewColumn dgvc = this.OwningColumn;
            if (dgvc is DateTimeTextBoxDataGridViewColumn)
            {
                DateTimeTextBoxDataGridViewColumn rtvc = dgvc as DateTimeTextBoxDataGridViewColumn;
                ctl.Format = rtvc.Format;
                ctl.CustomFormat = rtvc.CustomFormat;
                this.Style.Format = rtvc.CustomFormat;
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
                return typeof(DateTimeTextBoxDataGridViewControl);
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

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            object myValue = value;
            DataGridViewColumn dgvc = this.OwningColumn;
            if (dgvc is DateTimeTextBoxDataGridViewColumn)
            {
                DateTimeTextBoxDataGridViewColumn rtvc = dgvc as DateTimeTextBoxDataGridViewColumn;
                this.Format = rtvc.Format;
                this.CustomFormat = rtvc.CustomFormat;
                this.Style.Format = rtvc.CustomFormat;
                DateTime dt = Convert.ToDateTime(value);
                if (rtvc.CustomFormat != "")
                    myValue = dt.ToString(CustomFormat);
            }
            return base.GetFormattedValue(myValue, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        public override object Clone()
        {
            DateTimeTextBoxDataGridViewCell ctl = (DateTimeTextBoxDataGridViewCell)base.Clone();

            ctl.CustomFormat = CustomFormat;
            ctl.Format = Format;
            ctl.Style.Format = CustomFormat;
            return ctl;
        }
    }

    #endregion

    #region DateTimeTextBoxDataGridViewColumn

    public class DateTimeTextBoxDataGridViewColumn : DataGridViewColumn
    {
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        [DefaultValue(DateTimePickerFormat.Custom)]
        public DateTimePickerFormat Format { get; set; } = DateTimePickerFormat.Custom;

        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public string CustomFormat { get; set; }

        public DateTimeTextBoxDataGridViewColumn()
            : base(new DateTimeTextBoxDataGridViewCell())
        {
        }

        public override object Clone()
        {
            DateTimeTextBoxDataGridViewColumn column = (DateTimeTextBoxDataGridViewColumn)base.Clone();

            column.Format = Format;
            column.CustomFormat = CustomFormat;

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
                    !value.GetType().IsAssignableFrom(typeof(DateTimeTextBoxDataGridViewCell)))
                {
                    throw new InvalidCastException("Must be a DateTimeTextBoxDataGridViewCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    #endregion
}