namespace ERPManager.Forms
{
    partial class DataEntryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataEntryForm));
            this.metroFlowLayoutPanel2 = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.mfpTopPanel = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.metroLink2 = new MetroFramework.Controls.MetroLink();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.metroLink9 = new MetroFramework.Controls.MetroLink();
            this.metroFlowLayoutPanel3 = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.metroLink6 = new MetroFramework.Controls.MetroLink();
            this.metroLink7 = new MetroFramework.Controls.MetroLink();
            this.metroFlowLayoutPanel2.SuspendLayout();
            this.mfpTopPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.metroFlowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroFlowLayoutPanel2
            // 
            resources.ApplyResources(this.metroFlowLayoutPanel2, "metroFlowLayoutPanel2");
            this.metroFlowLayoutPanel2.Controls.Add(this.mfpTopPanel);
            this.metroFlowLayoutPanel2.Name = "metroFlowLayoutPanel2";
            this.metroFlowLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.metroFlowLayoutPanel2_Paint);
            // 
            // mfpTopPanel
            // 
            resources.ApplyResources(this.mfpTopPanel, "mfpTopPanel");
            this.mfpTopPanel.Controls.Add(this.metroLink2);
            this.mfpTopPanel.Name = "mfpTopPanel";
            // 
            // metroLink2
            // 
            this.metroLink2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.metroLink2.Image = global::ERPFramework.Properties.Resources.Edit32;
            this.metroLink2.ImageSize = 32;
            resources.ApplyResources(this.metroLink2, "metroLink2");
            this.metroLink2.Name = "metroLink2";
            this.metroLink2.NoFocusImage = global::ERPFramework.Properties.Resources.Edit32g;
            this.metroLink2.UseSelectable = true;
            this.metroLink2.UseStyleColors = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.metroFlowLayoutPanel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroLink9, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // metroLink9
            // 
            resources.ApplyResources(this.metroLink9, "metroLink9");
            this.metroLink9.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.metroLink9.Image = global::ERPFramework.Properties.Resources.Print32;
            this.metroLink9.ImageSize = 32;
            this.metroLink9.Name = "metroLink9";
            this.metroLink9.NoFocusImage = global::ERPFramework.Properties.Resources.Print32g;
            this.metroLink9.UseSelectable = true;
            this.metroLink9.UseStyleColors = true;
            // 
            // metroFlowLayoutPanel3
            // 
            resources.ApplyResources(this.metroFlowLayoutPanel3, "metroFlowLayoutPanel3");
            this.metroFlowLayoutPanel3.Controls.Add(this.metroLink6);
            this.metroFlowLayoutPanel3.Controls.Add(this.metroLink7);
            this.metroFlowLayoutPanel3.Name = "metroFlowLayoutPanel3";
            // 
            // metroLink6
            // 
            this.metroLink6.Image = global::ERPFramework.Properties.Resources.Save32;
            this.metroLink6.ImageSize = 32;
            resources.ApplyResources(this.metroLink6, "metroLink6");
            this.metroLink6.Name = "metroLink6";
            this.metroLink6.NoFocusImage = global::ERPFramework.Properties.Resources.Save32g;
            this.metroLink6.UseSelectable = true;
            this.metroLink6.UseStyleColors = true;
            // 
            // metroLink7
            // 
            this.metroLink7.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.metroLink7.Image = global::ERPFramework.Properties.Resources.Undo32;
            this.metroLink7.ImageSize = 32;
            resources.ApplyResources(this.metroLink7, "metroLink7");
            this.metroLink7.Name = "metroLink7";
            this.metroLink7.NoFocusImage = global::ERPFramework.Properties.Resources.Undo32g;
            this.metroLink7.UseSelectable = true;
            this.metroLink7.UseStyleColors = true;
            // 
            // DataEntryForm
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackLocation = MetroFramework.Forms.BackLocation.TopRight;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.metroFlowLayoutPanel2);
            this.DisplayHeader = false;
            this.Name = "DataEntryForm";
            this.Load += new System.EventHandler(this.DataEntryForm_Load);
            this.metroFlowLayoutPanel2.ResumeLayout(false);
            this.metroFlowLayoutPanel2.PerformLayout();
            this.mfpTopPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.metroFlowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected internal MetroFramework.Extender.MetroFlowLayoutPanel metroFlowLayoutPanel2;
        protected internal MetroFramework.Extender.MetroFlowLayoutPanel mfpTopPanel;
        private MetroFramework.Controls.MetroLink metroLink2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        protected internal MetroFramework.Extender.MetroFlowLayoutPanel metroFlowLayoutPanel3;
        private MetroFramework.Controls.MetroLink metroLink6;
        private MetroFramework.Controls.MetroLink metroLink7;
        private MetroFramework.Controls.MetroLink metroLink9;
    }
}