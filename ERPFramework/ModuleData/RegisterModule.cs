using ERPFramework.Data;
using ERPFramework.Forms;
using System;

#region RegisterTable

namespace ERPFramework.ModuleData
{
    public class RegisterModule : ERPFramework.Data.RegisterModule
    {
        override public int CurrentVersion()
        {
            return 1;
        }

        override public string Module()
        {
            return "APPL";
        }

        public override Version DllVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public override void Addon(IDocument frm, NameSpace nameSpace)
        {
            throw new NotImplementedException();
        }

        override protected bool CreateDBTables()
        {
            AddTable<AM_Users>(false);
            AddTable<AM_Version>(false);
            AddTable<AM_Descriptions>();
            AddTable<AM_Counter>();
            AddTable<AM_CounterValue>();
            AddTable<AM_Codes>();
            AddTable<AM_CodeSegment>();
            AddTable<AM_Preferences>();

            var table = new AM_Users();
            var dt = table.CreateTable();
            var row = dt.NewRow();
            row.SetValue<string>(AM_Users.Username, "ciao");
            dt.Rows.Add(row);
            var res = dt.Rows[0].GetValue<string>(AM_Users.Username);

            return true;
        }

        override protected bool UpdateDBTables()
        {
            //if (dbVersion < 2)
            //{
            //    if (!SearchColumn(AM_Counter.InvertSufPref))
            //    {
            //        SqlCreateTable.AlterTable<AM_Counter>();
            //        SqlCreateTable.AddColumn(AM_Counter.InvertSufPref);
            //        return SqlCreateTable.Alter();
            //    }
            //}
            return true;
        }
    }
}

#endregion