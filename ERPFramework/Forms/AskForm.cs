using ERPFramework.Controls;
using ERPFramework.Data;
using ERPFramework.DataGridViewControls;
using ERPFramework.Preferences;
using MetroFramework.Forms;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPFramework.Forms
{
    public partial class AskForm : MetroForm, IDocumentBase
    {
        public virtual bool OnOk() { return true; }
        public virtual bool OnCancel() { return true; }

        private bool cancelVisible = true;
        [DefaultValue(true)]
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public bool CancelVisible { get { return cancelVisible; } set { mtlBack.Visible = cancelVisible = value; } }

        [Localizable(true)]
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public string Title { get; set; }
        public DBMode DocumentMode { get { return DBMode.Browse; } }
        public event EventHandler Exit;

        [Browsable(false)]
        public SqlProxyTransaction Transaction { get; set; } = null;

        SqlProxyConnection connection;
        [Browsable(false)]
        public SqlProxyConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlProxyConnection();
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                    }
                    catch (SqlException exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
                return connection;
            }
        }

        public AskForm()
        {
            InitializeComponent();

            if ((DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime))
                return;

            GlobalInfo.StyleManager.Clone(this);

            var globalPref = new PreferencesManager<GlobalPreferences>("", null).ReadPreference();
            ControlBox = globalPref.ShowControlBox;
            ShowInTaskbar = !Modal;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            OnBindData();
        }

        protected virtual void OnBindData() { }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();

            base.OnClosing(e);
        }

        private void mtlOk_Click(object sender, System.EventArgs e)
        {
            if (OnOk())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void mtlBack_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (OnCancel())
                this.Close();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            mtlOk.Left = Width - 56;
            mtlBack.Left = Width - 95;
        }

        public void CloseForm(DialogResult result)
        {
            if (Exit != null)
                Exit(this.Parent, EventArgs.Empty);

            DialogResult = result;
            this.Close();
        }

        public Binding BindControl(Control control)
        {
            if (control.GetType() == typeof(ExtendedDataGridView))
            {
                ((ExtendedDataGridView)control).DocumentForm = this;
                ((ExtendedDataGridView)control).LoadSetting();
            }

            return null;
        }

        protected static void BindColumn(DataGridViewColumn gridcol, IColumn column)
        {
            gridcol.Name = column.Name;
            gridcol.DataPropertyName = column.Name;
            gridcol.DefaultValue(column.DefaultValue);
            if (gridcol.CellType == typeof(DataGridViewTextBoxCell) && column.Len > 0)
                ((DataGridViewTextBoxColumn)gridcol).MaxInputLength = column.Len;
        }

        protected static void BindColumn<T>(DataGridViewColumn gridcol, IColumn column)
        {
            gridcol.Name = column.Name;
            gridcol.DataPropertyName = column.Name;
            gridcol.DefaultValue(column.DefaultValue);
            if (gridcol.CellType == typeof(DataGridViewTextBoxCell) && column.Len > 0)
                ((DataGridViewTextBoxColumn)gridcol).MaxInputLength = column.Len;
            if (gridcol.CellType == typeof(TrimMaskedTextBoxDataGridViewCell))
                ((TrimMaskedTextBoxDataGridViewColumn)gridcol).EnumsType = typeof(T);
        }

        protected SqlProxyTransaction StartTransaction()
        {
            Transaction = connection.BeginTransaction();
            return Transaction;
        }

        protected void Commit()
        {
            if (Transaction != null)
                Transaction.Commit();
        }

        protected void RollBack()
        {
            if (Transaction != null)
                Transaction.Rollback();
        }
    }
}
