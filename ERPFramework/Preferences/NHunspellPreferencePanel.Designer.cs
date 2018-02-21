namespace ERPFramework.Preferences
{
    partial class NHUnspellPreferencePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NHUnspellPreferencePanel));
            this.cbbLanguages = new System.Windows.Forms.ComboBox();
            this.ckbEnable = new ERPFramework.Controls.DBCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbbLanguages
            // 
            resources.ApplyResources(this.cbbLanguages, "cbbLanguages");
            this.cbbLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLanguages.FormattingEnabled = true;
            this.cbbLanguages.Name = "cbbLanguages";
            // 
            // ckbEnable
            // 
            resources.ApplyResources(this.ckbEnable, "ckbEnable");
            this.ckbEnable.DBChecked = ((byte)(0));
            this.ckbEnable.Name = "ckbEnable";
            this.ckbEnable.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // NHUnspellPreferencePanel
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckbEnable);
            this.Controls.Add(this.cbbLanguages);
            this.Name = "NHUnspellPreferencePanel";
            this.Controls.SetChildIndex(this.cbbLanguages, 0);
            this.Controls.SetChildIndex(this.ckbEnable, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ComboBox cbbLanguages;
        private ERPFramework.Controls.DBCheckBox ckbEnable;
        private System.Windows.Forms.Label label1;


    }
}
