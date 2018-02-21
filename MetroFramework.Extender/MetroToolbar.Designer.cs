using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroFramework.Extender
{
    partial class MetroToolbar
    {
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnExecute = new MetroFramework.Extender.MetroToolbarButton();
            this.btnNew = new MetroFramework.Extender.MetroToolbarButton();
            this.btnEdit = new MetroFramework.Extender.MetroToolbarButton();
            this.btnSave = new MetroFramework.Extender.MetroToolbarButton();
            this.btnSearch = new MetroFramework.Extender.MetroToolbarButton();
            this.btnFilter = new MetroFramework.Extender.MetroToolbarButton();
            this.btnPreview = new MetroFramework.Extender.MetroToolbarButton();
            this.btnPrint = new MetroFramework.Extender.MetroToolbarButton();
            this.btnPref = new MetroFramework.Extender.MetroToolbarButton();
            this.btnExit = new MetroFramework.Extender.MetroToolbarButton();
            this.btnDelete = new MetroFramework.Extender.MetroToolbarButton();
            this.btnUndo = new MetroFramework.Extender.MetroToolbarButton();
            this.btnAddon = new MetroFramework.Extender.MetroToolbarButton();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.AutoSize = true;
            this.btnExecute.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnExecute.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Execute;
            this.btnExecute.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnExecute.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnExecute.Image = global::MetroFramework.Extender.Properties.Resources.More32;
            this.btnExecute.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExecute.ImageSize = 32;
            this.btnExecute.Location = new System.Drawing.Point(0, 0);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(6);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.More32g;
            this.btnExecute.Size = new System.Drawing.Size(34, 34);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.TabStop = false;
            this.btnExecute.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExecute.UseSelectable = true;
            this.btnExecute.UseStyleColors = true;
            this.btnExecute.Visible = false;
            this.btnExecute.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = true;
            this.btnNew.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnNew.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.New;
            this.btnNew.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnNew.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnNew.Image = global::MetroFramework.Extender.Properties.Resources.New32;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNew.ImageSize = 32;
            this.btnNew.Location = new System.Drawing.Point(0, 0);
            this.btnNew.Margin = new System.Windows.Forms.Padding(6);
            this.btnNew.Name = "btnNew";
            this.btnNew.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.New32g;
            this.btnNew.Size = new System.Drawing.Size(34, 34);
            this.btnNew.TabIndex = 0;
            this.btnNew.TabStop = false;
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNew.UseSelectable = true;
            this.btnNew.UseStyleColors = true;
            this.btnNew.Visible = false;
            this.btnNew.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnEdit.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Edit;
            this.btnEdit.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnEdit.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnEdit.Image = global::MetroFramework.Extender.Properties.Resources.Edit32;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEdit.ImageSize = 32;
            this.btnEdit.Location = new System.Drawing.Point(0, 0);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Edit32g;
            this.btnEdit.Size = new System.Drawing.Size(34, 34);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.TabStop = false;
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEdit.UseSelectable = true;
            this.btnEdit.UseStyleColors = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnSave.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Save;
            this.btnSave.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnSave.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnSave.Image = global::MetroFramework.Extender.Properties.Resources.Save32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.ImageSize = 32;
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6);
            this.btnSave.Name = "btnSave";
            this.btnSave.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Save32g;
            this.btnSave.Size = new System.Drawing.Size(34, 34);
            this.btnSave.TabIndex = 0;
            this.btnSave.TabStop = false;
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.UseSelectable = true;
            this.btnSave.UseStyleColors = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnSearch.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Search;
            this.btnSearch.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnSearch.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnSearch.Image = global::MetroFramework.Extender.Properties.Resources.Search32;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSearch.ImageSize = 32;
            this.btnSearch.Location = new System.Drawing.Point(0, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Search32g;
            this.btnSearch.Size = new System.Drawing.Size(34, 34);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.TabStop = false;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSearch.UseSelectable = true;
            this.btnSearch.UseStyleColors = true;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.AutoSize = true;
            this.btnFilter.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnFilter.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Find;
            this.btnFilter.DisplayFocus = true;
            this.btnFilter.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnFilter.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnFilter.Image = global::MetroFramework.Extender.Properties.Resources.Filter32;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFilter.ImageSize = 32;
            this.btnFilter.Location = new System.Drawing.Point(0, 0);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(6);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Filter32g;
            this.btnFilter.Size = new System.Drawing.Size(34, 34);
            this.btnFilter.TabIndex = 0;
            this.btnFilter.TabStop = false;
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFilter.UseSelectable = true;
            this.btnFilter.UseStyleColors = true;
            this.btnFilter.Visible = false;
            this.btnFilter.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.AutoSize = true;
            this.btnPreview.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnPreview.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Preview;
            this.btnPreview.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnPreview.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnPreview.Image = global::MetroFramework.Extender.Properties.Resources.Preview32;
            this.btnPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPreview.ImageSize = 32;
            this.btnPreview.Location = new System.Drawing.Point(0, 0);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(6);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Preview32g;
            this.btnPreview.Size = new System.Drawing.Size(34, 34);
            this.btnPreview.TabIndex = 0;
            this.btnPreview.TabStop = false;
            this.btnPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPreview.UseSelectable = true;
            this.btnPreview.UseStyleColors = true;
            this.btnPreview.Visible = false;
            this.btnPreview.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AutoSize = true;
            this.btnPrint.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnPrint.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Print;
            this.btnPrint.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnPrint.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnPrint.Image = global::MetroFramework.Extender.Properties.Resources.Print32;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrint.ImageSize = 32;
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Print32g;
            this.btnPrint.Size = new System.Drawing.Size(34, 34);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.TabStop = false;
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrint.UseSelectable = true;
            this.btnPrint.UseStyleColors = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnPref
            // 
            this.btnPref.AutoSize = true;
            this.btnPref.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnPref.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Preference;
            this.btnPref.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnPref.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnPref.Image = global::MetroFramework.Extender.Properties.Resources.Setting32;
            this.btnPref.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPref.ImageSize = 32;
            this.btnPref.Location = new System.Drawing.Point(0, 0);
            this.btnPref.Margin = new System.Windows.Forms.Padding(6);
            this.btnPref.Name = "btnPref";
            this.btnPref.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Setting32g;
            this.btnPref.Size = new System.Drawing.Size(34, 34);
            this.btnPref.TabIndex = 0;
            this.btnPref.TabStop = false;
            this.btnPref.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPref.UseSelectable = true;
            this.btnPref.UseStyleColors = true;
            this.btnPref.Visible = false;
            this.btnPref.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnExit.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Exit;
            this.btnExit.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnExit.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnExit.Image = global::MetroFramework.Extender.Properties.Resources.Exit32;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit.ImageSize = 32;
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(6);
            this.btnExit.Name = "btnExit";
            this.btnExit.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Exit32g;
            this.btnExit.Size = new System.Drawing.Size(34, 34);
            this.btnExit.TabIndex = 0;
            this.btnExit.TabStop = false;
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.UseSelectable = true;
            this.btnExit.UseStyleColors = true;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnDelete.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Delete;
            this.btnDelete.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnDelete.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnDelete.Image = global::MetroFramework.Extender.Properties.Resources.Delete32;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.ImageSize = 32;
            this.btnDelete.Location = new System.Drawing.Point(0, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Delete32g;
            this.btnDelete.Size = new System.Drawing.Size(34, 34);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.TabStop = false;
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.UseSelectable = true;
            this.btnDelete.UseStyleColors = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.AutoSize = true;
            this.btnUndo.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnUndo.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.Undo;
            this.btnUndo.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnUndo.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnUndo.Image = global::MetroFramework.Extender.Properties.Resources.Undo32;
            this.btnUndo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUndo.ImageSize = 32;
            this.btnUndo.Location = new System.Drawing.Point(0, 0);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(6);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.Undo32g;
            this.btnUndo.Size = new System.Drawing.Size(34, 34);
            this.btnUndo.TabIndex = 0;
            this.btnUndo.TabStop = false;
            this.btnUndo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUndo.UseSelectable = true;
            this.btnUndo.UseStyleColors = true;
            this.btnUndo.Visible = false;
            this.btnUndo.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnAddon
            // 
            this.btnAddon.AutoSize = true;
            this.btnAddon.ButtonDock = MetroFramework.Extender.MetroToolbarButtonDock.Left;
            this.btnAddon.ButtonType = MetroFramework.Extender.MetroToolbarButtonType.AddOn;
            this.btnAddon.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.btnAddon.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.btnAddon.Image = global::MetroFramework.Extender.Properties.Resources.More32;
            this.btnAddon.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddon.ImageSize = 32;
            this.btnAddon.Location = new System.Drawing.Point(0, 0);
            this.btnAddon.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddon.Name = "btnAddon";
            this.btnAddon.NoFocusImage = global::MetroFramework.Extender.Properties.Resources.More32g;
            this.btnAddon.Size = new System.Drawing.Size(34, 34);
            this.btnAddon.TabIndex = 0;
            this.btnAddon.TabStop = false;
            this.btnAddon.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddon.UseSelectable = true;
            this.btnAddon.UseStyleColors = true;
            this.btnAddon.Visible = false;
            this.btnAddon.Click += new System.EventHandler(this.btn_Click);
            // 
            // MetroToolbar
            // 
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPref);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnAddon);
            this.Size = new System.Drawing.Size(900, 53);
            this.UseStyleColors = true;
            this.VerticalScrollbarBarColor = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private MetroToolbarButton btnNew;
        private MetroToolbarButton btnEdit;
        private MetroToolbarButton btnSave;
        private MetroToolbarButton btnSearch;
        private MetroToolbarButton btnFilter;
        private MetroToolbarButton btnPreview;
        private MetroToolbarButton btnPrint;
        private MetroToolbarButton btnPref;
        private MetroToolbarButton btnExit;
        private MetroToolbarButton btnDelete;
        private MetroToolbarButton btnAddon;
        private MetroToolbarButton btnExecute;
        private MetroToolbarButton btnUndo;
    }
}
