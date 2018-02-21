using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    [Designer(typeof(MySnapLinesDesigner))]
    public partial class RadarTextBox : MetroFramework.Controls.MetroTextBox
    {
        protected RadarForm rdrRadarForm;

        public RadarTextBox()
        {
            CustomButton.Image = Properties.Resources.Search24;
            CustomButton.Location = new System.Drawing.Point(139, 1);
            CustomButton.Visible = false;
            ButtonClick += RadarTextBox_ButtonClick;
            CharacterCasing = CharacterCasing.Upper;
            DelegateValidating = false;
            EnableAddOnFly = true;
        }

        private void RadarTextBox_ButtonClick(object sender, EventArgs e)
        {
            Search();
        }

        #region Public Properties

        public bool EnableAddOnFly { get; set; }

        public bool DelegateValidating { get; set; }

        public string Description { get; private set; }

        public Control DescriptionControl { private get; set; }

        public bool TrimResult { get; set; }

        public RadarForm RadarForm
        {
            set
            {
                rdrRadarForm = value;
                CustomButton.Visible = value != null;
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
            Text = RadarForm.GetCodeFromParameters(pe.parameters);
            if (DescriptionControl != null)
                DescriptionControl.Text = rdrRadarForm.Description;

            Description = rdrRadarForm.Description;
            OnValidating(new CancelEventArgs());
        }

        public void UpdateDescription()
        {
            IRadarParameters iParam = rdrRadarForm.GetRadarParameters(Text);
            DescriptionControl.Text = (Text != string.Empty && rdrRadarForm.Find(iParam))
                                    ? rdrRadarForm.Description
                                    : "";
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            if (rdrRadarForm == null) return;

            IRadarParameters iParam = rdrRadarForm.GetRadarParameters(Text);
            if (Text != "" && !rdrRadarForm.Find(iParam))
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
                else
                    MessageBox.Show(Properties.Resources.Msg_CodeNotFound,
                                                            Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                Focus();
            }
            else
            {
                if (Text == string.Empty)
                {
                    Description = string.Empty;
                    return;
                }
            }
            Description = rdrRadarForm.Description;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (DescriptionControl != null)
                UpdateDescription();
        }

        private void RadarTextBox_Validated(object sender, EventArgs e)
        {
            if (rdrRadarForm == null || DescriptionControl == null) return;

            DescriptionControl.Text = rdrRadarForm.Description;
            Description = rdrRadarForm.Description;
        }

        private void textBox1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.F8)
            {
                Search();
            }
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