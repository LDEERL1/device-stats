using System.Data.Common;
namespace DeviceStats.Data;

public interface IDbConnectionFactory
{
    DbConnection Create();
}
