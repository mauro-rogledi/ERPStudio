using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetroFramework.Extender
{
    public class MetroExToolbar : ScrollableControl
    {
        private Collection<MetroToolbarButton> field = new Collection<MetroToolbarButton>();

        [Category("Data")]
        [Description("asdf")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Collection<MetroToolbarButton> Items
        {
            get { return field; }
        }
    }
}
