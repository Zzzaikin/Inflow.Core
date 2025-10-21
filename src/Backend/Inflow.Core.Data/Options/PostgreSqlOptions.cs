using Npgsql;
using SqlKata.Compilers;

namespace Inflow.Core.Data.Options;

public class PostgreSqlOptions() : BaseSqlOptions(new PostgresCompiler(), new NpgsqlConnection());