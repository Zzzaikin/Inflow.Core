using Npgsql;
using SqlKata.Compilers;

namespace Inflow.Data.Options
{
    public class PostgreSqlOptions() : BaseSqlOptions(new PostgresCompiler(), new NpgsqlConnection());
}
