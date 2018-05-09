
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERPFramework.Data;
using ERPFramework.ModulesHelper;
using ERPFramework.Preferences;
using MetroFramework.Controls;
using MetroFramework.Extender;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ERPFramework.Forms
{
    public partial class ReportForm : MetroUserControl, IDocumentBase
    {
        #region Private

        protected string ConnectionString { get; private set; }

        public ProviderType providerType { get; private set; }

        private DBMode batchStatus = DBMode.Browse;
        private ReportClass myReport = null;
        private readonly List<string> clickableColumn = new List<string>();

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

        [Category(ErpFrameworkDefaults.PropertyCategory.Behaviour)]
        public PrintType PrintType { get; set; }

        [Browsable(false)]
        public DBMode DocumentMode { get { return batchStatus; } }

        [Browsable(false)]
        public SqlProxyTransaction Transaction { get; set; } = null;

        [Browsable(false)]
        public SqlProxyConnection Connection { get; } = null;

        #endregion

        #region Events & Delegate
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

        protected virtual void OnDoubleClickColumn(string column, string value)
        {
        }

        protected virtual void OnPrinted() { }

        #endregion

        public ReportForm()
        {
            InitializeComponent();
        }

        public ReportForm(string formname)
        {
            Name = formname;
            InitializeComponent();

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            ConnectionString = GlobalInfo.DBaseInfo.dbManager.DB_ConnectionString;
            this.providerType = GlobalInfo.LoginInfo.ProviderType;
            controlBinder = new ControlBinder();

            printerForm.SetPrinterForm(Name);
        }

        public void AddClickableColumn(IColumn column)
        {
            AddClickableColumn(column.Name);
        }

        public void AddClickableColumn(string column)
        {
            if (!clickableColumn.Contains(column))
                clickableColumn.Add(column);
        }

        protected virtual void OnCustomizeToolbar(MetroToolbar toolstrip)
        {
            //toolstrip.ButtonPrefVisible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            tbcMain.SelectedTab = tbpSelection;

            OnInitializeData();
            OnCustomizeToolbar(metroToolbar);

            OnBindData();

            if (controlBinder != null)
                controlBinder.Enable(true);


            ManageToolbarEvents();
            OnDisableControlsForRun();

            OnAddSplitMenuButton();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            OnReady();
        }

        protected virtual void OnReady()
        {
        }

        protected bool UserStopBatch()
        {
            Application.DoEvents();
            return userStopBatch;
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

        /// <summary>
        /// Only For Enable/Disable Controls
        /// </summary>
        /// <param name="control">todo: describe control parameter on BindControl</param>
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

        protected virtual bool OnPrintDocument(string sender, PrinterForm pf)
        {
            return true;
        }

        private void OnStartPrintEvent()
        {
            if (!this.Validate())
                return;

            if (OnBeforeRun())
            {
                userStopBatch = false;
                batchStatus = DBMode.Run;
                ManageToolbarEvents();
                //Parent.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                ClearParameters();
                OnRun();
                //Cursor = System.Windows.Forms.Cursors.Default;
                batchStatus = DBMode.Browse;
                ManageToolbarEvents();
                OnDisableControlsForRun();
            }
        }

        private void OnStopPrintEvent()
        {
            userStopBatch = MessageBox.Show("Do you want stop process ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes;
        }

        #endregion

        #region Enable Disable OperationButton

        private void ManageToolbarEvents()
        {
            if (controlBinder != null)
                controlBinder.Enable(batchStatus == DBMode.Browse);
        }

        #endregion


        #region Print

        protected virtual void OnAddSplitMenuButton()
        {
        }

        protected void AddSplitMenuButton(string text, string tag)
        {
        }
        #endregion

        private void OnExitEvent()
        {
            if (Exit != null)
                Exit(this.Parent, EventArgs.Empty);
        }

        private void OnPreviewEvent()
        {
            if (batchStatus != DBMode.Run)
                OnStartPrintEvent();
            else
                OnStopPrintEvent();
        }

        private void OnPrintEvent()
        {
            if (batchStatus != DBMode.Run)
                OnStartPrintEvent();
            else
            {
                OnStopPrintEvent();
                return;
            }
        }

        public void ShowReport<T>(string reportName, object dataSource, string detailName = "", object detail = null)
        {
            printerForm.ShowReport<T>(reportName, dataSource, detailName, detail);
            tbcMain.SelectedTab = tbpPrint;
        }

        public int GetPages()
        {
            return printerForm.GetPages();
        }

        public string ExportToPdf(string filename)
        {
            filename = filename.Replace(" ", string.Empty).Replace("/", "_");
            var file = System.IO.Path.ChangeExtension(System.IO.Path.Combine(System.IO.Path.GetTempPath(), filename), "pdf");
            myReport.ExportToDisk(ExportFormatType.PortableDocFormat, file);
            return file;
        }

        public void SetParameterValue(string paramName, object paramValue)
        {
            printerForm.SetParameterValue(paramName, paramValue);
        }

        public void ClearParameters()
        {
            printerForm.RemoveAllParameters();
        }

        private void OnPreferenceEvent()
        {
            var pf = new Preferences.PreferenceForm();
            OnAddPreferenceButton(pf);
            OpenDocument.Show(pf);
        }

        protected virtual void OnAddPreferenceButton(PreferenceForm prefForm)
        {
            prefForm.AddPanel(new PrinterPreferencePanel(Name));
        }

        private void tsbEmail_Click(object sender, EventArgs e)
        {
            using (var ef = new Emailer.emailForm())
            {
                ef.LoadAddress += new Emailer.emailForm.LoadAddressEventHandler(ef_LoadAddress);
                var emailArgs = SendEmail();
                ef.Address = emailArgs.Address;
                ef.Subject = emailArgs.Subject;
                ef.Body = emailArgs.Body;
                ef.Attachment = emailArgs.Attachment;

                ef.ShowDialog();
                ef.Close();
            }
        }

        private void ef_LoadAddress(Emailer.emailAddress ea)
        {
            LoadAddress(ea);
        }

        protected virtual void LoadAddress(Emailer.emailAddress ea)
        {
        }

        protected virtual EmailArgs SendEmail()
        {
            var ea = new EmailArgs
            {
                Attachment = ExportToPdf("Attachment")
            };
            return ea;
        }

        private void crystalReportViewer1_DoubleClickPage(object sender, CrystalDecisions.Windows.Forms.PageMouseEventArgs e)
        {
            if (clickableColumn.Contains(e.ObjectInfo.Name))
                OnDoubleClickColumn(e.ObjectInfo.Name, e.ObjectInfo.Text);
        }

        private void printerForm_Exit(object sender, EventArgs e)
        {
            OnExitEvent();
        }

        private void metroToolbar_ItemClicked(object sender, MetroToolbarButtonType e)
        {
            switch (e)
            {
                case MetroToolbarButtonType.Preview:
                    OnPreviewEvent();
                    break;
                case MetroToolbarButtonType.Print:
                    OnPrintEvent();
                    break;
                case MetroToolbarButtonType.Exit:
                    OnExitEvent();
                    break;
                case MetroToolbarButtonType.Preference:
                    OnPreferenceEvent();
                    break;
            }
        }
    }
}