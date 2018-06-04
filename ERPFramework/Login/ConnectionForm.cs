using System;
using System.ComponentModel;
using System.Windows.Forms;
using ERPFramework.Controls;
using ERPFramework.Data;

namespace ERPFramework.Login
{
    public partial class ConnectionForm : MetroFramework.Forms.MetroForm
    {
        #region Properties

        public ProviderType Provider { get; private set; }

        public string InitialCatalog { get; private set; }

        public string DataSource { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public AuthenticationMode DataBase_Authentication { get; private set; }

        public bool NewDatabase { get; private set; }

        #endregion

        private readonly EnumsManager<ProviderType> cbbManager;

        public ConnectionForm()
        {
            InitializeComponent();
            cbbManager = new EnumsManager<ProviderType>(cbbProvider, "", false, DisplayProvider);

            cbbAuthentication.SelectedIndex = 0;

            txtexistCMP.ReadOnly = !rdbExistCMP.Checked;
            txtnewCMP.ReadOnly = rdbExistCMP.Checked;
            cbbExistSQL.Enabled = rdbExistSQL.Checked;
            txtNewSQL.ReadOnly = rdbExistSQL.Checked;
        }

        private static bool DisplayProvider(ProviderType provider)
        {
            return ProxyProviderLoader.HasProvider(provider);
        }

        private void wizard1_BeforeSwitchPages(object sender, ERPFramework.Controls.Wizard.BeforeSwitchPagesEventArgs e)
        {
            if (e.OldIndex < e.NewIndex)
            {
                switch (e.OldIndex)
                {
                    case 0:

                        if (cbbManager.GetValue() == ProviderType.SQLServer)
                            e.NewIndex = 1;

                        if (cbbManager.GetValue() == ProviderType.SQLCompact)
                            e.NewIndex = 3;

                        if (cbbManager.GetValue() == ProviderType.SQLite)
                            e.NewIndex = 4;
                        break;

                    case 1:
                        if (e.NewIndex == 2)
                        {
                            e.Cancel = cbbServer.Text.IsEmpty();
                        }
                        break;
                    case 2:
                        if (e.NewIndex == 1)
                            e.NewIndex = 0;

                        //if (e.NewIndex == 3)
                        //    e.Cancel = CheckConnection();
                        break;

                    case 3:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (e.OldIndex)
                {
                    case 3:
                        e.NewIndex = 0;
                        break;

                    case 4:
                        e.NewIndex = 0;
                        break;
                    default:
                        break;

                        //case 4:
                        //    if (cbbProvider.SelectedIndex == 1)
                        //        e.NewIndex = 1;
                        //    break;
                }
            }
        }

        private void wizard1_AfterSwitchPages(object sender, ERPFramework.Controls.Wizard.AfterSwitchPagesEventArgs e)
        {
            if (e.NewIndex == 1 && e.OldIndex < e.NewIndex)
            {
                SearchSqlServer();
            }
            if (e.NewIndex == 2)
                ListDataBase();
        }

        private void rdbExistCMP_CheckedChanged(object sender, EventArgs e)
        {
            txtexistCMP.ReadOnly = !rdbExistCMP.Checked;
            txtnewCMP.ReadOnly = rdbExistCMP.Checked;
        }

        private void rdbExistSQL_CheckedChanged(object sender, EventArgs e)
        {
            cbbExistSQL.Enabled = rdbExistSQL.Checked;
            txtNewSQL.ReadOnly = rdbExistSQL.Checked;
        }

        private void cbbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUser.ReadOnly = cbbAuthentication.SelectedIndex == 0;
            txtPass.ReadOnly = cbbAuthentication.SelectedIndex == 0;
        }

        #region SqlServer Methods

        private async void SearchSqlServer()
        {
            this.cbbServer.Items.Clear();
            this.cbbServer.DropDownStyle = ComboBoxStyle.DropDown;
            this.cbbServer.Text = "Search SqlServer";
            Application.DoEvents();

            wizard1.NextEnabled = false;

            var serverList = await SqlProxyDatabaseHelper.GetServers();

            cbbServer.Items.AddRange(serverList.ToArray());

            if (this.cbbServer.Items.Count > 0)
            {
                this.cbbServer.SelectedIndex = 0;
                this.cbbServer.DropDownStyle = ComboBoxStyle.DropDown;
                wizard1.NextEnabled = true;
            }
            else
                this.cbbServer.Text = "<No available SQL Servers>";
        }

        private async void ListDataBase()
        {
            cbbExistSQL.Items.Clear();

            var serverList = await SqlProxyDatabaseHelper.ListDatabase(cbbServer.Text, txtUser.Text, txtPass.Text, cbbAuthentication.SelectedIndex == AuthenticationMode.Windows.Int());

            cbbExistSQL.Items.AddRange(serverList.ToArray());
        }

        #endregion

        private void wizard1_Finish(object sender, CancelEventArgs e)
        {
            Provider = (ProviderType)cbbManager.GetValue();
            switch (Provider)
            {
                case ProviderType.SQLCompact:
                    if (rdbExistCMP.Checked)
                        InitialCatalog = txtexistCMP.Text;
                    else
                        InitialCatalog = txtnewCMP.Text;

                    Password = txtpassCMP.Text;

                    NewDatabase = rdbNewCMP.Checked;
                    break;

                case ProviderType.SQLServer:
                    if (rdbExistSQL.Checked)
                        InitialCatalog = cbbExistSQL.Text;
                    else
                        InitialCatalog = txtNewSQL.Text;

                    DataBase_Authentication = (AuthenticationMode)cbbAuthentication.SelectedIndex;

                    DataSource = cbbServer.Text;
                    Username = txtUser.Text;
                    Password = txtPass.Text;

                    NewDatabase = rdbNewSQL.Checked;
                    break;
                case ProviderType.SQLite:
                    if (rdbexistLIT.Checked)
                        InitialCatalog = txtexistLIT.Text;
                    else
                        InitialCatalog = txtnewLIT.Text;

                    Password = txtPassLIT.Text;
                    NewDatabase = rdbnewLIT.Checked;
                    break;
                default:
                    break;
            }
        }

        private void rdbExistLIT_CheckedChanged(object sender, EventArgs e)
        {
            txtexistLIT.ReadOnly = rdbnewLIT.Checked;
            txtnewLIT.ReadOnly = !rdbnewLIT.Checked;
        }
    }
}