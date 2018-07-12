using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFramework.Data
{
    internal class DataModelProperties
    {
        public SqlProxyDataAdapter DataAdapter { get; set; }
        public SqlProxyCommand Command { get; set; }
        public string TableName { get; set; }

        public SqlProxyParameterCollection Parameters { get => Command.Parameters; }

        public DataModelProperties(string tableName) => TableName = tableName;
    }
}
