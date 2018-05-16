using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERPFramework.Controls;
using ERPFramework.Data;
using ERPFramework.Preferences;

namespace ERPFramework.Forms
{
    public partial class PrinterForm : MetroFramework.Controls.MetroUserControl, IDocumentBase
    {
        public enum PrintMode { Preview, Print };
        public string Title { get; set; }

        public DBMode DocumentMode { get { return DBMode.Browse; } }

        private Dictionary<string, ParameterValues> ParameterList;
        private PreferencesManager<PrinterPref> myPrinter;
        private ReportClass myReport;
        private PrinterPref pPref;
        private PrintMode printMode;
        private PrintType printType;
        private PaperSource paperSource;
        private string application;
        private readonly List<string> clickableColumn = new List<string>();
        private readonly EnumsManager<PageRange> pageRangeManager = new EnumsManager<PageRange>(Properties.Resources.ResourceManager);

        public delegate void EmailArgsEventHanlder(object sender, EmailArgs e);

        public event EmailArgsEventHanlder SendEmail;

        public delegate void LoadAddressEventHandler(ERPFramework.Emailer.emailAddress ea);

        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event LoadAddressEventHandler LoadAddress;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event EventHandler Exit;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        [Description("Occurs after print execute")]
        public event EventHandler OnPrinted;
        [Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        public event EventHandler<CrystalDecisions.Windows.Forms.PageMouseEventArgs> DoubleClickPage;

        [Browsable(false)]
        public SqlProxyTransaction Transaction { get; set; } = null;

        [Browsable(false)]
        public SqlProxyConnection Connection { get; } = null;

        protected virtual void OnDoubleClickColumn(string column, string value)
        {
        }

        public void SetPrinterForm(string application, PrintMode printMode = PrintMode.Preview, PrintType printType = PrintType.Letter)
        {
            this.application = application;
            this.printMode = printMode;
            this.printType = printType;
            LoadPreferences();
        }

        public PrinterForm()
            : this("", PrintMode.Preview, PrintType.Letter)
        { }

        public PrinterForm(string application, PrintMode printMode, PrintType printType)
        {
            InitializeComponent();
            this.application = application;
            this.printMode = printMode;
            this.printType = printType;
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (!application.IsEmpty())
                LoadPreferences();

            foreach (ZoomLevel i in Enum.GetValues(typeof(ZoomLevel)))
            {
                var tsi = new ToolStripMenuItem(ERPFrameworkTranslator.Translate(Enum.GetName(typeof(ZoomLevel), i)))
                {
                    Tag = i.Int()
                };
                btnZoomIn.AddDropDownItem(tsi);
            }
        }

        private void LoadPreferences()
        {
            myPrinter = new PrinterPreferencesManager(application);

            pageRangeManager.AttachTo(cbbPages.Control);
            cbbPages.SelectedText = Properties.Resources.E_Page_All;

            pPref = myPrinter.ReadPreference();

            foreach (String printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                cbbPrinter.Items.Add(printer.ToString());

            switch (printType)
            {
                case PrintType.Letter:
                    cbbPrinter.Text = pPref.L_PrinterName;
                    nudCopies.Value = pPref.L_Copies;
                    btnCollate.Checked = pPref.L_Collate;
                    paperSource = (PaperSource)pPref.L_Source;
                    break;

                case PrintType.Envelope:
                    cbbPrinter.Text = pPref.E_PrinterName;
                    nudCopies.Value = pPref.E_Copies;
                    btnCollate.Checked = pPref.E_Collate;
                    paperSource = (PaperSource)pPref.E_Source;
                    break;

                case PrintType.Label:
                    cbbPrinter.Text = pPref.B_PrinterName;
                    nudCopies.Value = pPref.B_Copies;
                    btnCollate.Checked = pPref.B_Collate;
                    paperSource = (PaperSource)pPref.B_Source;
                    break;
            }
        }

        private void OnExitEvent()
        {
            if (Exit != null)
                Exit(this.Parent, EventArgs.Empty);
        }

        public void ShowReport<T>(string reportName, object dataSource, string detailName = "", object detail = null)
        {
            System.Diagnostics.Debug.Assert(typeof(T).BaseType.Equals(typeof(ReportClass)), "Type isn't report class");

            myReport = Activator.CreateInstance<T>() as ReportClass;
            myReport.SummaryInfo.ReportTitle = reportName;
            ShowReport(dataSource, detailName, detail);
            nudFromPage.Value = 1;
            nudToPage.Value = GetPages();
            nudToPage.MaxValue = nudToPage.Value;
        }

        public string ExportToPdf(string filename)
        {
            filename = filename.Replace(" ", string.Empty).Replace("/", "_");
            var file = System.IO.Path.ChangeExtension(System.IO.Path.Combine(System.IO.Path.GetTempPath(), filename), "pdf");
            myReport.ExportToDisk(ExportFormatType.PortableDocFormat, file);
            return file;
        }

        public int GetPages()
        {
            crystalReportViewer1.ShowLastPage();
            var lastPage = crystalReportViewer1.GetCurrentPageNumber();
            crystalReportViewer1.ShowFirstPage();

            return lastPage; // lastPage;
        }

        private void ShowReport(object dataSource, string detailName, object detail)
        {
            myReport.SetDataSource(dataSource);
            if (detail != null)
                myReport.Subreports[detailName].SetDataSource(detail);
            if (ParameterList != null)
            {
                foreach (KeyValuePair<string, ParameterValues> kvp in ParameterList)
                    myReport.ParameterFields[kvp.Key].CurrentValues = kvp.Value;
            }
            if (printMode == PrintMode.Preview)
                crystalReportViewer1.ReportSource = myReport;
            else
                btnPrint_Click(this, EventArgs.Empty);
        }

        public void SetParameterValue(string paramName, object paramValue)
        {
            if (ParameterList == null)
                ParameterList = new Dictionary<string, ParameterValues>();

            if (ParameterList.ContainsKey(paramName))
                ParameterList.Remove(paramName);

            var parameterDiscreteValue = new ParameterDiscreteValue
            {
                Value = paramValue
            };
            var currentParameterValues = new ParameterValues();
            currentParameterValues.Add(parameterDiscreteValue);

            ParameterList.Add(paramName, currentParameterValues);
        }

        public void ChangeParameterValue(string paramName, object paramValue)
        {
            if (ParameterList != null && ParameterList.ContainsKey(paramName))
            {
                var parameterDiscreteValue = new ParameterDiscreteValue
                {
                    Value = paramValue
                };
                var currentParameterValues = new ParameterValues();
                currentParameterValues.Add(parameterDiscreteValue);
                myReport.ParameterFields[paramName].CurrentValues = currentParameterValues;
            }
        }

        public void RemoveAllParameters()
        {
            if (ParameterList != null)
                ParameterList.Clear();
        }

        private void tsbEmail_Click(object sender, EventArgs e)
        {
            using (Emailer.emailForm ef = new Emailer.emailForm())
            {
                ef.LoadAddress += new Emailer.emailForm.LoadAddressEventHandler(ef_LoadAddress);
                if (SendEmail != null)
                {
                    var emailArgs = new EmailArgs();
                    SendEmail(this, emailArgs);
                    ef.Address = emailArgs.Address;
                    ef.Subject = emailArgs.Subject;
                    ef.Body = emailArgs.Body;
                    ef.Attachment = emailArgs.Attachment;
                }
                ef.ShowDialog();
                ef.Close();
            }
        }

        private void ef_LoadAddress(Emailer.emailAddress ea)
        {
            if (LoadAddress != null)
                LoadAddress(ea);
        }

        public void AddClickableColumn(IColumn column)
        {
            if (!clickableColumn.Contains(column.Name))
                clickableColumn.Add(column.Name);
        }

        private void crystalReportViewer1_DoubleClickPage(object sender, CrystalDecisions.Windows.Forms.PageMouseEventArgs e)
        {
            GlobalInfo.MainForm.UseWaitCursor = true;
            Application.DoEvents();

            if (DoubleClickPage != null)
                DoubleClickPage(sender, e);

            if (clickableColumn.Contains(e.ObjectInfo.Name))
                OnDoubleClickColumn(e.ObjectInfo.Name, e.ObjectInfo.Text);

            GlobalInfo.MainForm.UseWaitCursor = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            OnExitEvent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public void Print()
        {
            myReport.PrintOptions.PrinterName = cbbPrinter.Text;
            myReport.PrintOptions.PaperSource = paperSource;

            switch (pageRangeManager.GetValue())
            {
                case PageRange.E_Page_All:
                    myReport.PrintToPrinter(nudCopies.Value, btnCollate.Checked, 0, 0);
                    break;
                case PageRange.E_Page_Current:
                    var page = crystalReportViewer1.GetCurrentPageNumber();
                    myReport.PrintToPrinter(nudCopies.Value, btnCollate.Checked, page, page);
                    break;
                case PageRange.E_Page_Range:
                    myReport.PrintToPrinter(nudCopies.Value, btnCollate.Checked, nudFromPage.Value, nudToPage.Value);
                    break;
            }

            if (OnPrinted != null)
                OnPrinted(this, EventArgs.Empty);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void btnPageUp_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ShowPreviousPage();
        }

        private void btnPageDn_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ShowNextPage();
        }

        private void btnPageFirst_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ShowFirstPage();
        }

        private void btnPageLast_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ShowLastPage();
        }

        private void btnZoomIn_DropDownItemClicked(object sender, EventArgs e)
        {
            var tsmi = sender as ToolStripMenuItem;
            crystalReportViewer1.Zoom((int)tsmi.Tag);
        }

        private void cbbPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (pageRangeManager.GetValue())
            {
                case PageRange.E_Page_All:
                    nudFromPage.Enabled = false;
                    nudToPage.Enabled = false;
                    break;
                case PageRange.E_Page_Current:
                    nudFromPage.Enabled = false;
                    nudToPage.Enabled = false;
                    break;
                case PageRange.E_Page_Range:
                    nudFromPage.Enabled = true;
                    nudToPage.Enabled = true;
                    nudFromPage.MinValue = 1;
                    nudFromPage.MaxValue = GetPages();
                    break;
            }
        }

        private void nudFromPage_TextChanged(object sender, EventArgs e)
        {
            nudToPage.MinValue = nudFromPage.Value;
            nudToPage.MaxValue = nudFromPage.MaxValue;
            if (nudToPage.Value < nudFromPage.Value)
                nudToPage.Value = nudFromPage.Value;
        }

    }

    public class EmailArgs : EventArgs
    {
        public object Sender { get; set; }

        public string Address { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Attachment { get; set; }
    }
}