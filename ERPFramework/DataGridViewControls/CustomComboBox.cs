using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ERPFramework.DataGridViewControls
{
    public class CustomComboBoxColumn : DataGridViewComboBoxColumn
    {
        public CustomComboBoxColumn()
        {
            CustomComboBoxCell cbc = new CustomComboBoxCell();
            this.CellTemplate = cbc;
        }
    }

    public class CustomComboBoxCell : DataGridViewComboBoxCell
    {
        public CustomComboBoxCell()
        : base()
        { }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            var ctl = DataGridView.EditingControl as CustomComboBoxControl;

            if (this.Value == null)
                ctl.SelectedIndex = 0;
        }

        public override Type EditType
        {
            get { return typeof(CustomComboBoxControl); }
        }

        public override Type ValueType
        {
            get { return typeof(int); }
        }

        public override object DefaultNewRowValue
        {
            get { return 1; }
        }

    }


    public class CustomComboBoxControl : ComboBox, IDataGridViewEditingControl
    {
        private int index_;
        private DataGridView dataGridView_;
        private bool valueChanged_;

        public CustomComboBoxControl() : base()
        {
            this.SelectedIndexChanged += new EventHandler(ComboBoxControl_SelectedIndexChanged);
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        public void ComboBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotifyDataGridViewOfValueChange();
        }


        protected virtual void NotifyDataGridViewOfValueChange()
        {
            this.valueChanged_ = true;
            if (this.dataGridView_ != null)
            {
                this.dataGridView_.NotifyCurrentCellDirty(true);
            }
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle) { }

        public DataGridView EditingControlDataGridView
        {
            get { return dataGridView_; }
            set { dataGridView_ = value; }
        }

        public object EditingControlFormattedValue
        {
            get { return base.SelectedValue; }
            set { base.SelectedValue = value; NotifyDataGridViewOfValueChange(); }
        }

        public int EditingControlRowIndex
        {
            get { return index_; }
            set { index_ = value; }
        }

        public bool EditingControlValueChanged
        {
            get { return valueChanged_; }
            set { valueChanged_ = value; }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if (keyData == Keys.Return)
                return true;
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    return true;
                default:
                    return false;
            }
        }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            var val = EditingControlFormattedValue;
            if (val == null)
                val = DashStyle.Solid;
            return val.ToString();
        }

        public void PrepareEditingControlForEdit(bool selectAll) { }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

    }
}
