using Microsoft.Data.SqlClient;
using SqlKata.Compilers;

namespace Inflow.Core.Data.Options;

public class SqlServerOptions() : BaseSqlOptions(new SqlServerCompiler(), new SqlConnection());