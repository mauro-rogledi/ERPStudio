using ApplicationLibrary;
using ApplicationLibrary.ExtendedControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationManager
{
    partial class usersFormOld
    {

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private DBRadioButton rdbNever;
        private DBRadioButton rdbDate;
        private DBCheckBox ckbForceChange;
        private DBCheckBox ckbBlocked;
        private EnumsManager<UserType> PrivilegeTypes = null;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DateTimePicker dteExpire;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cbbPrivilege;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label1;
    }
}
