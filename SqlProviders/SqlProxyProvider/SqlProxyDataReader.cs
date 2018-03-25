using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public class SqlProxyDataReader : IDisposable
    {

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        private readonly IDataReader SqlDataReader;


        public SqlProxyDataReader(IDataReader dr)
        {
            SqlDataReader = dr;
        }


        public object this[string name]
        {
            get => SqlDataReader[name];
        }

        public object this[int i]
        {
            get => SqlDataReader[i];
        }
    }
}