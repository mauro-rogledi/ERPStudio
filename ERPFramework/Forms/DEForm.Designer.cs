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
            this.metroFlowLayoutPanel1 = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.mtlOk = new MetroFramework.Controls.MetroLink();
            this.mtlBack = new MetroFramework.Controls.MetroLink();
            this.metroFlowLayoutPanel2 = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.metroLink1 = new MetroFramework.Controls.MetroLink();
            this.metroFlowLayoutPanel1.SuspendLayout();
            this.metroFlowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroFlowLayoutPanel1
            // 
            this.metroFlowLayoutPanel1.Controls.Add(this.mtlOk);
            this.metroFlowLayoutPanel1.Controls.Add(this.mtlBack);
            this.metroFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.metroFlowLayoutPanel1.Location = new System.Drawing.Point(20, 30);
            this.metroFlowLayoutPanel1.Name = "metroFlowLayoutPanel1";
            this.metroFlowLayoutPanel1.Size = new System.Drawing.Size(760, 39);
            this.metroFlowLayoutPanel1.TabIndex = 0;
            // 
            // mtlOk
            // 
            this.mtlOk.Image = global::ERPFramework.Properties.Resources.Checked32;
            this.mtlOk.ImageSize = 32;
            this.mtlOk.Location = new System.Drawing.Point(724, 3);
            this.mtlOk.Name = "mtlOk";
            this.mtlOk.NoFocusImage = global::ERPFramework.Properties.Resources.Checked32g;
            this.mtlOk.Size = new System.Drawing.Size(33, 33);
            this.mtlOk.TabIndex = 7;
            this.mtlOk.UseSelectable = true;
            this.mtlOk.UseStyleColors = true;
            // 
            // mtlBack
            // 
            this.mtlBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mtlBack.Image = global::ERPFramework.Properties.Resources.CircledLeft32;
            this.mtlBack.ImageSize = 32;
            this.mtlBack.Location = new System.Drawing.Point(685, 3);
            this.mtlBack.Name = "mtlBack";
            this.mtlBack.NoFocusImage = global::ERPFramework.Properties.Resources.CircledLeft32g;
            this.mtlBack.Size = new System.Drawing.Size(33, 33);
            this.mtlBack.TabIndex = 8;
            this.mtlBack.UseSelectable = true;
            this.mtlBack.UseStyleColors = true;
            // 
            // metroFlowLayoutPanel2
            // 
            this.metroFlowLayoutPanel2.Controls.Add(this.metroLink1);
            this.metroFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.metroFlowLayoutPanel2.Location = new System.Drawing.Point(20, 383);
            this.metroFlowLayoutPanel2.Name = "metroFlowLayoutPanel2";
            this.metroFlowLayoutPanel2.Size = new System.Drawing.Size(760, 47);
            this.metroFlowLayoutPanel2.TabIndex = 1;
            // 
            // metroLink1
            // 
            this.metroLink1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.metroLink1.Image = global::ERPFramework.Properties.Resources.Print32;
            this.metroLink1.ImageSize = 32;
            this.metroLink1.Location = new System.Drawing.Point(724, 3);
            this.metroLink1.Name = "metroLink1";
            this.metroLink1.NoFocusImage = global::ERPFramework.Properties.Resources.Print32g;
            this.metroLink1.Size = new System.Drawing.Size(33, 33);
            this.metroLink1.TabIndex = 9;
            this.metroLink1.UseSelectable = true;
            this.metroLink1.UseStyleColors = true;
            // 
            // DataEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackLocation = MetroFramework.Forms.BackLocation.TopRight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.metroFlowLayoutPanel2);
            this.Controls.Add(this.metroFlowLayoutPanel1);
            this.DisplayHeader = false;
            this.Name = "DataEntryForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Text = "DataEntryForm";
            this.metroFlowLayoutPanel1.ResumeLayout(false);
            this.metroFlowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Extender.MetroFlowLayoutPanel metroFlowLayoutPanel1;
        private MetroFramework.Controls.MetroLink mtlOk;
        private MetroFramework.Controls.MetroLink mtlBack;
        private MetroFramework.Extender.MetroFlowLayoutPanel metroFlowLayoutPanel2;
        private MetroFramework.Controls.MetroLink metroLink1;
    }
}