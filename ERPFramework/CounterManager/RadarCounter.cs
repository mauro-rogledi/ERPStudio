using System;
using System.ComponentModel;
using System.Windows.Forms;

using ERPFramework.Controls;
using ERPFramework.Data;

namespace ERPFramework.CounterManager
{
    [Designer(typeof(MySnapLinesDesigner))]
    public partial class RadarCounters : CounterControl
    {
        protected RadarForm rdrRadarForm = null;

        public RadarCounters()
        {
            InitializeComponent();
            button1.Enabled = false;
            DelegateValidating = false;
            EnableAddOnFly = true;
            MustExistData = true;
        }

        #region Public Properties

        public bool MustExistData { get; set; }

        public bool EnableAddOnFly { get; set; }

        public bool DelegateValidating { get; set; }

        public string Description { get; private set; }

        public Control DescriptionControl { private get; set; }

        public override void AttachCounterType(int key, DateTime curDate, SqlABTransaction transaction)
        {
            base.AttachCounterType(key, curDate, transaction);
            IsAutomatic = false;
            EditButtonVisible = false;
        }

        public RadarForm RadarForm
        {
            set
            {
                rdrRadarForm = value;
                button1.Enabled = value != null;
                if (rdrRadarForm != null)
                    rdrRadarForm.RadarFormRowSelected += new RadarForm.RadarFormRowSelectedEventHandler(rdrRadarForm_RadarFormRowSelected);
            }
            get
            {
                return rdrRadarForm;
            }
        }

        #endregion

        private void rdrRadarForm_RadarFormRowSelected(object sender, RadarFormRowSelectedArgs pe)
        {
            Text = rdrRadarForm.Seed;
            if (DescriptionControl != null)
                DescriptionControl.Text = rdrRadarForm.Description;

            Description = rdrRadarForm.Description;
        }

        public void UpdateDescription()
        {
            IRadarParameters iParam = rdrRadarForm.GetRadarParameters(Text);
            DescriptionControl.Text = (Text != "" && rdrRadarForm.Find(iParam))
                                    ? rdrRadarForm.Description
                                    : "";
        }

        private void RadarTextBox_Resize(object sender, EventArgs e)
        {
            button1.Width = button1.Height + 1;
            button1.Left = this.Size.Width - button1.Width;
        }

        public override void Refresh()
        {
            base.Refresh();
            Width += 25;
            button1.Left = this.Size.Width - button1.Width;
        }

        private void RadarTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (rdrRadarForm == null) return;
            string cnt = Text;

            IRadarParameters iParam = rdrRadarForm.GetRadarParameters(cnt);

            if (cnt != EmptyCode && !rdrRadarForm.Find(iParam))
            {
                if (EnableAddOnFly && rdrRadarForm.CanOpenNew)
                {
                    DialogResult result = MessageBox.Show(Properties.Resources.Msg_InsertNewCode,
                                                            Properties.Resources.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        rdrRadarForm.OpenNew(iParam);
                        Focus();
                    }
                }
                else if (MustExistData)
                {
                    MessageBox.Show(Properties.Resources.Msg_CodeNotFound,
                                                            Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Focus();
                }
                e.Cancel = MustExistData;
            }
            else
            {
                if (cnt == EmptyCode)
                {
                    Description = string.Empty;
                    return;
                }
            }
            Description = rdrRadarForm.Description;
        }

        private void RadarTextBox_Validated(object sender, EventArgs e)
        {
            if (rdrRadarForm == null || DescriptionControl == null) return;

            DescriptionControl.Text = Description;
        }

        private void textBox1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.F8)
            {
                Search();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (rdrRadarForm != null)
            {
                rdrRadarForm.Seed = Text;
                if (rdrRadarForm.ShowDialog() == DialogResult.OK)
                {
                    Focus();
                }
            }
        }
    }
}