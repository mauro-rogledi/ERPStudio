namespace ERPManager.MenuManager
{
    partial class MenuControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tbcForms = new ERPManager.MenuManager.ApplicationTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.AutoScroll = true;
            this.splitContainer.Panel1MinSize = 175;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tbcForms);
            this.splitContainer.Size = new System.Drawing.Size(849, 611);
            this.splitContainer.SplitterDistance = 175;
            this.splitContainer.TabIndex = 1;
            // 
            // tbcForms
            // 
            this.tbcForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcForms.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.tbcForms.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            this.tbcForms.Location = new System.Drawing.Point(0, 0);
            this.tbcForms.Name = "tbcForms";
            this.tbcForms.Size = new System.Drawing.Size(670, 611);
            this.tbcForms.TabIndex = 0;
            this.tbcForms.UseSelectable = true;
            this.tbcForms.UseStyleColors = true;
            // 
            // MenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MenuControl";
            this.Size = new System.Drawing.Size(849, 611);
            this.UseStyleColors = true;
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ApplicationTabControl tbcForms;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
