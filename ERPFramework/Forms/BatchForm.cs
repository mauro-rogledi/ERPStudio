#region Using directives

using System;
using System.Windows.Forms;

#endregion

using ERPFramework.Data;
using MetroFramework.Controls;
using System.ComponentModel;
using MetroFramework;
using MetroFramework.Extender;
using ERPFramework.Controls;
using System.Data.SqlClient;
using System.Data;

namespace ERPFramework.Forms
{
    public partial class BatchForm : MetroUserControl, IDocumentBase
    {
        #region Private

        private string formName = string.Empty;

        protected string ConnectionString { get; private set; }

        public ProviderType providerType { get; private set; }

        private DBMode batchStatus = DBMode.Browse;
        private bool userStopBatch;

        protected Control keyControl;
        protected Data.DBManager dbManager;
        internal ControlBinder controlBinder;

        #endregion

        #region Properties

        [Localizable(true)]
        [Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        public string Title { get; set; }

        [Browsable(false)]
        public bool IsNew { get; private set; }

        [Browsable(false)]
        public Control KeyControl { get { return keyControl; } }

        [Browsable(false)]
        public DBMode DocumentMode { get { return batchStatus; } }

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

        #endregion

        #region Events & Delegate

        public delegate bool ToolbarButtonEventHandler(ToolStripButton sender);

        public delegate bool ToolbarSplitButtonEventHandler(ToolStripSplitButton sender);

        public event EventHandler Exit;

        #endregion

        #region Virtual Function

        protected virtual void OnInitializeData()
        {
        }

        protected virtual void OnRun()
        {
        }

        protected virtual bool OnBeforeRun()
        {
            return true;
        }

        protected virtual void OnDisableControlsForRun()
        {
        }

        protected virtual void OnBindData()
        {
        }

        #endregion

        public BatchForm()
        {
            InitializeComponent();
        }

        public BatchForm(string formname)
        {
            ConnectionString = GlobalInfo.DBaseInfo.dbManager.DB_ConnectionString;
            this.providerType = GlobalInfo.LoginInfo.ProviderType;
            formName = formname;
            InitializeComponent();
            controlBinder = new ControlBinder();
        }


        protected override void OnLoad(EventArgs e)
        {
            OnInitializeData();
            if (controlBinder != null)
                controlBinder.Enable(true);

            OnBindData();

            ManageToolbarEvents();
            OnDisableControlsForRun();

            base.OnLoad(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            OnReady();
        }

        protected virtual void OnReady()
        {
        }

        public void Close()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        protected bool UserStopBatch()
        {
            Application.DoEvents();
            return userStopBatch;
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

        private void formDB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
            }
        }

        private void formDB_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F12:
                    //if (tbnPreview.Enabled)
                    //    OnStartPrintEvent("");
                    break;
            }
        }

        private void formDB_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = OnBeforeClosing();
        }

        /// <summary>
        /// Only For Enable/Disable Controls
        /// </summary>
        public Binding BindControl(Control control)
        {
            controlBinder.Bind(control);
            return null;
        }

        #region virtual override function

        protected virtual bool OnBeforeClosing()
        {
            return false;
        }
        #endregion

        #region Enable Disable OperationButton

        private void ManageToolbarEvents()
        {
            if (controlBinder != null)
                controlBinder.Enable(batchStatus == DBMode.Browse);
        }

        #endregion

        private void OnExitEvent()
        {
            if (Exit != null)
                Exit(this.Parent, EventArgs.Empty);
        }

        private void metroToolbar1_ItemClicked(object sender, MetroFramework.Extender.MetroToolbarButtonType e)
        {
            switch (e)
            {
                case MetroToolbarButtonType.Execute:
                    OnExecuteEvent();
                    break;
                case MetroToolbarButtonType.Exit:
                    OnExitEvent();
                    break;
            }
        }

        private void OnExecuteEvent()
        {
            if (batchStatus == DBMode.Run)
            {
                userStopBatch = MetroMessageBox.Show(this, Properties.Resources.Msg_StopBatch, Properties.Resources.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes;
                return;
            }

            if (!this.Validate())
                return;

            if (OnBeforeRun())
            {
                metroToolbar1.Status = MetroToolbarState.Run;
                userStopBatch = false;
                batchStatus = DBMode.Run;
                ManageToolbarEvents();
                UseWaitCursor = true;
                //Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                OnRun();
                //Cursor = System.Windows.Forms.Cursors.Default;
                UseWaitCursor = false;
                batchStatus = DBMode.Browse;
                ManageToolbarEvents();
                OnDisableControlsForRun();
                metroToolbar1.Status = MetroToolbarState.Browse;
            }
        }
    }
}