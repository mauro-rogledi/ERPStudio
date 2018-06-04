using ERPFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFramework.Data
{
    public class SqlParametersCollection : Dictionary<string, SqlProxyParameter>
    {
        public void  Add(SqlProxyParameter param)
        {
            this.Add(param.ParameterName, param);
        }

        public void Add(IColumn column, SqlProxyParameter param)
        {
            this.Add(column.Name, param);
        }

        public SqlProxyParameter this[IColumn col] { get => this[col.Name]; }
    }
}
