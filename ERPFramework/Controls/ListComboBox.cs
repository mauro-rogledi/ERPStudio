using ERPFramework.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    [Designer(typeof(MySnapLinesDesigner))]
    public partial class ListComboBox : UserControl, ISnapLineControl
    {
        public Control textBoxValue { get { return txtText; } }

        public Control labelValue { get { return null; } }

        private ComboBoxStyle dropDownStyle = ComboBoxStyle.DropDown;
        private List<GenericList<string>> SL = null;
        private Graphics graphics = null;

        internal float MaxDescLength = 0;

        #region Public Properties

        [System.ComponentModel.Category("Appearance")]
        public ComboBoxStyle DropDownStyle
        {
            get { return dropDownStyle; }
            set { dropDownStyle = value; OnDropDownStyleChanged(); }
        }

        [System.ComponentModel.Category("Appearance")]
        public Control DescriptionControl { private get; set; }

        [Browsable(true)]
        public override string Text
        {
            get
            {
                return txtText.Text;
            }
            set
            {
                txtText.Text = value;
                popup_AfterRowSelectEvent(this, txtText.Text);
            }
        }

        public string GetDescription(string code)
        {
            GenericList<string> val = SL.Find(p => p.Archive.Equals(txtText.Text));
            return val == null
                    ? ""
                    : val.Display;
        }

        public void Clear()
        {
            SL.Clear();
        }

        public int SelectionStart
        {
            get { return txtText.SelectionStart; }
            set { txtText.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return txtText.SelectionLength; }
            set { txtText.SelectionLength = value; }
        }

        #endregion

        public ListComboBox()
        {
            InitializeComponent();
            OnDropDownStyleChanged();
            SL = new List<GenericList<string>>();
            graphics = Graphics.FromHwnd(this.Handle);
        }

        public void AddElements(List<GenericList<string>> sl)
        {
            SL.Clear();
            foreach (GenericList<string> elem in sl)
                AddElement(elem.Archive, elem.Display);
        }

        public void AddElement(string code, string desc)
        {
            SL.Add(new GenericList<string>(code, desc));
            float lenDesc = graphics.MeasureString(desc, this.Font).Width;

            if (lenDesc > MaxDescLength) MaxDescLength = lenDesc;
        }

        public void RefreshButton()
        {
        }

        private void OnDropDownStyleChanged()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListComboBox));
            this.btnButton.Dock = DropDownStyle == System.Windows.Forms.ComboBoxStyle.DropDown
                                    ? System.Windows.Forms.DockStyle.Right
                                    : System.Windows.Forms.DockStyle.Fill;

            this.btnButton.Image = ((System.Drawing.Image)(resources.GetObject("btnButton.Image")));
            this.btnButton.ImageAlign = DropDownStyle == System.Windows.Forms.ComboBoxStyle.DropDown
                                    ? System.Drawing.ContentAlignment.MiddleCenter
                                    : System.Drawing.ContentAlignment.MiddleRight;

            this.btnButton.TextAlign = DropDownStyle == System.Windows.Forms.ComboBoxStyle.DropDown
                                         ? ContentAlignment.MiddleCenter
                                         : ContentAlignment.MiddleLeft;

            this.txtText.Dock = DropDownStyle == System.Windows.Forms.ComboBoxStyle.DropDown
                                    ? System.Windows.Forms.DockStyle.Fill
                                    : System.Windows.Forms.DockStyle.None;

            this.txtText.Visible = DropDownStyle == System.Windows.Forms.ComboBoxStyle.DropDown;
        }

        private void btnButton_Click(object sender, EventArgs e)
        {
            PopUpListBox<string> popup = new PopUpListBox<string>(SL, txtText.Text, Math.Max(MaxDescLength, this.Width));
            popup.AfterRowSelectEvent += new PopUpListBox<string>.AfterRowSelectEventHandler(popup_AfterRowSelectEvent);
            popup.Closed += new EventHandler(popup_Closed);
            popup.Location = location();
            popup.Show();
        }

        private void popup_AfterRowSelectEvent(object sender, string SelectedValue)
        {
            if (DropDownStyle == System.Windows.Forms.ComboBoxStyle.DropDown)
            {
                txtText.Focus();
                txtText.Text = SelectedValue;
            }
            else
            {
                txtText.Text = SelectedValue;
                GenericList<string> val = SL.Find(p => p.Archive.Equals(SelectedValue));
                btnButton.Text = val == null ? string.Empty : val.Display;
            }
        }

        private Point location()
        {
            int X = this.PointToScreen(txtText.Location).X;
            int Y = this.PointToScreen(txtText.Location).Y + txtText.Size.Height;

            return new Point(X, Y);
        }

        private void popup_Closed(object sender, EventArgs e)
        {
            txtText.Focus();
        }

        private void LookUpComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (SL.Find(p => p.Archive.Equals(txtText.Text)) == null)
            {
                MessageBox.Show("errore");

                //e.Cancel = true;
            }
        }

        private void LookUpComboBox_Validated(object sender, EventArgs e)
        {
            if (DescriptionControl != null)
            {
                GenericList<string> val = SL.Find(p => p.Archive.Equals(txtText.Text));
                DescriptionControl.Text = val.Display;
            }
        }

        private void txtText_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        private void btnButton_Enter(object sender, EventArgs e)
        {
            popup_AfterRowSelectEvent(sender, txtText.Text);
        }
    }
}