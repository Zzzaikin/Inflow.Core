using MySql.Data.MySqlClient;
using SqlKata.Compilers;

namespace Inflow.Data.Options
{
    public class MySqlOptions : BaseSqlOptions
    {
        public MySqlOptions() : base(new MySqlCompiler(), new MySqlConnection()) { }
    }
}
