using MySql.Data.MySqlClient;
using SqlKata.Compilers;

namespace Inflow.Core.Data.Options;

public class MySqlOptions() : BaseSqlOptions(new MySqlCompiler(), new MySqlConnection());