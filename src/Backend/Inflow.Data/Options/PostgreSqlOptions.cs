using Npgsql;
using SqlKata.Compilers;

namespace Inflow.Data.Options
{
    public class PostgreSqlOptions : BaseSqlOptions
    {
        public PostgreSqlOptions() : base(new PostgresCompiler(), new NpgsqlConnection()) { }
    }
}
