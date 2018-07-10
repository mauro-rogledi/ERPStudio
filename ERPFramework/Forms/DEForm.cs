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

namespace ERPFramework.Forms
{
    public partial class DataEntryForm : MetroForm
    {
        public DataEntryForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            OnBindData();
        }

        public virtual void OnBindData() { }

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
