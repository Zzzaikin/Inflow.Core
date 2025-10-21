using Microsoft.Data.SqlClient;
using SqlKata.Compilers;

namespace Inflow.Data.Options
{
    public class SqlServerOptions() : BaseSqlOptions(new SqlServerCompiler(), new SqlConnection());
}
