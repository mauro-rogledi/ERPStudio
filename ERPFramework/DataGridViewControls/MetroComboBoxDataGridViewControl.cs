using System;
using System.ComponentModel;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace ERPFramework.DataGridViewControls
{
    #region MetroDataGridViewComboBoxEditingControl

    internal class MetroComboBoxControl : MetroComboBox, IDataGridViewEditingControl
    {
        private int index_;
        private DataGridView dataGridView_;
        private bool valueChanged_;

        public MetroComboBoxControl() : base()
        {
            this.SelectedIndexChanged += new EventHandler(ComboBoxControl_SelectedIndexChanged);
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
                val = default(int);
            
            return this.Text;
        }

        public void PrepareEditingControlForEdit(bool selectAll) { }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

    }

    #endregion

    #region NumericTextBoxDataGridViewCell

    public class MetroDataGridViewComboBoxCell : DataGridViewComboBoxCell
    {
        public MetroDataGridViewComboBoxCell()
        : base()
        { }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            var ctl = DataGridView.EditingControl as MetroComboBoxControl;

            if (this.Value == null)
                ctl.SelectedIndex = 0;
        }

        public override Type EditType
        {
            get { return typeof(MetroComboBoxControl); }
        }

        public override Type ValueType
        {
            get { return typeof(int); }
        }

        public override object DefaultNewRowValue
        {
            get { return 0; }
        }

    }

    #endregion

    #region MetroDataGridViewComboBoxColumn

    public class MetroDataGridViewComboBoxColumn : DataGridViewColumn
    {
        public MetroDataGridViewComboBoxColumn()
        {
            MetroDataGridViewComboBoxCell cbc = new MetroDataGridViewComboBoxCell();
            this.CellTemplate = cbc;
        }

        public override DataGridViewCell CellTemplate { get; set; }

        #region Public Methods


        [Category("Data")]
        [Description("The maximum number of digits allowed right of the decimal point.")]
        public object DataSource
        {
            get
            {
                return ((MetroDataGridViewComboBoxCell)this.CellTemplate).DataSource;
            }
            set
            {
                ((MetroDataGridViewComboBoxCell)this.CellTemplate).DataSource = value;
            }
        }

        [Category("Data")]
        [Description("Determines whether the value is allowed to be negative or not.")]
        public string DisplayMember
        {
            get
            {
                return ((MetroDataGridViewComboBoxCell)this.CellTemplate).DisplayMember;
            }
            set
            {
                ((MetroDataGridViewComboBoxCell)this.CellTemplate).DisplayMember = value;
            }
        }

        [Category("Data")]
        [Description("Determines whether the value is allowed to be negative or not.")]
        public string ValueMember
        {
            get
            {
                return ((MetroDataGridViewComboBoxCell)this.CellTemplate).ValueMember;
            }
            set
            {
                ((MetroDataGridViewComboBoxCell)this.CellTemplate).ValueMember = value;
            }
        }
        /// <include file='doc\PropertyPages.uex' path='docs/doc[@for="GeneralPropertyPage.AssemblyName"]/*' />
        [Category("Data")]
        [Description("The minimum value allowed.")]
        public DataGridViewComboBoxCell.ObjectCollection Items
        {
            get
            {
                return ((MetroDataGridViewComboBoxCell)this.CellTemplate).Items;
            }
        }

        #endregion Public Methods
    }
}

    #endregion
