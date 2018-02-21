namespace ERPManager.Forms
{
    partial class settingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(settingForm));
            this.btnInfo = new MetroFramework.Controls.MetroTile();
            this.btnUser = new MetroFramework.Controls.MetroTile();
            this.btnRegister = new MetroFramework.Controls.MetroTile();
            this.btnCounter = new MetroFramework.Controls.MetroTile();
            this.btnCode = new MetroFramework.Controls.MetroTile();
            this.btnLastUser = new MetroFramework.Controls.MetroTile();
            this.btnPreferences = new MetroFramework.Controls.MetroTile();
            this.btnExport = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // btnInfo
            // 
            this.btnInfo.ActiveControl = null;
            resources.ApplyResources(this.btnInfo, "btnInfo");
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.TileImage = global::ERPManager.Properties.Resources.Info24;
            this.btnInfo.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInfo.UseSelectable = true;
            this.btnInfo.UseStyleColors = true;
            this.btnInfo.UseTileImage = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnUser
            // 
            this.btnUser.ActiveControl = null;
            resources.ApplyResources(this.btnUser, "btnUser");
            this.btnUser.Name = "btnUser";
            this.btnUser.TileImage = global::ERPManager.Properties.Resources.AddUser24;
            this.btnUser.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUser.UseSelectable = true;
            this.btnUser.UseStyleColors = true;
            this.btnUser.UseTileImage = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.ActiveControl = null;
            resources.ApplyResources(this.btnRegister, "btnRegister");
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.TileImage = global::ERPManager.Properties.Resources.Key24;
            this.btnRegister.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRegister.UseSelectable = true;
            this.btnRegister.UseStyleColors = true;
            this.btnRegister.UseTileImage = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnCounter
            // 
            this.btnCounter.ActiveControl = null;
            resources.ApplyResources(this.btnCounter, "btnCounter");
            this.btnCounter.Name = "btnCounter";
            this.btnCounter.TileImage = global::ERPManager.Properties.Resources.Counter24;
            this.btnCounter.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCounter.UseSelectable = true;
            this.btnCounter.UseStyleColors = true;
            this.btnCounter.UseTileImage = true;
            this.btnCounter.Click += new System.EventHandler(this.btnCounter_Click);
            // 
            // btnCode
            // 
            this.btnCode.ActiveControl = null;
            resources.ApplyResources(this.btnCode, "btnCode");
            this.btnCode.Name = "btnCode";
            this.btnCode.TileImage = global::ERPManager.Properties.Resources.Code24;
            this.btnCode.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCode.UseSelectable = true;
            this.btnCode.UseStyleColors = true;
            this.btnCode.UseTileImage = true;
            this.btnCode.Click += new System.EventHandler(this.btnCode_Click);
            // 
            // btnLastUser
            // 
            this.btnLastUser.ActiveControl = null;
            resources.ApplyResources(this.btnLastUser, "btnLastUser");
            this.btnLastUser.Name = "btnLastUser";
            this.btnLastUser.TileImage = global::ERPManager.Properties.Resources.LastUser24;
            this.btnLastUser.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLastUser.UseSelectable = true;
            this.btnLastUser.UseStyleColors = true;
            this.btnLastUser.UseTileImage = true;
            this.btnLastUser.Click += new System.EventHandler(this.btnLastUser_Click);
            // 
            // btnPreferences
            // 
            this.btnPreferences.ActiveControl = null;
            resources.ApplyResources(this.btnPreferences, "btnPreferences");
            this.btnPreferences.Name = "btnPreferences";
            this.btnPreferences.TileImage = global::ERPManager.Properties.Resources.Info24;
            this.btnPreferences.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPreferences.UseSelectable = true;
            this.btnPreferences.UseStyleColors = true;
            this.btnPreferences.UseTileImage = true;
            this.btnPreferences.Click += new System.EventHandler(this.btnPreferences_Click);
            // 
            // btnExport
            // 
            this.btnExport.ActiveControl = null;
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.TileImage = global::ERPManager.Properties.Resources.Export24;
            this.btnExport.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExport.UseSelectable = true;
            this.btnExport.UseStyleColors = true;
            this.btnExport.UseTileImage = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // settingForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnLastUser);
            this.Controls.Add(this.btnCode);
            this.Controls.Add(this.btnCounter);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.btnPreferences);
            this.Controls.Add(this.btnInfo);
            this.Name = "settingForm";
            this.Resizable = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Deactivate += new System.EventHandler(this.settingForm_Deactivate);
            this.Leave += new System.EventHandler(this.settingForm_Leave);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTile btnInfo;
        private MetroFramework.Controls.MetroTile btnUser;
        private MetroFramework.Controls.MetroTile btnRegister;
        private MetroFramework.Controls.MetroTile btnCounter;
        private MetroFramework.Controls.MetroTile btnCode;
        private MetroFramework.Controls.MetroTile btnLastUser;
        private MetroFramework.Controls.MetroTile btnPreferences;
        private MetroFramework.Controls.MetroTile btnExport;
    }
}