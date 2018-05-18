namespace ERPFramework.Controls
{
    public partial class FindableIntelliTextBox : MetroFramework.Extender.MetroIntelliTextBox, IFindable
    {
        public void Clean()
        {
            this.Text = string.Empty;
        }
    }
}