namespace ERPFramework.Preferences
{
    partial class GenericPreferencePanel<T>
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
            this.components = new System.ComponentModel.Container();
            this.ButtonComputer.Click += ButtonComputer_Click;
            this.ButtonUser.Click += ButtonUser_Click;
            this.ButtonApplication.Click += ButtonApplication_Click;
        }

        private void ButtonApplication_Click(object sender, System.EventArgs e)
        {
            if (IsNew)
                OnToolStripButtonClick(ButtonClicked.Application);
        }

        private void ButtonUser_Click(object sender, System.EventArgs e)
        {
            if (IsNew)
                OnToolStripButtonClick(ButtonClicked.User);
        }

        private void ButtonComputer_Click(object sender, System.EventArgs e)
        {
            if (IsNew)
                OnToolStripButtonClick(ButtonClicked.Computer);
        }

        #endregion
    }
}
