using ERPFramework.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ERPFramework.Controls
{
    public class RadarComboBox : MetroFramework.Controls.MetroComboBox
    {
        private SqlProxyConnection sqlCN = null;
        private IColumn dC;
        private string Category = string.Empty;

        public RadarComboBox()
            : base()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void AddTableReader(string category, IColumn descriptionColumn)
        {
            dC = descriptionColumn;

            FillComboBox();
        }

        private void FillComboBox()
        {
            QueryBuilder qB = new QueryBuilder().
                SelectAllFrom(dC.TableType);

            SqlProxyCommand fillCM = new SqlProxyCommand(qB.Query, sqlCN);

            try
            {
                SqlProxyDataReader fillRD = fillCM.ExecuteReader();
                while (fillRD.Read())
                    this.Items.Add(fillRD.GetString(dC.Name));

                fillRD.Close();
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.StackTrace, exc.Message);
            }
        }
    }
}