using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERPFramework.Forms
{
    internal class ControlFinder
    {
        private Dictionary<string, Control> controlFound = new Dictionary<string, Control>();
        private Control doc;

        public ControlFinder(Control doc)
        {
            this.doc = doc;
        }

        public T Find<T>(string ctrlName)
        {
            if (controlFound.ContainsKey(ctrlName))
                return (T)Convert.ChangeType(controlFound[ctrlName], typeof(T));
            else
                return Find<T>(ctrlName, doc.Controls);
        }

        public T Find<T>(string ctrlName, Control.ControlCollection controls)
        {
            if (controls.ContainsKey(ctrlName))
            {
                controlFound.Add(ctrlName, controls[ctrlName]);
                return (T)Convert.ChangeType(controls[ctrlName], typeof(T));
            }

            foreach (Control cntrl in controls)
            {
                T cFound = Find<T>(ctrlName, cntrl.Controls);
                if (cFound != null)
                    return cFound;
            }

            return default(T);
        }
    }
}