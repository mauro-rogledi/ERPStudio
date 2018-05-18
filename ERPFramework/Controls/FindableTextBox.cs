namespace ERPFramework.Controls
{
    public partial class FindableTextBox : MetroFramework.Controls.MetroTextBox, IFindable
    {
        public void Clean()
        {
            this.Text = string.Empty;
        }
    }
}