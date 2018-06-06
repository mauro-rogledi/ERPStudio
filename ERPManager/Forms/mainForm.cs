using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using ERPFramework;
using ERPFramework.Controls;
using ERPFramework.Data;
using ERPFramework.Libraries;
using ERPFramework.Login;
using ERPFramework.ModulesHelper;
using ERPFramework.Preferences;
using ERPManager.Library;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace ERPManager.Forms
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class frmMain : MetroFramework.Forms.MetroForm
    {
        ThreadHelper _thread = null;
        public frmMain()
        {
            InitializeComponent();
            InitializeAuxComponent();
        }

        private void InitializeAuxComponent()
        {
            GlobalInfo.MainForm = this;
            GlobalInfo.StyleManager = metroStyleManager;
            GlobalInfo.CurrentDate = DateTime.Today;

            System.Globalization.CultureInfo cInfo = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            cInfo.DateTimeFormat.ShortDatePattern = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            System.Threading.Thread.CurrentThread.CurrentCulture = cInfo;
            userControlHelper.AddControl(menuControl);
            userControlHelper.AddControl(btnCalendar);
            userControlHelper.AddControl(btnSetting);
            userControlHelper.AddControl(btnExit);

            OpenDocument.ShowObject += OpenDocument_ShowObject;
        }

        private void OpenDocument_ShowObject(object sender, Tuple<object, bool> e)
        {
            if (e.Item1 is MetroUserControl)
                menuControl.Show(e.Item1);
            else
                userControlHelper.ShowControl(e.Item1 as Control, e.Item2, OpenControlHelper.ControlPosition.Center, this as Control);
        }

        //private void OpenDocument_ShowObject1(object sender, object e)
        //{
        //    if (e is MetroUserControl)
        //        menuControl.Show(e);
        //    else
        //        userControlHelper.ShowControl(e as Control, true, OpenControlHelper.ControlPosition.Center, this as Control);
        //}

        protected override void OnLoad(EventArgs e)
        {
            var configFound = ActivationManager.Load();

            // Mi connetto al Database
            GlobalInfo.DBaseInfo.dbManager = new SqlManager();
            if (!GlobalInfo.DBaseInfo.dbManager.CreateConnection())
            {
                this.Close();
                return;
            }

            GlobalInfo.globalPref = new PreferencesManager<GlobalPreferences>("", null).ReadPreference();
            if (GlobalInfo.globalPref.ForceLanguage)
            {
                var lang = Enum.GetName(typeof(Languages), GlobalInfo.globalPref.Language).Replace('_', '-');
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang, false);
            }
            ControlBox = GlobalInfo.globalPref.ShowControlBox;

            metroStyleManager.Style = GlobalInfo.globalPref.CustmColor;
            StyleManager.Style = GlobalInfo.globalPref.CustmColor;
            GlobalInfo.StyleManager.Style = GlobalInfo.globalPref.CustmColor;
            metroStyleManager.Update();

            this.StyleManager.Clone(menuControl);
            metroStyleManager.Update();

            // Carico l'attivazione
            if (!configFound)
            {
                if (!ShowRegistrationForm())
                    this.Close();
                return;
            }

            // Faccio la login
            // Controllo se devo saltarla
            using (var login = new loginForm())
            {
                this.StyleManager.Clone(login);
                if (!GlobalInfo.LoginInfo.RememberLastLogin || !login.CheckUser(GlobalInfo.LoginInfo.LastUser, GlobalInfo.LoginInfo.LastPassword))
                {
                    if (login.ShowDialog() != DialogResult.OK)
                    {
                        this.Close();
                        return;
                    }
                }
            }
            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!menuControl.CanClose())
            {
                MetroMessageBox.Show(this, Properties.Resources.Msg_CantClose, Properties.Resources.Msg_Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            // Controllo che il programma sia correttamente registrato
            // Aggancio il menu


            // Carica i moduli
            if (!ModuleManager.LoadModules())
                Application.Exit();

            this.StyleManager.Clone(menuControl);
            metroStyleManager.Update();
            menuControl.AddModule(ModuleManager.ModuleList);
            //menuControl.UpdateStyle += MenuControl_UpdateStyle;

            this.Text = ActivationManager.ApplicationName;
            this.Update();


            if (GlobalInfo.UserInfo.userType != UserType.Administrator)
            {
                //tsmUsers.Visible = false;
            }

            // AutoUpdater
            #if (!DEBUG)
            Version vrs = Assembly.GetEntryAssembly().GetName().Version;
            _thread = new ThreadHelper();
            _thread.Method = CheckNewVersion.CheckVersion;
            CheckNewVersion.OpenForm += new EventHandler(CheckNewVersion_OpenForm);
            CheckNewVersion.GiveMessage += new EventHandler(CheckNewVersion_GiveMessage);
            _thread.Execute(new CheckNewVersion.parameters() { ApplicationName = ActivationManager.ApplicationName, Version = vrs, Verbose = false });
            #endif
            base.OnShown(e);
        }

        private bool ShowRegistrationForm()
        {
            using (var rF = new registerForm(ActivationManager.ApplicationName))
            {
                if (rF.ShowDialog() == DialogResult.Cancel)
                    return false;
            }

            return true;
        }

        //private void MenuControl_UpdateStyle(object sender, EventArgs e)
        //{
        //    metroStyleManager.Update();
        //}

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }
        private void menuItem_Click(object sender, EventArgs e)
        {
            ((FormToolStripMenuItem)sender).Form.Activate();
        }

        private void tsmInfo_Click(object sender, EventArgs e)
        {
            InfoForm about = new InfoForm();
            about.ShowDialog();
        }

        private PreferencePanel[] GetPreferencePanel(ApplicationMenuModule amm)
        {
            string module = amm.Namespace.Module;
            amm.Namespace.Application = "ModuleData.RegisterModule";
            RegisterModule registerTable = (RegisterModule)DllManager.CreateIstance(amm.Namespace, null);
            if (registerTable != null)
            {
                return registerTable.RegisterPreferences();
            }

            return null;
        }



        private void tsmNewVersion_Click(object sender, EventArgs e)
        {
            Version vrs = Assembly.GetEntryAssembly().GetName().Version;
            _thread = new ThreadHelper();
            NotifyForm nf = new NotifyForm();
            _thread.Method = CheckNewVersion.CheckVersion;
            CheckNewVersion.GiveMessage += new EventHandler(CheckNewVersion_GiveMessage);
            _thread.Execute(new CheckNewVersion.parameters { ApplicationName = ActivationManager.ApplicationName, Version = vrs, Verbose = true });
        }

        void CheckNewVersion_OpenForm(object sender, EventArgs e)
        {
            //NotifyForm nf = new NotifyForm();
            //MetroTaskWindow.ShowTaskWindow("Nuova", nf);
            //_thread.CancelAsync();
            //Application.DoEvents();

            pInfoTxt.Invoke(
                new MethodInvoker(() =>
                {
                    pInfoTxt.Text = Properties.Resources.Msg_NewVersion;
                    pInfoImg.Image = Properties.Resources.New24;
                    pnlInfo.Visible = true;

                }));
        }

        void CheckNewVersion_GiveMessage(object sender, EventArgs e)
        {
            _thread.CancelAsync();

        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            userControlHelper.ShowControl(new dateForm(), true, OpenControlHelper.ControlPosition.Owner, sender as Control);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            if (!menuControl.CanClose())
            {
                MetroMessageBox.Show(this, Properties.Resources.Msg_CantClose, Properties.Resources.Msg_Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Application.Exit();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            settingForm sf = userControlHelper.ShowControl(new settingForm(), true, OpenControlHelper.ControlPosition.Owner, sender as Control) as settingForm;
            sf.Open += Sf_Open;
            sf.ButtonClick += Sf_ButtonClick;
        }

        private void Sf_ButtonClick(object sender, settingForm.SettingButton button)
        {
            switch (button)
            {
                case settingForm.SettingButton.Register:
                    var rF = new registerForm(ActivationManager.ApplicationName);
                    userControlHelper.ShowControl(rF, true, OpenControlHelper.ControlPosition.Owner, sender as Control);
                    break;
                case settingForm.SettingButton.LastUser:
                    DontRememberUser();
                    break;
                case settingForm.SettingButton.Info:
                    var iF = new InfoForm();
                    userControlHelper.ShowControl(iF, true, OpenControlHelper.ControlPosition.Owner, sender as Control);
                    break;
                default:
                    break;
            }
        }

        private void Sf_Open(object sender, ApplicationMenuItem e)
        {
            OpenDocument.Show(e.Namespace);
            (sender as Form).Close();
        }

        private void DontRememberUser()
        {
            GlobalInfo.LoginInfo.RememberLastLogin = false;
            GlobalInfo.DBaseInfo.dbManager.WriteConfigFile();
            MetroMessageBox.Show(this, Properties.Resources.Msg_UserNotRemember,
                                Properties.Resources.Msg_Attention,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuControl_OpenFastDocument(object sender, MetroForm frm)
        {
            userControlHelper.ShowControl(frm, true, OpenControlHelper.ControlPosition.Center, this as Control);
        }
    }
}