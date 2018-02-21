using System;
using System.ComponentModel;
using System.Windows.Forms;

using ERPFramework.Controls;

namespace ERPFramework.DataGridViewControls
{
    #region TrimMaskedTextBoxDataGridViewControl

    internal class TrimMaskedTextBoxDataGridViewControl : TrimMaskedTextBox, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;
        private bool valueChanged;
        private System.Type enumsType = typeof(System.DBNull);
        private int rowIndex;

        public TrimMaskedTextBoxDataGridViewControl()
        {
            //this.Format = DateTimePickerFormat.Short;
        }

        public System.Type EnumsType
        {
            get { return enumsType; }
            set { enumsType = value; }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            // Let the DataGridView know about the value change
           // NotifyDataGridViewOfValueChange();
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
                this.SelectionStart = 0;
                this.SelectionLength = this.EditingControlFormattedValue.ToString().Length;
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
    }

    #endregion

    #region TrimMaskedTextBoxDataGridViewCell

    public class TrimMaskedTextBoxDataGridViewCell : DataGridViewTextBoxCell
    {
        private string mask;
        private System.Type enumsType = typeof(System.DBNull);
        private bool trim;

        public TrimMaskedTextBoxDataGridViewCell()
            : base()
        {
            // Use the short date format.
            //this.Style.Format = "d";
        }

        #region Public Method

        public bool TrimResult
        {
            get { return trim; }
            set { trim = value; }
        }

        public string Mask
        {
            get { return mask; }
            set { mask = value; }
        }

        public System.Type EnumsType
        {
            get { return enumsType; }
            set { enumsType = value; }
        }

        #endregion

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            TrimMaskedTextBoxDataGridViewControl ctl =
                DataGridView.EditingControl as TrimMaskedTextBoxDataGridViewControl;

            DataGridViewColumn dgvc = this.OwningColumn;
            if (dgvc is TrimMaskedTextBoxDataGridViewColumn)
            {
                TrimMaskedTextBoxDataGridViewColumn rtvc = dgvc as TrimMaskedTextBoxDataGridViewColumn;
                ctl.Mask = rtvc.Mask;
                ctl.TrimResult = rtvc.TrimResult;
                ctl.PromptChar = ' ';
                ctl.EnumsType = rtvc.EnumsType;
            }
            ctl.Text = (initialFormattedValue is System.DBNull) ? string.Empty : (string)initialFormattedValue;

            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            object myvalue;
            TrimMaskedTextBoxDataGridViewColumn dgvc = this.OwningColumn as TrimMaskedTextBoxDataGridViewColumn;
            if (enumsType != typeof(System.DBNull) && value != null && value.GetType() != typeof(System.DBNull) &&!string.IsNullOrEmpty(value.ToString()))
            {
                string text = Enum.GetName(enumsType, int.Parse(value.ToString()));
                myvalue = text.Replace('_', ' ');
                if (dgvc.ResourceManager != null)
                    myvalue = dgvc.ResourceManager.GetString(text);
            }
            else
                myvalue = value;
            return base.GetFormattedValue(myvalue, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(TrimMaskedTextBoxDataGridViewControl);
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
            TrimMaskedTextBoxDataGridViewCell ctl = (TrimMaskedTextBoxDataGridViewCell)base.Clone();
            ctl.Mask = mask;
            ctl.EnumsType = enumsType;
            ctl.TrimResult = trim;
            return ctl;
        }
    }

    #endregion

    #region TrimMaskedTextBoxDataGridViewColumn

    public class TrimMaskedTextBoxDataGridViewColumn : DataGridViewColumn
    {
        private string mask = "";
        private System.Type enumsType = typeof(System.DBNull);
        private bool trim;
        public bool IsVirtual { get; set; }

        public TrimMaskedTextBoxDataGridViewColumn()
            : base(new TrimMaskedTextBoxDataGridViewCell())
        {
        }

        #region Public Method

        public string Mask
        {
            get { return mask; }
            set
            {
                mask = value;
                ((TrimMaskedTextBoxDataGridViewCell)this.CellTemplate).Mask = value;
            }
        }

        public bool TrimResult
        {
            get { return trim; }
            set
            {
                trim = value;
                ((TrimMaskedTextBoxDataGridViewCell)this.CellTemplate).TrimResult = value;
            }
        }

        public System.Type EnumsType
        {
            get { return enumsType; }
            set
            {
                enumsType = value;
                ((TrimMaskedTextBoxDataGridViewCell)this.CellTemplate).EnumsType = value;
            }
        }

        public System.Resources.ResourceManager ResourceManager { get; set; }

        #endregion

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
                    !value.GetType().IsAssignableFrom(typeof(TrimMaskedTextBoxDataGridViewCell)))
                {
                    throw new InvalidCastException("Must be a TextBoxDataGridViewCell");
                }
                base.CellTemplate = value;
            }
        }

        public override object Clone()
        {
            TrimMaskedTextBoxDataGridViewColumn column = (TrimMaskedTextBoxDataGridViewColumn)base.Clone();
            column.Mask = mask;
            column.EnumsType = enumsType;
            column.TrimResult = trim;

            return column;
        }
    }

    #endregion
}