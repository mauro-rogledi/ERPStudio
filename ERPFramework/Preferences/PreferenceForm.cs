using System;
using System.Drawing;
using ERPFramework.Data;
using MetroFramework;

namespace ERPFramework.Preferences
{
    public partial class PreferenceForm : MetroFramework.Forms.MetroForm
    {
        #region Private

        private string formName = string.Empty;

        protected string ConnectionString { get; private set; }

        public ProviderType providerType { get; private set; }

        #endregion

        //[Category(ErpFrameworkDefaults.PropertyCategory.Appearance)]
        //public string Title { get; set; }

        //[Category(ErpFrameworkDefaults.PropertyCategory.Event)]
        //public event EventHandler Exit;

        public PreferenceForm()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (GlobalInfo.StyleManager != null)
            {
                if (GlobalInfo.StyleManager.Theme == MetroThemeStyle.Dark)
                    preferenceContainer1.Panel1.BackColor = Color.FromArgb(11, 11, 11);
                else
                    preferenceContainer1.Panel1.BackColor = Color.FromArgb(244, 244, 244);
            }
        }

        public PreferenceForm(string formname)
        {
            ConnectionString = GlobalInfo.DBaseInfo.dbManager.DB_ConnectionString;
            this.providerType = GlobalInfo.LoginInfo.ProviderType;
            formName = formname;
            InitializeComponent();
        }

        public void AddPanel(PreferencePanel prefPanel)
        {
            if (preferenceContainer1 != null)
                preferenceContainer1.AddPanel(prefPanel);
        }

        public void AddPanel(PreferencePanel[] prefPanel)
        {
            if (prefPanel == null)
                return;

            for (int t = 0; t < prefPanel.Length; t++)
                if (prefPanel[t] != null)
                    preferenceContainer1.AddPanel(prefPanel[t]);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}