using System.Data.SqlClient;
using SqlKata.Compilers;

namespace Inflow.Data.Options
{
    public class SqlServerOptions : BaseSqlOptions
    {
        public SqlServerOptions() : base(new SqlServerCompiler(), new SqlConnection()) { }
    }
}
