using ERPFramework.Data;
using ERPFramework.Libraries;
using System.ComponentModel;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    public class ExCheckedListBox : CheckedListBox
    {
        [Browsable(true)]
        [System.ComponentModel.Bindable(true, BindingDirection.TwoWay)]
        public string Value
        {
            get { return Encode(); }
            set { decode(value); }
        }

        public string Decode
        {
            get
            {
                string result = string.Empty;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (GetItemChecked(i))
                        result = result.SeparConcat(((GenericList<string>)Items[i]).Display, " ");
                }
                return result;
            }
        }

        private void decode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            string[] list = value.Split(new char[] { ',' });
            for (int i = 0; i < Items.Count; i++)
            {
                for (int t = 0; t < list.Length; t++)
                {
                    if (((GenericList<string>)Items[i]).Archive == list[t])
                    {
                        SetItemChecked(i, true);
                        break;
                    }
                    SetItemChecked(i, false);
                }
            }
        }

        private string Encode()
        {
            string result = string.Empty;
            for (int i = 0; i < Items.Count; i++)
                if (GetItemChecked(i))
                    result = result.CommaConcat(((GenericList<string>)Items[i]).Archive);

            return result;
        }

        virtual public void AttachDataReader(IDataReaderUpdater dr, IColumn code, IColumn description)
        {
            if (GlobalInfo.DBaseInfo.dbManager != null)
            {
                dr.Find();
                for (int t = 0; t < dr.Count; t++)
                    Items.Add(new GenericList<string>(dr.GetValue<string>(code, t), dr.GetValue<string>(description, t), true));
            }
        }
    }
}