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
            //AddTable<EF_Users>(false);
            AddTable<EF_Version>(false);
            AddTable<EF_Descriptions>();
            AddTable<EF_Counter>();
            AddTable<EF_CounterValue>();
            AddTable<EF_Codes>();
            AddTable<EF_CodeSegment>();
            AddTable<EF_Preferences>();
            AddTable<EF_Serial>();

            var table = new EF_Serial();
            var dt = table.CreateTable();
            //var row = dt.NewRow();
            //row.SetValue<string>(AM_Users.Username, "ciao");
            //dt.Rows.Add(row);
            //var res = dt.Rows[0].GetValue<string>(AM_Users.Username);

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