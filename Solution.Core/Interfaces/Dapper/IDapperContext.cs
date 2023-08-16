using System.Data;

namespace Solution.Core.Interfaces.Dapper
{
    public interface IDapperContext
    {
        IDbConnection getDbConnection();
    }
}
