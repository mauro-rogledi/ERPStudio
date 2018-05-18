using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Resources;
using ERPFramework.DataGridViewControls;
using MetroFramework.Extender;

namespace ERPFramework.Controls
{
    public static class EnumsListCreator
    {
        public static object CreateItem(object a, string b)
        {
#if (SQLiteOld)
            if (GlobalInfo.LoginInfo.ProviderType == ProviderType.SQLite)
                return new EnumsList<long>((long)(int)(a), b);
            else
#endif
                return new EnumsList<int>((int)a, b);
        }

        public static object GetValue(object enumlist)
        {
#if (SQLiteOld)
            if (GlobalInfo.LoginInfo.ProviderType == ProviderType.SQLite)
                return ((EnumsList<long>)enumlist).Archive;
            else
#endif
            return ((EnumsList<int>)enumlist).Archive;
        }

        public static string GetText(object enumlist)
        {
#if (SQLiteOld)
            if (GlobalInfo.LoginInfo.ProviderType == ProviderType.SQLite)
                return ((EnumsList<long>)enumlist).Display;
            else
#endif
            return ((EnumsList<int>)enumlist).Display;
        }

        public static void ChangeText(object enumlist, string text)
        {
#if (SQLiteOld)
            if (GlobalInfo.LoginInfo.ProviderType == ProviderType.SQLite)
                ((EnumsList<long>)enumlist).Display = text;
            else
#endif
            ((EnumsList<int>)enumlist).Display = text; ;
        }
    }


    public interface IEnumsManager
    {

    }

    public class EnumsManager<T> : IEnumsManager
    {
        private List<object> List = new List<object>();
        private System.Resources.ResourceManager myManager;

        private ComboBox cbbBox;
        private MetroDataGridViewComboBoxColumn dgwCbbBox;
        private MetroFramework.Extender.MetroListBox lsbBox;

        public Predicate<T> Filter { get; set; }

        public int Count
        {
            get { return List.Count; }
        }


        public EnumsManager(string resource = "", Predicate<T> filter = null)
        {
            myManager = resource.IsEmpty()
                            ? Properties.Resources.ResourceManager
                            : myManager = new System.Resources.ResourceManager(resource, System.Reflection.Assembly.GetAssembly(typeof(T)));

            Filter = filter;
            FillArray(false);
        }

        public EnumsManager(ResourceManager resource = null, Predicate<T> filter = null)
        {
            myManager = resource == null
                            ? Properties.Resources.ResourceManager
                            : resource;

            Filter = filter;
            FillArray(false);
        }
        public EnumsManager(ComboBox cbbBox, string resource = "", bool bAlsoNull= false, Predicate<T> filter = null)
        {
            myManager = resource.IsEmpty()
                            ? Properties.Resources.ResourceManager
                            : myManager = new System.Resources.ResourceManager(resource, System.Reflection.Assembly.GetAssembly(typeof(T)));

            Filter = filter;
            if (cbbBox != null)
                AttachTo(cbbBox, bAlsoNull);
        }

        public EnumsManager(ComboBox cbbBox, ResourceManager resource = null, bool bAlsoNull = false, Predicate<T> filter = null)
        {
            myManager = resource == null
                            ? Properties.Resources.ResourceManager
                            : resource;

            Filter = filter;
            if (cbbBox != null)
                AttachTo(cbbBox, bAlsoNull);
        }

        public EnumsManager(MetroDataGridViewComboBoxColumn cbbBox, string resource = "", bool bAlsoNull = false, Predicate<T> filter = null)
        {
            myManager = resource.IsEmpty()
                            ? Properties.Resources.ResourceManager
                            : myManager = new System.Resources.ResourceManager(resource, System.Reflection.Assembly.GetAssembly(typeof(T)));

            Filter = filter;
            if (cbbBox != null)
                AttachTo(cbbBox, bAlsoNull);
        }

        public EnumsManager(MetroDataGridViewComboBoxColumn cbbBox, ResourceManager resource = null, bool bAlsoNull = false, Predicate<T> filter = null)
        {
            myManager = resource == null
                            ? Properties.Resources.ResourceManager
                            : resource;

            Filter = filter;
            if (cbbBox != null)
                AttachTo(cbbBox, bAlsoNull);
        }

        public EnumsManager(MetroListBox lstBox, string resource = "", bool bAlsoNull = false, Predicate<T> filter = null)
        {
            myManager = resource.IsEmpty()
                            ? Properties.Resources.ResourceManager
                            : myManager = new System.Resources.ResourceManager(resource, System.Reflection.Assembly.GetAssembly(typeof(T)));

            Filter = filter;
            if (lstBox != null)
                AttachTo(lstBox, bAlsoNull);
        }

        public EnumsManager(MetroListBox lstBox, ResourceManager resource = null, bool bAlsoNull = false, Predicate<T> filter = null)
        {
            myManager = resource == null
                            ? Properties.Resources.ResourceManager
                            : resource;

            Filter = filter;
            if (lstBox != null)
                AttachTo(lstBox, bAlsoNull);
        }

