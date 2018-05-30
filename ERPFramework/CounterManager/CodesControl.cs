using ERPFramework.Controls;
using ERPFramework.Data;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Extender;

namespace ERPFramework.CounterManager
{
    [Designer(typeof(MySnapLinesDesigner))]
    public partial class CodesControl : UserControl, IFindable
    {
        private List<Segment> Segments = new List<Segment>();
        private DUCodes dUCodes;
        private Graphics graphics;
        private int headerPos;

        public int controlPos { get; private set; }

        private bool hasHeader = true;
        private bool showHeader = true;
        private string Code = string.Empty;

        public string CodeType { get; set; }

        private Label designLabel;
        private TextBox designText;

        public Control textBoxValue { get { return designText; } }

        public Control labelValue { get { return designLabel; } }

        public bool ShowHeader
        {
            get { return showHeader; }
            set
            {
                showHeader = value;
                MoveDesignModeControl();
            }
        }

        public CodesControl()
        {
            InitializeComponent();
            controlPos = 0;
            if (GlobalInfo.DBaseInfo.dbManager != null)
                dUCodes = new DUCodes(null);

            AddDesignModeControl();
        }

        //public new bool Enabled
        //{
        //    set
        //    {
        //        base.Enabled = value;
        //        foreach (Segment seg in Segments)
        //            seg.Control.Enabled = value;
        //    }

        //    get
        //    {
        //        return base.Enabled;
        //    }
        //}

        #region Design Layout

        private void AddDesignModeControl()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
                return;

            if (designLabel == null)
            {
                designLabel = new Label();
                designLabel.Location = new Point(0, headerPos);
                designLabel.Text = "Header";
                designLabel.Tag = designLabel.Visible.ToString();
                designLabel.Location = new Point(0, headerPos);
                designLabel.AutoSize = true;
                this.Controls.Add(designLabel);
            }

            if (designText == null)
            {
                designText = new TextBox();
                designText.Text = "Text";
                designText.Location = new Point(0, this.Font.Height);
                this.Controls.Add(designText);
            }
        }

        private void MoveDesignModeControl()
        {
            if (designLabel == null)
                return;
            designLabel.Visible = showHeader;
            designLabel.Text = showHeader ? "Header" : "False";
            designLabel.Tag = designLabel.Visible.ToString();
            controlPos = showHeader ? this.Font.Height : 0;
            designText.Location = new Point(0, controlPos);
        }

        #endregion

        public override void Refresh()
        {
            graphics = Graphics.FromHwnd(this.Handle);
            int newctrlpos = 0;
            if (hasHeader && ShowHeader)
            {
                controlPos += this.Font.Height;
                this.Height = controlPos + this.Font.Height + 8;
            }

            this.SuspendLayout();

            foreach (Segment seg in Segments)
            {
                switch (seg.InputType)
                {
                    case InputType.E_Numeric:
                        newctrlpos = AddNumericControl(newctrlpos, seg);
                        break;

                    case InputType.E_Text:
                        newctrlpos = AddTextControl(newctrlpos, seg);
                        break;
                }
            }
            this.Width = newctrlpos;
            this.ResumeLayout();
            base.Refresh();
        }

