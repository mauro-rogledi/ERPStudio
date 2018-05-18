using ERPFramework.Data;
using ERPFramework.Forms;
using System.Windows.Forms;

namespace ERPFramework.Login
{
    /// <summary>
    /// Summary description for ChgPwdForm.
    /// </summary>
    public partial class ChgPwdForm : AskForm
    {
        string oldPwd;
        public string Password { get; private set; }

        public ChgPwdForm()
        {
            InitializeComponent();
        }

        public ChgPwdForm(string oldpwd)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            oldPwd = oldpwd;
        }

        public override bool OnOk()
        {
            if (Cryption.Encrypt(txtOldPwd.Text) != oldPwd)
            {
                MessageBox.Show(
                    Properties.Resources.Password_Different,
                    Properties.Resources.Warning,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtNewPwd.Text != txtConfirmPwd.Text)
            {
                MessageBox.Show(
                    Properties.Resources.Password_Different,
                    Properties.Resources.Warning,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Password = txtNewPwd.Text;

            return base.OnOk();
        }
    }
}