using ERPFramework.Controls;
namespace ERPFramework.CounterManager
{
    partial class RadarCodesCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RadarCodesCtrl));
            this.btnLens = new System.Windows.Forms.Button();
            this.btnButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLens
            // 
            this.btnLens.Font = new System.Drawing.Font("Webdings", 8.25F);
            this.btnLens.Image = global::ERPFramework.Properties.Resources.Search24;
            this.btnLens.Location = new System.Drawing.Point(131, 0);
            this.btnLens.Name = "btnLens";
            this.btnLens.Size = new System.Drawing.Size(22, 20);
            this.btnLens.TabIndex = 1;
            this.btnLens.TabStop = false;
            this.btnLens.UseVisualStyleBackColor = true;
            this.btnLens.Visible = false;
            this.btnLens.Click += new System.EventHandler(this.btnLens_Click);
            // 
            // btnButton
            // 
            this.btnButton.Image = ((System.Drawing.Image)(resources.GetObject("btnButton.Image")));
            this.btnButton.Location = new System.Drawing.Point(109, 0);
            this.btnButton.Name = "btnButton";
            this.btnButton.Size = new System.Drawing.Size(22, 20);
            this.btnButton.TabIndex = 2;
            this.btnButton.TabStop = false;
            this.btnButton.UseVisualStyleBackColor = true;
            this.btnButton.Visible = false;
            this.btnButton.Click += new System.EventHandler(this.btnButton_Click);
            // 
            // RadarCodesCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnButton);
            this.Controls.Add(this.btnLens);
            this.Name = "RadarCodesCtrl";
            this.Size = new System.Drawing.Size(153, 37);
            this.Resize += new System.EventHandler(this.RadarTextBox_Resize);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.RadarTextBox_Validating);
            this.Validated += new System.EventHandler(this.RadarTextBox_Validated);
            this.Controls.SetChildIndex(this.btnLens, 0);
            this.Controls.SetChildIndex(this.btnButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button btnLens;
        private System.Windows.Forms.Button btnButton;
    }
}