        #region MyRegion

        private void FillArray(bool bAlsoNull)
        {
            List.Clear();

            if (bAlsoNull)
                List.Add(new EnumsList<T>(default(T), ""));

            foreach (T i in Enum.GetValues(typeof(T)))
            {
                var a = (object)Enum.Parse(typeof(T), i.ToString());

                if (Filter?.Invoke(i) ?? true)
                        List.Add(EnumsListCreator.CreateItem(a, Tranlate(Enum.GetName(typeof(T), i))));
            }
        }

        private string Tranlate(string sText)
        {
            string text = string.Empty;
            if (myManager == null)
                return sText;

            try
            {
                text = myManager.GetString(sText);
                System.Diagnostics.Debug.Assert(text != null);
            }
            catch (Exception e)
            {
                text = e.Message;
                System.Diagnostics.Debug.Assert(false);
            }

            return text;
        }

        #endregion

        public void Refresh(bool alsoFill = false, bool alsoNull = false)
        {
            if (alsoFill)
            {
                List.Clear();
                FillArray(alsoNull);
            }

            if (cbbBox != null && cbbBox.BindingContext[List] != null)
                ((CurrencyManager)cbbBox.BindingContext[List]).Refresh();
        }

        public void AttachTo(ComboBox cbbBox, bool bAlsoNull = false)
        {
            FillArray(bAlsoNull);

            this.cbbBox = cbbBox;

            cbbBox.DataSource = List;
            cbbBox.DisplayMember = "Display";
            cbbBox.ValueMember = "Archive";
        }

        public void AttachTo(MetroDataGridViewComboBoxColumn cbbBox, bool bAlsoNull = false)
        {
            FillArray(bAlsoNull);

            this.dgwCbbBox = cbbBox;

            dgwCbbBox.DataSource = List;
            dgwCbbBox.DisplayMember = "Display";
            dgwCbbBox.ValueMember = "Archive";
        }

        public void AttachTo(MetroListBox lstBox, bool bAlsoNull = false)
        {
            FillArray(bAlsoNull);

            this.lsbBox = lstBox;
            this.lsbBox.DataSource = List;
            this.lsbBox.DisplayMember = "Display";
            this.lsbBox.ValueMember = "Archive";
        }

        public virtual bool DisplayValue(T t)
        {
            return true;
        }

        public T GetValue()
        {
            if (cbbBox != null)
            {
                if (cbbBox.SelectedValue == null || cbbBox.SelectedValue == System.DBNull.Value)
                    return (T)Enum.ToObject(typeof(T), 0);
                int pp = (int)Enum.Parse(typeof(T), cbbBox.SelectedValue.ToString());
                return (T)Enum.Parse(typeof(T), cbbBox.SelectedValue.ToString());
            }
            else
            {
                return (T)Enum.ToObject(typeof(T), 0);
                //if (dgwCbbBox.v == null)
                //    return 0;
                //int pp = (int)Enum.Parse(enumType, dgwCbbBox.t.ToString());
                //return Enum.Parse(enumType, dgwCbbBox.SelectedValue.ToString());
            }
        }

        public T GetValue(object selectedValue)
        {
            if (selectedValue == null || selectedValue is System.DBNull)
                return (T)Enum.ToObject(typeof(T), 0);
            string text = Tranlate(((string)selectedValue));

            if (Enum.IsDefined(typeof(T), text))
                return (T)Enum.Parse(typeof(T), text);
            else
            {
                Type undertype = Enum.GetUnderlyingType(typeof(T));
                return (T)Enum.ToObject(typeof(T), 0);
            }
        }

        public string GetText(int pos)
        {
            return EnumsListCreator.GetText(List[pos]);
        }

        public string GetText()
        {
            if (cbbBox != null)
                return EnumsListCreator.GetText(List[cbbBox.SelectedIndex]);
            else
                return string.Empty;
        }

        public object GetValue(int pos)
        {
            return EnumsListCreator.GetValue(List[pos]);
        }

        //public int GetIntValue()
        //{
        //    if (cbbBox != null && cbbBox.SelectedValue != null)
        //        return (int)((EnumsList)List[cbbBox.SelectedIndex]).Archive;
        //    else
        //        return 0;
        //}

        public void ChangeText(int val, string Text)
        {
            EnumsListCreator.ChangeText(List[val], Text);
        }
    }

    public class EnumsList<L>
    {
        private L myArchive;
        private string mydisplay;

        public EnumsList(L archive, string display)
        {
            this.myArchive = archive;
            this.mydisplay = display;
        }

        public L Archive { get { return myArchive; } }

        public string Display
        {
            get { return mydisplay; }
            set { mydisplay = value; }
        }

        public override string ToString()
        {
            return this.myArchive.ToString();
        }
    }
}