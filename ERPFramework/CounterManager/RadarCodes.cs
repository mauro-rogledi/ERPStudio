using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERPFramework.Controls;

namespace ERPFramework.CounterManager
{
    public partial class RadarCodesCtrl : CodesControl
    {
        protected RadarForm rdrRadarForm;
        protected SortedList<string, string> SL;

        public int MaxCodeLength;
        public int MaxDescLength;

        public RadarCodesCtrl()
        {
            InitializeComponent();
            btnLens.Enabled = false;
            DelegateValidating = false;
            EnableAddOnFly = true;
            MustExistData = true;
            SL = new SortedList<string, string>();
        }

        #region Public Properties

        public bool EnableAddOnFly { get; set; }

        public bool MustExistData { get; set; }

        public bool DelegateValidating { get; set; }

        public string Description { get; private set; }

        public Control DescriptionControl { private get; set; }

        // TODO SelectionStart e length in codescontrol
        public int SelectionLength { get; set; }

        public int SelectionStart { get; set; }

        public RadarForm RadarForm
        {
            set
            {
                rdrRadarForm = value;
                btnLens.Enabled = value != null;
                if (rdrRadarForm != null)
                    rdrRadarForm.RadarFormRowSelected += new RadarForm.RadarFormRowSelectedEventHandler(rdrRadarForm_RadarFormRowSelected);
            }
            get
            {
                return rdrRadarForm;
            }
        }

        public void ClearElement()
        {
            SL.Clear();
        }

        public void AddElements(Dictionary<string, string> sl)
        {
            if (SL.Count == 0)
                foreach (KeyValuePair<string, string> kvp in sl)
                    AddElement(kvp.Key, kvp.Value);
        }

        public void AddElement(string code, string desc)
        {
            btnButton.Visible = true;
            SL.Add(code, desc);
            if (code.Length > MaxCodeLength) MaxCodeLength = code.Length;
            if (desc.Length > MaxDescLength) MaxDescLength = desc.Length;
        }

        #endregion

        private void rdrRadarForm_RadarFormRowSelected(object sender, RadarFormRowSelectedArgs pe)
        {
            Text = rdrRadarForm.Seed;
            if (DescriptionControl != null)
                DescriptionControl.Text = rdrRadarForm.Description;

            Description = rdrRadarForm.Description;
            OnValidating(new CancelEventArgs());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public override void AttachCodeType(string code)
        {
            base.AttachCodeType(code);
            btnLens.Visible = !string.IsNullOrEmpty(code);
        }

        public override void Refresh()
        {
            base.Refresh();
            Width += 25 + (btnButton.Visible ? btnButton.Width : 0);
            btnLens.Left = this.Size.Width - btnLens.Width;
            btnLens.Top = controlPos;
            btnButton.Left = btnLens.Left - btnButton.Width;
            btnButton.Top = controlPos;
        }

        public void UpdateDescription()
        {
            IRadarParameters iParam = rdrRadarForm.GetRadarParameters(Text);
            DescriptionControl.Text = (Text != EmptyCode && rdrRadarForm.Find(iParam))
                                    ? rdrRadarForm.Description
                                    : "";
        }

        private void RadarTextBox_Resize(object sender, EventArgs e)
        {
            btnLens.Left = this.Size.Width - btnLens.Width;
            btnLens.Top = controlPos;

            btnButton.Left = btnLens.Left - btnButton.Width;
            btnButton.Top = controlPos;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }

        private void cntControl_TextChanged(object sender, EventArgs e)
        {
            if (DescriptionControl != null && !Focused)
                UpdateDescription();

            OnTextChanged(e);
        }

        private void textBox1_LostFocus(object sender, System.EventArgs e)
        {
            OnLostFocus(e);
        }

        private void textBox1_GotFocus(object sender, System.EventArgs e)
        {
            OnGotFocus(e);
        }

        private void RadarTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (rdrRadarForm == null) return;
            IRadarParameters iParam = rdrRadarForm.GetRadarParameters(Text);

            if (Text != EmptyCode && !rdrRadarForm.Find(iParam))
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
                if (Text == EmptyCode)
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

        private void btnLens_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (rdrRadarForm != null)
            {
                rdrRadarForm.Seed = Text == EmptyCode
                    ? ""
                    : Text;
                if (rdrRadarForm.ShowDialog() == DialogResult.OK)
                {
                    Focus();
                }
            }
        }

        private void btnButton_Click(object sender, EventArgs e)
        {
            ApriCombo();
        }

        private void ApriCombo()
        {
            PopUpComboBox popup = new PopUpComboBox(SL, this.Text);
            popup.AfterRowSelectEvent += new PopUpComboBox.AfterRowSelectEventHandler(popup_AfterRowSelectEvent);
            popup.Closed += new EventHandler(popup_Closed);
            popup.Location = location();
            popup.Show();
        }

        private Point location()
        {
            int X = this.PointToScreen(this.Location).X;
            int Y = this.PointToScreen(this.Location).Y + this.Size.Height;

            return new Point(X, Y);
        }

        private void popup_AfterRowSelectEvent(object sender, string SelectedValue)
        {
            this.Focus();
            this.Text = SelectedValue;
        }

        private void popup_Closed(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}