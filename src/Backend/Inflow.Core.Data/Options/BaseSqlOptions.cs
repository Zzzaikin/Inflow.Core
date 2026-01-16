using System.Data.Common;
using SqlKata.Compilers;

namespace Inflow.Core.Data.Options;

public abstract class BaseSqlOptions
{
    public Compiler Compiler { get; private set; }

    /// <summary>
    /// Did not dispose there because this is just options. DbConnection dispose in <see cref="BaseQuery"/> through the
    /// Database provider disposing.
    /// </summary>
    public DbConnection DbConnection { get; }

    protected BaseSqlOptions(Compiler compiler, DbConnection dbConnection) 
    {
        ArgumentNullException.ThrowIfNull(compiler);
        ArgumentNullException.ThrowIfNull(dbConnection);
        Compiler = compiler;
        DbConnection = dbConnection;
    }
}