        public virtual void AttachCodeType(string code)
        {
            if (code == CodeType)
                return;

            if (string.IsNullOrEmpty(code) || !dUCodes.Find(code))
            {
                if (!string.IsNullOrEmpty(code))
                    MessageBox.Show(this, Properties.Resources.Msg_MissingCodeDef,
                    Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DeattachAll();
            CodeType = code;
            for (int t = 0; t < dUCodes.Count; t++)
            {
                Segment seg = new Segment();
                seg.InputType = (InputType)dUCodes.GetValue<int>(EF_CodeSegment.InputType, t);
                seg.Header = dUCodes.GetValue<string>(EF_CodeSegment.Description, t);
                seg.Length = dUCodes.GetValue<int>(EF_CodeSegment.InputLen, t);
                hasHeader |= seg.Header.Length > 0;
                Segments.Add(seg);
            }

            this.Refresh();
        }

        public virtual void DeattachAll()
        {
            foreach (Segment seg in Segments)
            {
                if (seg.Control.Tag != null)
                    this.Controls.Remove((Control)seg.Control.Tag);
                this.Controls.Remove(seg.Control);
            }
            Segments.Clear();
            controlPos = 0;
            headerPos = 0;
            Code = string.Empty;
        }

        public virtual void ReattachCodeType(string key)
        {
            System.Diagnostics.Debug.Assert(dUCodes != null);
            DeattachAll();

            if (dUCodes == null || string.IsNullOrEmpty(key))
                return;

            if (!dUCodes.Find(key))
            {
                MessageBox.Show(this, Properties.Resources.Msg_MissingCodeDef,
                Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int t = 0; t < dUCodes.Count; t++)
            {
                Segment seg = new Segment();
                seg.InputType = (InputType)dUCodes.GetValue<int>(EF_CodeSegment.InputType, t);
                seg.Header = dUCodes.GetValue<string>(EF_CodeSegment.Description, t);
                seg.Length = dUCodes.GetValue<int>(EF_CodeSegment.InputLen, t);
                hasHeader |= seg.Header.Length > 0;
                Segments.Add(seg);
            }

            Code = string.Empty;

            this.Refresh();

            Encode();
        }

        [Browsable(true)]
        public override string Text
        {
            get
            {
                Encode();
                return Code;
            }
            set
            {
                Code = value;
                Decode();
            }
        }

        public bool IsEmpty
        {
            get { return String.IsNullOrEmpty(Text); }
        }

        protected string EmptyCode
        {
            get
            {
                string temp = string.Empty;
                foreach (Segment seg in Segments)
                {
                    switch (seg.InputType)
                    {
                        case InputType.E_Numeric:
                            temp += "00000000".Substring(0, seg.Length);
                            break;

                        case InputType.E_Text:
                            temp += "                        ".Substring(0, seg.Length);
                            break;
                    }
                }
                return temp.TrimEnd();
            }
        }

        public string Findable
        {
            get
            {
                string temp = string.Empty;
                string empty = string.Empty;
                foreach (Segment seg in Segments)
                {
                    switch (seg.InputType)
                    {
                        case InputType.E_Numeric:
                            if (seg.Control != null && ((MetroTextBoxNumeric)seg.Control).Int != 0)
                                temp += ((MetroTextBoxNumeric)seg.Control).Int.ToString("00000000".Substring(0, seg.Length));
                            else
                                temp += temp.EndsWith("%") ? string.Empty : "%";
                            break;

                        case InputType.E_Text:
                            if (seg.Control != null && ((TextBox)seg.Control).Text != string.Empty)
                                temp += ((TextBox)seg.Control).Text;
                            temp += temp.EndsWith("%") ? string.Empty : "%";
                            break;
                    }
                }
                return temp.EndsWith("%")
                            ? temp.Substring(0, temp.Length - 1)
                            : temp.TrimEnd();
            }
        }

        private void Decode()
        {
            int pos = 0;
            int dummy = 0;
            foreach (Segment seg in Segments)
            {
                switch (seg.InputType)
                {
                    case InputType.E_Numeric:
                        if (Code == null || pos >= Code.Length || pos + seg.Length > Code.Length)
                            ((MetroTextBoxNumeric)seg.Control).Int = 0;
                        else
                            ((MetroTextBoxNumeric)seg.Control).Int =
                                int.TryParse(Code.Substring(pos, Math.Min(Code.Length, seg.Length)), out dummy)
                                    ? dummy
                                    : 0;
                        break;

                    case InputType.E_Text:
                        if (Code == null || pos >= Code.Length)
                            ((TextBox)seg.Control).Text = "";
                        else
                            ((TextBox)seg.Control).Text = Code.Substring(pos, Math.Min(Code.Length - pos, seg.Length));
                        break;
                }
                pos += seg.Length;
            }
        }

        private void Encode()
        {
            string temp = string.Empty;
            foreach (Segment seg in Segments)
            {
                switch (seg.InputType)
                {
                    case InputType.E_Numeric:
                        temp += ((MetroTextBoxNumeric)seg.Control).Int.ToString("00000000".Substring(0, seg.Length));
                        break;

                    case InputType.E_Text:

                        //{0,-10}
                        string fmt = "{0,-" + seg.Length.ToString() + "}";
                        temp += string.Format(fmt, ((TextBox)seg.Control).Text);
                        break;
                }
            }
            Code = temp.TrimEnd();
        }

        private int AddNumericControl(int newctrlpos, Segment seg)
        {
            string text = "0123456789";
            seg.Control = new MetroTextBoxNumeric();
            seg.Control.Location = new Point(newctrlpos, controlPos);
            seg.Control.Text = "";

            //seg.Control.Enabled = this.Enabled;
            int width = (int)Math.Floor(graphics.MeasureString(text.Substring(1, seg.Length), this.Font).Width + 4);

            seg.Control.Size = new Size(width, 23);
            ((MetroTextBoxNumeric)seg.Control).MaxDecimalPlaces = 0;
            ((MetroTextBoxNumeric)seg.Control).MaxWholeDigits = seg.Length;
            ((MetroTextBoxNumeric)seg.Control).AllowNegative = false;
            ((MetroTextBoxNumeric)seg.Control).TextChanged += new EventHandler(Control_TextChanged);

            if (!seg.Header.Equals(string.Empty) && ShowHeader)
            {
                Label lbl = new Label();
                lbl.Location = new Point(newctrlpos, headerPos);
                lbl.Text = seg.Header;
                lbl.AutoSize = true;
                seg.Control.Tag = lbl;
                this.Controls.Add(lbl);
            }

            this.Controls.Add(seg.Control);
            newctrlpos += width + 2;
            return newctrlpos;
        }

        private int AddTextControl(int newctrlpos, Segment seg)
        {
            string text = "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM";
            seg.Control = new MetroTextBox();
            seg.Control.Location = new Point(newctrlpos, controlPos);
            seg.Control.Text = "";

            //seg.Control.Enabled = this.Enabled;
            ((MetroTextBox)seg.Control).CharacterCasing = CharacterCasing.Upper;
            ((MetroTextBox)seg.Control).MaxLength = seg.Length;
            int width = (int)Math.Floor(graphics.MeasureString(text.Substring(1, Math.Min(seg.Length, 15)), this.Font).Width + 4);
            ((MetroTextBox)seg.Control).TextChanged += new EventHandler(Control_TextChanged);

            seg.Control.Size = new Size(width, 23);
            ((TextBox)seg.Control).MaxLength = seg.Length;

            if (!seg.Header.Equals(string.Empty) && ShowHeader)
            {
                Label lbl = new Label();
                lbl.Location = new Point(newctrlpos, headerPos);
                lbl.Text = seg.Header;
                lbl.AutoSize = true;
                seg.Control.Tag = lbl;
                this.Controls.Add(lbl);
            }

            this.Controls.Add(seg.Control);
            newctrlpos += width + 2;
            return newctrlpos;
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }

        #region Segment

        private class Segment
        {
            public string Header { get; set; }

            public InputType InputType { get; set; }

            public int Length { get; set; }

            public Control Control { get; set; }
        }

        #endregion

        #region Findable

        public void Clean()
        {
            Text = EmptyCode;
        }

        #endregion
    }
}