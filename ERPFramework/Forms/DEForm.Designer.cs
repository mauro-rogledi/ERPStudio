namespace ERPFramework.Forms
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
            this.mfpTop = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.mfpTopPanel = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.btnEdit = new MetroFramework.Controls.MetroLink();
            this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
            this.mfpBottomRight = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.btnSave = new MetroFramework.Controls.MetroLink();
            this.btnUndo = new MetroFramework.Controls.MetroLink();
            this.mfpBottomCenter = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.btnPrint = new MetroFramework.Controls.MetroLink();
            this.mfpBottomLeft = new MetroFramework.Extender.MetroFlowLayoutPanel();
            this.mfpTop.SuspendLayout();
            this.mfpTopPanel.SuspendLayout();
            this.tlpBottom.SuspendLayout();
            this.mfpBottomRight.SuspendLayout();
            this.mfpBottomCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // mfpTop
            // 
            resources.ApplyResources(this.mfpTop, "mfpTop");
            this.mfpTop.Controls.Add(this.mfpTopPanel);
            this.mfpTop.Name = "mfpTop";
            // 
            // mfpTopPanel
            // 
            resources.ApplyResources(this.mfpTopPanel, "mfpTopPanel");
            this.mfpTopPanel.Controls.Add(this.btnEdit);
            this.mfpTopPanel.Name = "mfpTopPanel";
            // 
            // btnEdit
            // 
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEdit.Image = global::ERPFramework.Properties.Resources.Edit32;
            this.btnEdit.ImageSize = 32;
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NoFocusImage = global::ERPFramework.Properties.Resources.Edit32g;
            this.btnEdit.UseSelectable = true;
            this.btnEdit.UseStyleColors = true;
            // 
            // tlpBottom
            // 
            resources.ApplyResources(this.tlpBottom, "tlpBottom");
            this.tlpBottom.Controls.Add(this.mfpBottomRight, 2, 0);
            this.tlpBottom.Controls.Add(this.mfpBottomCenter, 1, 0);
            this.tlpBottom.Controls.Add(this.mfpBottomLeft, 0, 0);
            this.tlpBottom.Name = "tlpBottom";
            // 
            // mfpBottomRight
            // 
            resources.ApplyResources(this.mfpBottomRight, "mfpBottomRight");
            this.mfpBottomRight.Controls.Add(this.btnSave);
            this.mfpBottomRight.Controls.Add(this.btnUndo);
            this.mfpBottomRight.Name = "mfpBottomRight";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::ERPFramework.Properties.Resources.Save32;
            this.btnSave.ImageSize = 32;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.NoFocusImage = global::ERPFramework.Properties.Resources.Save32g;
            this.btnSave.UseSelectable = true;
            this.btnSave.UseStyleColors = true;
            // 
            // btnUndo
            // 
            this.btnUndo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUndo.Image = global::ERPFramework.Properties.Resources.Undo32;
            this.btnUndo.ImageSize = 32;
            resources.ApplyResources(this.btnUndo, "btnUndo");
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.NoFocusImage = global::ERPFramework.Properties.Resources.Undo32g;
            this.btnUndo.UseSelectable = true;
            this.btnUndo.UseStyleColors = true;
            // 
            // mfpBottomCenter
            // 
            this.mfpBottomCenter.Controls.Add(this.btnPrint);
            resources.ApplyResources(this.mfpBottomCenter, "mfpBottomCenter");
            this.mfpBottomCenter.Name = "mfpBottomCenter";
            // 
            // btnPrint
            // 
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.Image = global::ERPFramework.Properties.Resources.Print32;
            this.btnPrint.ImageSize = 32;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.NoFocusImage = global::ERPFramework.Properties.Resources.Print32g;
            this.btnPrint.UseSelectable = true;
            this.btnPrint.UseStyleColors = true;
            // 
            // mfpBottomLeft
            // 
            resources.ApplyResources(this.mfpBottomLeft, "mfpBottomLeft");
            this.mfpBottomLeft.Name = "mfpBottomLeft";
            // 
            // DataEntryForm
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackLocation = MetroFramework.Forms.BackLocation.TopRight;
            this.Controls.Add(this.tlpBottom);
            this.Controls.Add(this.mfpTop);
            this.DisplayHeader = false;
            this.Name = "DataEntryForm";
            this.mfpTop.ResumeLayout(false);
            this.mfpTop.PerformLayout();
            this.mfpTopPanel.ResumeLayout(false);
            this.tlpBottom.ResumeLayout(false);
            this.tlpBottom.PerformLayout();
            this.mfpBottomRight.ResumeLayout(false);
            this.mfpBottomCenter.ResumeLayout(false);
            this.mfpBottomCenter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected internal MetroFramework.Extender.MetroFlowLayoutPanel mfpTop;
        protected internal MetroFramework.Extender.MetroFlowLayoutPanel mfpTopPanel;
        private MetroFramework.Controls.MetroLink btnEdit;
        private System.Windows.Forms.TableLayoutPanel tlpBottom;
        protected internal MetroFramework.Extender.MetroFlowLayoutPanel mfpBottomRight;
        private MetroFramework.Controls.MetroLink btnSave;
        private MetroFramework.Controls.MetroLink btnUndo;
        private MetroFramework.Extender.MetroFlowLayoutPanel mfpBottomLeft;
        private MetroFramework.Extender.MetroFlowLayoutPanel mfpBottomCenter;
        private MetroFramework.Controls.MetroLink btnPrint;
    }
}