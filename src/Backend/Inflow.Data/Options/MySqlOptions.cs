using MySql.Data.MySqlClient;
using SqlKata.Compilers;

namespace Inflow.Data.Options;

public class MySqlOptions() : BaseSqlOptions(new MySqlCompiler(), new MySqlConnection());