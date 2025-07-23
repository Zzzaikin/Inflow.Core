using System.Data;
using System.Data.Common;
using SqlKata.Compilers;
using Inflow.Common;

namespace Inflow.Data.Options
{
    public abstract class BaseSqlOptions 
    {
        public Compiler Compiler { get; private set; }

        public DbConnection DbConnection { get; private set; }

        protected BaseSqlOptions(Compiler compiler, DbConnection dbConnection) 
        {
            Argument.IsNotNull(compiler, nameof(compiler));
            Argument.IsNotNull(dbConnection, nameof(dbConnection));

            Compiler = compiler;
            DbConnection = dbConnection;
        }

        ~BaseSqlOptions() 
        {
            CloseConnectionIfOpened();
        }

        public void OpenConnectionIfClosed()
        {
            if (DbConnection.State == ConnectionState.Closed)
            {
                DbConnection.Open();
            }
        }

        private void CloseConnectionIfOpened()
        {
            if (DbConnection.State == ConnectionState.Open)
            {
                DbConnection.Close();
            }
        }
    }
}
