namespace ERPFramework.Emailer
{
    partial class emailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(emailForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.tsbSend = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAttachment = new System.Windows.Forms.TextBox();
            this.pbxAttachment = new System.Windows.Forms.PictureBox();
            this.rtfBody = new ERPFramework.RicherTextBox.RicherTextBox();
            this.btnLoadAddress = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAttachment)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtAddress
            // 
            resources.ApplyResources(this.txtAddress, "txtAddress");
            this.txtAddress.Name = "txtAddress";
            // 
            // txtSubject
            // 
            resources.ApplyResources(this.txtSubject, "txtSubject");
            this.txtSubject.Name = "txtSubject";
            // 
            // tsbSend
            // 
            this.tsbSend.Image = global::ERPFramework.Properties.Resources.Edit24g;
            resources.ApplyResources(this.tsbSend, "tsbSend");
            this.tsbSend.Name = "tsbSend";
            this.tsbSend.Click += new System.EventHandler(this.tsbSend_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSend});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtAttachment
            // 
            resources.ApplyResources(this.txtAttachment, "txtAttachment");
            this.txtAttachment.Name = "txtAttachment";
            this.txtAttachment.ReadOnly = true;
            // 
            // pbxAttachment
            // 
            resources.ApplyResources(this.pbxAttachment, "pbxAttachment");
            this.pbxAttachment.Image = global::ERPFramework.Properties.Resources.Automatic16g;
            this.pbxAttachment.Name = "pbxAttachment";
            this.pbxAttachment.TabStop = false;
            this.pbxAttachment.Click += new System.EventHandler(this.pbxAttachment_Click);
            // 
            // rtfBody
            // 
            this.rtfBody.AlignCenterVisible = false;
            this.rtfBody.AlignLeftVisible = false;
            this.rtfBody.AlignRightVisible = false;
            this.rtfBody.AllowDrop = true;
            this.rtfBody.BoldVisible = false;
            this.rtfBody.BulletsVisible = false;
            this.rtfBody.ChooseFontVisible = false;
            resources.ApplyResources(this.rtfBody, "rtfBody");
            this.rtfBody.FindReplaceVisible = false;
            this.rtfBody.FontColorVisible = false;
            this.rtfBody.FontFamilyVisible = false;
            this.rtfBody.FontSizeVisible = false;
            this.rtfBody.GroupAlignmentVisible = false;
            this.rtfBody.GroupBoldUnderlineItalicVisible = false;
            this.rtfBody.GroupFontColorVisible = false;
            this.rtfBody.GroupFontNameAndSizeVisible = false;
            this.rtfBody.GroupIndentationAndBulletsVisible = false;
            this.rtfBody.GroupInsertVisible = false;
            this.rtfBody.GroupSaveAndLoadVisible = false;
            this.rtfBody.GroupZoomVisible = false;
            this.rtfBody.INDENT = 10;
            this.rtfBody.IndentVisible = false;
            this.rtfBody.InsertPictureVisible = false;
            this.rtfBody.IsSpellingEnabled = true;
            this.rtfBody.ItalicVisible = false;
            this.rtfBody.LoadVisible = false;
            this.rtfBody.Name = "rtfBody";
            this.rtfBody.OutdentVisible = false;
            this.rtfBody.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1040{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}\r" +
    "\n\\viewkind4\\uc1\\pard\\f0\\fs20\\par\r\n}\r\n";
            this.rtfBody.SaveVisible = false;
            this.rtfBody.SeparatorAlignVisible = false;
            this.rtfBody.SeparatorBoldUnderlineItalicVisible = false;
            this.rtfBody.SeparatorFontColorVisible = false;
            this.rtfBody.SeparatorFontVisible = false;
            this.rtfBody.SeparatorIndentAndBulletsVisible = false;
            this.rtfBody.SeparatorInsertVisible = false;
            this.rtfBody.SeparatorSaveLoadVisible = false;
            this.rtfBody.ToolStripVisible = false;
            this.rtfBody.UnderlineVisible = false;
            this.rtfBody.WordWrapVisible = false;
            this.rtfBody.ZoomFactorTextVisible = false;
            this.rtfBody.ZoomInVisible = false;
            this.rtfBody.ZoomOutVisible = false;
            // 
            // btnLoadAddress
            // 
            resources.ApplyResources(this.btnLoadAddress, "btnLoadAddress");
            this.btnLoadAddress.Name = "btnLoadAddress";
            this.btnLoadAddress.UseVisualStyleBackColor = true;
            this.btnLoadAddress.Click += new System.EventHandler(this.btnLoadAddress_Click);
            // 
            // emailForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLoadAddress);
            this.Controls.Add(this.pbxAttachment);
            this.Controls.Add(this.txtAttachment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.rtfBody);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "emailForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAttachment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private RicherTextBox.RicherTextBox rtfBody;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.ToolStripButton tsbSend;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAttachment;
        private System.Windows.Forms.PictureBox pbxAttachment;
        private System.Windows.Forms.Button btnLoadAddress;
    }
}