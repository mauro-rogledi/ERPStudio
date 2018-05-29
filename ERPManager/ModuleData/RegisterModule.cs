using System;

using ERPFramework.Data;
using ERPFramework.Preferences;
using System.Collections.Generic;
using ERPFramework;

#region RegisterTable

namespace ERPManager.ModuleData
{
    public class RegisterModule : ERPFramework.Data.RegisterModule
    {
        override public int CurrentVersion()
        {
            return 2;
        }

        override public string Module()
        {
            return "APPM";
        }

        public override Version DllVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        override protected bool CreateDBTables()
        {
            //#region Users Table

            //if (SearchTable<AM_Users>())
            //    SqlCreateTable.CreateTable<AM_Users>();


            //#endregion

            //#region Lock Table

            //if (SearchTable<AM_Locks>())
            //    SqlCreateTable.CreateTable<AM_Locks>();

            //#endregion

            //#region AM_Version

            //if (SearchTable<AM_Version>())
            //    SqlCreateTable.CreateTable<AM_Version>();

            //#endregion

            //#region AM_Counter

            ////newTable.DropTable(AM_Counter.Name);

            //if (SearchTable<AM_Counter>())
            //    SqlCreateTable.CreateTable<AM_Counter>();


            //#endregion

            //#region AM_CounterValue

            //if (SearchTable<AM_CounterValue>())
            //    SqlCreateTable.CreateTable<AM_CounterValue>();

            //#endregion

            return true;
        }

        override protected bool UpdateDBTables()
        {
            return true;
        }

        public override void Addon(ERPFramework.Forms.IDocument frm, ERPFramework.NameSpace nameSpace)
        {
            //throw new NotImplementedException();
        }

        public override PreferencePanel[] RegisterPreferences()
        {
            var panels = new List<PreferencePanel>();
            panels.Add(new GlobalPreferencePanel(""));
            panels.Add(new PrinterPreferencePanel(""));
            return panels.ToArray();
        }
    }
}

#endregion