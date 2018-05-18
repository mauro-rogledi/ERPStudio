using System;
using System.Windows.Forms;

namespace ERPFramework.Preferences
{
    public partial class GenericPreferencePanel<T> : ERPFramework.Preferences.PreferencePanel where T : new()
    {
        public PreferencesManager<T> Preference { get; private set; }

       // private readonly LockManager lockManager;
        public T Preferences = new T();

        public GenericPreferencePanel()
            : base()
        {
            InitializeComponent();
        }

        public GenericPreferencePanel(string appName, Type[] types=null)
            : base(appName)
        {
            InitializeComponent();

            Preference = new PreferencesManager<T>(appName, types);
            //lockManager = new LockManager();
        }

        protected override void OnLoad(EventArgs e)
        {
            // For Design mode
            if (Preference != null)
            {
                Preference.ButtonComputer = ButtonComputer;
                Preference.ButtonUser = ButtonUser;
                Preference.ButtonApplication = ButtonApplication;
                Preference.FillComboBox(ButtonList);
                AttachData(Preferences);
            }
            if (ButtonList.Items.Count > 0)
                OnFindData((MyToolStripItem)ButtonList.Items[0]);
            base.OnLoad(e);
        }

        protected override bool OnToolStripButtonClick(ButtonClicked button)
        {
            return Preference.ButtonClick(button);
        }


        protected override void OnFindData(MyToolStripItem tsi)
        {
            Preferences = Preference.ReadPreference(tsi);
            AttachData(Preferences);
            OnPrepareAuxData();
            Validate();
        }

        protected override void OnNewData()
        {
            Preference.Clear();
            CanRemove = false;
        }

        protected override bool OnSaveData()
        {
            if (!this.Validate())
                return false;

            if (IsNew && Preference.ExistPreference())
            {
                MessageBox.Show("C'e'");
                return false;
            }

            var bOk = Preference.SavePreference(Preferences);
            if (bOk)
                Preference.FillComboBox(ButtonList);

            CanRemove = true;
            return bOk;
        }

        protected override bool OnDeleteData()
        {
            var bOk = Preference.DeletePreference(Preferences);
            if (bOk)
            {
                Preferences = new T();
                AttachData(Preferences);
                Preference.FillComboBox(ButtonList);
            }
            return bOk;
        }
    }
}