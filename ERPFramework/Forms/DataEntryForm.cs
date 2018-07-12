using ERPFramework.Controls;
using ERPFramework.CounterManager;
using ERPFramework.Data;
using MetroFramework.Controls;
using MetroFramework.Extender;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ERPFramework.Forms.DocumentForm;

namespace ERPFramework.Forms
{
    public partial class DataEntryForm : MetroForm , IDataEntryBase
    {
        public DataModelManager DataModelManager { get; }
        private readonly ControlBinder controlBinder;

        public DataEntryForm()
        {
            InitializeComponent();

            DataModelManager = new DataModelManager(this);
            controlBinder = new ControlBinder();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            OnInitializeData();
            OnBindData();

            PanelsInEdit = true;
        }

        #region Virtual Methods
        public virtual void OnBindData() { }
        protected virtual void OnInitializeData() { }
        #endregion

        #region Binding Controls
        public Binding BindControl(Control control, IColumn column, string property = "", NullValue nullValue = NullValue.SetNull)
        {
            if (property.IsEmpty())
                property = GetProperty(control);

            if (column.Len > 0)
            {
                if (control is MetroTextBox)
                    ((MetroTextBox)control).MaxLength = column.Len;
                else if (control is TextBox)
                    ((TextBox)control).MaxLength = column.Len;
            }

            controlBinder.Bind(control, column, property);
            if (DataModelManager.GetDataColumn(column).DefaultValue is System.DBNull && nullValue == NullValue.SetNull)
                DataModelManager.GetDataColumn(column).DefaultValue = column.DefaultValue;
            return control.DataBindings.Add(property, DataModelManager.BindingMaster, column.Name);
        }

        public void BindCounter(ICounterManager bindable)
        {
            // @@TODO
            //if (DataModelManager != null)
            //    DataModelManager.BindCounter(ref bindable);
        }

        public Binding BindObject(IBindableComponent bindableObj, IColumn column)
        {
            return BindObject(bindableObj, column, NullValue.SetNull);
        }

        public Binding BindObject(IBindableComponent bindableObj, IColumn column, NullValue nullValue)
        {
            if (DataModelManager.GetDataColumn(column).DefaultValue is System.DBNull && nullValue == NullValue.SetNull)
                DataModelManager.GetDataColumn(column).DefaultValue = column.DefaultValue;
            return bindableObj.DataBindings.Add("Value", DataModelManager.BindingMaster, column.Name);
        }

        public Binding BindLocal(Control control, string property, string datamember)
        {
            controlBinder.Bind(control);
            return control.DataBindings.Add(property, this, datamember);
        }

        public Binding BindControl(Control control)
        {
            controlBinder.Bind(control);
            if (control.GetType() == typeof(ExtendedDataGridView))
            {
                ((ExtendedDataGridView)control).DataEntryBase = this;
                ((ExtendedDataGridView)control).LoadSetting();
            }

            return null;
        }

        public void BindColumn(DataGridViewColumn gridcol, IColumn column)
        {
            gridcol.Name = column.Name;
            gridcol.DataPropertyName = column.Name;
            if (gridcol.CellType == typeof(DataGridViewTextBoxCell) && column.Len > 0)
                ((DataGridViewTextBoxColumn)gridcol).MaxInputLength = column.Len;

            if (DataModelManager.GetDataColumn(column) != null && DataModelManager.GetDataColumn(column).DefaultValue is System.DBNull)
                DataModelManager.GetDataColumn(column).DefaultValue = column.DefaultValue;
        }

        private static string GetProperty(Control control)
        {
            var propertyConv = new Dictionary<Type, string>  {
                { typeof(DateTextBox), "Today"},
                { typeof(MetroTextBoxNumeric), "Double"},
                { typeof(TextBox), "Text"},
                { typeof(MetroTextBox), "Text"},
                { typeof(MetroCounterTextBox), "Text"},
                { typeof(MetroIntelliTextBox), "Text"},
                { typeof(MetroMaskedTextbox), "Text"},
                { typeof(CheckBox), "Checked"},
                { typeof(RadioButton), "Checked"},
                { typeof(MetroCheckBox), "Checked"},
                { typeof(MetroRadioButton), "Checked"},
                { typeof(MetroToggle), "Checked"},
                { typeof(ComboBox), "SelectedValue"},
                { typeof(MetroComboBox), "SelectedValue"},
                { typeof(NumericUpDown), "SelectedValue"},
                { typeof(MetroNumericUpDown), "Value"},

            };

            if (propertyConv.ContainsKey(control.GetType()))
                return propertyConv[control.GetType()];

            throw new Exception($"CreateTable {control.GetType().ToString()} Tipo di control sconosciuto");
            //if (control is DateTextBox)
            //    return "Today";
            //else if (control is MetroTextBoxNumeric)
            //    return "Double";
            //else if (control is TextBox || control is MetroTextBox || control is MetroCounterTextBox || control is MetroIntelliTextBox || control is MetroMaskedTextbox)
            //    return "Text";
            //else if (control is CheckBox || control is RadioButton || control is MetroCheckBox || control is MetroRadioButton || control is MetroToggle)
            //    return "Checked";
            //else if (control is ComboBox || control is MetroComboBox)
            //    return "SelectedValue";
            //else if (control is NumericUpDown || control is MetroNumericUpDown)
            //    return "Value";
            //else
            //    throw new Exception("Unknow type");
        } 
        #endregion

        #region Panel Management

        public bool PanelsInEdit
        {
            set
            {
                btnEdit.Visible = !value;
                BottomVisible = value;
                BottomRightVisible = value;
                BottomCenterVisible = value && DocumentHasReport;
                BottomLeftVisible = false;
            }
        }

        public bool BottomVisible
        {
            set => tlpBottom.Visible = value;
        }

        public bool BottomLeftVisible
        {
            set => mfpBottomLeft.Visible = value;
        }

        public bool BottomCenterVisible
        {
            set => mfpBottomCenter.Visible = value;
        }

        public bool BottomRightVisible
        {
            set => mfpBottomRight.Visible = value;
        }

        public bool ButtonEditVisible
        {
            set => btnEdit.Visible = value;
        }
        public bool DocumentHasReport { get; private set; } = false;

        #endregion

    }
}
