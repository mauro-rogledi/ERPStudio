using System;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Windows.Forms;
using ERPFramework.Controls;
using ERPFramework.Libraries;
using Microsoft.SqlServer.Management.Smo;

namespace ERPFramework.Login
{
    public partial class ConnectionForm : MetroFramework.Forms.MetroForm
    {
        #region Properties

        public ProviderType DataBase_Provider { get; private set; }

        public string DataBase_Name { get; private set; }

        public string DataBase_Host { get; private set; }

        public string DataBase_Username { get; private set; }

        public string DataBase_Password { get; private set; }

        public AuthenticationMode DataBase_Authentication { get; private set; }

        public bool DataBase_NewDatabase { get; private set; }

        #endregion

        private EnumsManager<ProviderType> cbbManager;

        public ConnectionForm()
        {
            InitializeComponent();
            cbbManager = new EnumsManager<ProviderType>(cbbProvider, "");

            cbbAuthentication.SelectedIndex = 0;

            txtexistCMP.ReadOnly = !rdbExistCMP.Checked;
            txtnewCMP.ReadOnly = rdbExistCMP.Checked;
            cbbExistSQL.Enabled = rdbExistSQL.Checked;
            txtNewSQL.ReadOnly = rdbExistSQL.Checked;
        }

        private void wizard1_BeforeSwitchPages(object sender, ERPFramework.Controls.Wizard.BeforeSwitchPagesEventArgs e)
        {
            if (e.OldIndex < e.NewIndex)
            {
                switch (e.OldIndex)
                {
                    case 0:
#if (SQLServer)
                        if (cbbManager.GetValue() == ProviderType.SQLServer)
                            e.NewIndex = 1;
#endif
#if (SQLCompact)
                        if (cbbManager.GetValue() == ProviderType.SQLCompact)
                            e.NewIndex = 3;
#endif
#if (SQLite)
                        if (cbbManager.GetValue() == ProviderType.SQLite)
                            e.NewIndex = 4;
#endif
                        break;

                    //case 1:
                    //    if (e.NewIndex == 2)
                    //    {
                    //        e.Cancel = CheckConnection();
                    //        e.NewIndex = 4;
                    //    }
                    //    break;
                    case 2:
                        if (e.NewIndex == 1)
                            e.NewIndex = 0;

                        //if (e.NewIndex == 3)
                        //    e.Cancel = CheckConnection();
                        break;

                    case 3:
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
                SearchSqlServer();
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

        private void SearchSqlServer()
        {
            this.cbbServer.Items.Clear();
            this.cbbServer.DropDownStyle = ComboBoxStyle.DropDown;
            this.cbbServer.Text = "Search SqlServer";
            Application.DoEvents();

            //var instance = SqlDataSourceEnumerator.Instance;
            //System.Data.DataTable table = instance.GetDataSources();

            DataTable dt = SmoApplication.EnumAvailableSqlServers(false);
            var localInstance = RegistryManager.ListLocalSqlInstances().ToList();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var srv = dt.Rows[i]["Name"].ToString();
                if (i == 0 && localInstance != null)
                    srv += localInstance.ElementAt<string>(0);
                cbbServer.Items.Add(srv);
            }
            if (this.cbbServer.Items.Count > 0)
            {
                this.cbbServer.SelectedIndex = 0;
                this.cbbServer.DropDownStyle = ComboBoxStyle.DropDown;
            }
            else
                this.cbbServer.Text = "<No available SQL Servers>";
        }

        private void ListDataBase()
        {
            cbbExistSQL.Items.Clear();

            Server srv = new Server(cbbServer.Text);
            foreach (Database db in srv.Databases)
                cbbExistSQL.Items.Add(db.Name);
        }

        #endregion

        private void wizard1_Finish(object sender, CancelEventArgs e)
        {
            DataBase_Provider = (ProviderType)cbbManager.GetValue();
            switch (DataBase_Provider)
            {
#if(SQLCompact)
                case ProviderType.SQLCompact:
                    if (rdbExistCMP.Checked)
                        DataBase_Name = txtexistCMP.Text;
                    else
                        DataBase_Name = txtnewCMP.Text;

                    DataBase_Password = txtpassCMP.Text;

                    DataBase_NewDatabase = rdbNewCMP.Checked;
                    break;
#endif
#if(SQLServer)
                case ProviderType.SQLServer:
                    if (rdbExistSQL.Checked)
                        DataBase_Name = cbbExistSQL.Text;
                    else
                        DataBase_Name = txtNewSQL.Text;

                    DataBase_Authentication = (AuthenticationMode)cbbAuthentication.SelectedIndex;

                    DataBase_Host = cbbServer.Text;
                    DataBase_Username = txtUser.Text;
                    DataBase_Password = txtPass.Text;

                    DataBase_NewDatabase = rdbNewSQL.Checked;
                    break;
#endif
#if(SQLite)
                case ProviderType.SQLite:
                    if (rdbexistLIT.Checked)
                        DataBase_Name = txtexistLIT.Text;
                    else
                        DataBase_Name = txtnewLIT.Text;

                    DataBase_Password = txtPassLIT.Text;
                    DataBase_NewDatabase = rdbnewLIT.Checked;
                    break;
#endif
            }
        }

        private void rdbExistLIT_CheckedChanged(object sender, EventArgs e)
        {
            txtexistLIT.ReadOnly = rdbnewLIT.Checked;
            txtnewLIT.ReadOnly = !rdbnewLIT.Checked;
        }
    }
}