using ERPFramework.CounterManager;
using ERPFramework.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    #region RadarCodesDataGridViewControl

    public class RadarCodesDataGridViewControl : RadarCodesCtrl, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;
        private bool valueChanged;
        private int rowIndex;
        public IColumn DescriptionColumn;

        public RadarCodesDataGridViewControl()
        {
            this.ShowHeader = false;
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
            if (dataGridView != null && dataGridView is ExtendedDataGridView)
            {
                ExtendedDataGridView edgw = dataGridView as ExtendedDataGridView;
                edgw.ControlValidate(edgw.CurrentCell.OwningColumn.Name, this.Description);

                //if (edgw == null)
                //    MessageBox.Show("edgw");
                //if (DescriptionColumn == null)
                //    MessageBox.Show("DescriptionColumn");
                //if (edgw.CurrentRow == null)
                //    MessageBox.Show("edgw.CurrentRow");
                //if (edgw.CurrentRow.Cells == null)
                //    MessageBox.Show("edgw.CurrentRow.Cells");
                //if (edgw.CurrentRow.Cells[DescriptionColumn.Name] == null)
                //    MessageBox.Show("edgw.CurrentRow.Cells[DescriptionColumn.Name]");
                //if (edgw.CurrentRow.Cells[DescriptionColumn.Name].Value == null)
                //    MessageBox.Show("edgw.CurrentRow.Cells[DescriptionColumn.Name].Value");

                if (DescriptionColumn != null &&
                    edgw.CurrentRow.Cells[DescriptionColumn.Name] != null &&
                    edgw.CurrentRow.Cells[DescriptionColumn.Name].Value != null &&
                    edgw.CurrentRow.Cells[DescriptionColumn.Name].Value.ToString() != this.Description)
                {
                    edgw.CurrentRow.Cells[DescriptionColumn.Name].Value = this.Description;
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
                return this.Text;
            }
        }

        ////   Get the value of the editing control for formatting.
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            string result = Text;
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
            // RadarCodesDataGridViewControl
            //
            this.Name = "RadarCodesDataGridViewControl";
            this.Size = new System.Drawing.Size(102, 20);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

    #endregion

    #region RadarCodesDataGridViewCell

    public class RadarCodesDataGridViewCell : DataGridViewTextBoxCell
    {
        private Dictionary<string, string> SL = new Dictionary<string, string>();
        public IColumn DescriptionColumn;
        public RadarForm RadarForm;
        public CharacterCasing CharacterCasing = CharacterCasing.Normal;
        public bool TrimResult;
        public string CodeType;

        public RadarCodesDataGridViewCell()
            : base()
        {
            // Use the short date format.
            //this.Style.Format = "d";
        }

        public void AddElements(Dictionary<string, string> sl)
        {
            foreach (KeyValuePair<string, string> kvp in sl)
                SL.Add(kvp.Key, kvp.Value);
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            RadarCodesDataGridViewControl ctl =
                DataGridView.EditingControl as RadarCodesDataGridViewControl;

            DataGridViewColumn dgvc = this.OwningColumn;
            if (dgvc is RadarCodesDataGridViewColumn)
            {
                RadarCodesDataGridViewColumn rtvc = dgvc as RadarCodesDataGridViewColumn;
                ctl.ClearElement();
                ctl.AddElements(rtvc.SL);
                ctl.RadarForm = rtvc.RadarForm;
                ctl.DescriptionColumn = rtvc.DescriptionColumn;
                ctl.AttachCodeType(rtvc.CodeType);
                ctl.CodeType = rtvc.CodeType;

                //ctl.CharacterCasing = rtvc.CharacterCasing;
                //ctl.TrimResult = rtvc.TrimResult;
            }
            ctl.Text = (initialFormattedValue is System.DBNull) ? string.Empty : (string)initialFormattedValue;

            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(RadarCodesDataGridViewControl);
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
            RadarCodesDataGridViewCell ctl = (RadarCodesDataGridViewCell)base.Clone();
            ctl.AddElements(SL);
            ctl.RadarForm = this.RadarForm;
            ctl.DescriptionColumn = DescriptionColumn;
            ctl.CharacterCasing = CharacterCasing;
            ctl.TrimResult = TrimResult;
            ctl.CodeType = CodeType;
            return ctl;
        }
    }

    #endregion

    #region RadarCodesDataGridViewColumn

    public class RadarCodesDataGridViewColumn : DataGridViewColumn
    {
        public Dictionary<string, string> SL = new Dictionary<string, string>();
        public IColumn DescriptionColumn;
        public CharacterCasing CharacterCasing = CharacterCasing.Normal;
        public RadarForm RadarForm;

        public string CodeType { set; get; }

        private bool trim;

        [Category("Behavior")]
        public bool TrimResult
        {
            get { return trim; }
            set { trim = value; }
        }

        public RadarCodesDataGridViewColumn()
            : base(new RadarCodesDataGridViewCell())
        {
        }

        public void AttachCodeType(string codeType)
        {
            CodeType = codeType;
        }

        public override object Clone()
        {
            RadarCodesDataGridViewColumn column = (RadarCodesDataGridViewColumn)base.Clone();
            column.AddElements(SL);
            column.RadarForm = RadarForm;
            column.DescriptionColumn = DescriptionColumn;
            column.CharacterCasing = CharacterCasing;
            column.TrimResult = TrimResult;
            column.CodeType = CodeType;
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
                    !value.GetType().IsAssignableFrom(typeof(RadarCodesDataGridViewCell)))
                {
                    throw new InvalidCastException("Must be a RadarCodesDataGridViewCell");
                }
                base.CellTemplate = value;
            }
        }

        public void Clear()
        {
            SL.Clear();
        }

        public void AddElements(Dictionary<string, string> sl)
        {
            foreach (KeyValuePair<string, string> kvp in sl)
                SL.Add(kvp.Key, kvp.Value);
        }

        public void AddElement(string key, string value)
        {
            SL.Add(key, value);
        }

        public string LookUp(string key)
        {
            return SL.ContainsKey(key)
                        ? SL[key]
                        : "";
        }
    }

    #endregion
}