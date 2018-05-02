using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProxyProvider
{
    public interface ISqlProxyCreateDatabase
    {
        string DataSource { get; set; }
        string UserID { get; set; }
        string InitialCatalog { get; set; }
        string Password { get; set; }
        bool IntegratedSecurity { get; set; }

        void CreateDatabase();
        void CreateDatabase(string connectionString);
    }
}
