using CochaVibes.Core.Enum;
using System.Data;

namespace CochaVibes.Core.Interfaces
{
    public interface IDbConnectionFactory
    {
        DataBaseProvider Provider { get; }

        IDbConnection CreateConnection();
    }
}