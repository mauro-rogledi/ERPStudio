namespace ERPFramework.Controls
{
    partial class ListComboBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListComboBox));
            this.btnButton = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnButton
            // 
            this.btnButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnButton.Image = ((System.Drawing.Image)(resources.GetObject("btnButton.Image")));
            this.btnButton.Location = new System.Drawing.Point(128, 0);
            this.btnButton.Name = "btnButton";
            this.btnButton.Size = new System.Drawing.Size(22, 20);
            this.btnButton.TabIndex = 0;
            this.btnButton.TabStop = false;
            this.btnButton.UseVisualStyleBackColor = true;
            this.btnButton.Click += new System.EventHandler(this.btnButton_Click);
            this.btnButton.Enter += new System.EventHandler(this.btnButton_Enter);
            // 
            // txtText
            // 
            this.txtText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtText.Location = new System.Drawing.Point(0, 0);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(128, 20);
            this.txtText.TabIndex = 1;
            this.txtText.TabStop = false;
            this.txtText.TextChanged += new System.EventHandler(this.txtText_TextChanged);
            // 
            // ListComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.btnButton);
            this.Name = "ListComboBox";
            this.Size = new System.Drawing.Size(150, 20);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.LookUpComboBox_Validating);
            this.Validated += new System.EventHandler(this.LookUpComboBox_Validated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnButton;
        private System.Windows.Forms.TextBox txtText;
    }
}
