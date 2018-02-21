using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace ERPFramework.Preferences
{
    public partial class NHUnspellPreferencePanel : NHUnspellPreferencePanelNoVis
    {
        public NHUnspellPreferencePanel()
            : base()
        { }

        public NHUnspellPreferencePanel(string appName)
            : base(appName)
        {
            InitializeComponent();
            ButtonText = Properties.Resources.T_Thesaurus;
            ButtonImage = Properties.Resources.Edit24;
        }

        //public NHUnspellPreferencePanel()
        //{
        //    InitializeComponent();
        //}

        protected override void OnInitializeData()
        {
            string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Dictionaries");
            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path, "*.dic"))
                {
                    string filename = Path.GetFileNameWithoutExtension(file);
                    if (filename == "it_IT")
                        cbbLanguages.Items.Add(Properties.Resources.it_IT);
                    if (filename == "en_US")
                        cbbLanguages.Items.Add(Properties.Resources.en_US);
                }
            }
        }

        protected override void OnBindData()
        {
            //BindControl(cbbLanguages, "Language");
            //BindControl(ckbEnable, "Enable", "DBChecked");
        }

        protected override void OnPrepareAuxData()
        {
        }

        protected override bool OnBeforeAddNew()
        {
            return true;
        }

        protected override void OnDisableControlsForNew()
        {
            ckbEnable.Enabled = cbbLanguages.Items.Count > 0;
        }

        protected override void OnDisableControlsForEdit()
        {
            ckbEnable.Enabled = cbbLanguages.Items.Count > 0;
        }

        protected override void OnValidateControl()
        {
            cbbLanguages.Focus();
        }
    }

    #region NHUnspellPreferencePanelNoVis

    public partial class NHUnspellPreferencePanelNoVis : ERPFramework.Preferences.GenericPreferencePanel<NHUnspellPref>
    {
        public NHUnspellPreferencePanelNoVis(string appName)
            : base(appName, null)
        {
        }

        public NHUnspellPreferencePanelNoVis()
        {
        }
    }

    #endregion

    [DataContract]
    public class NHUnspellPref
    {
        [DataMember]
        public bool Enable { get; set; }

        [DataMember]
        public string Language { get; set; }

        public NHUnspellPref()
        { }
    }
}