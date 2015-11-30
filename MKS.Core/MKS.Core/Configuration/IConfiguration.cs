using System.Data;

namespace MKS.Core.Configuration
{
    public interface IConfiguration
    {
        IDbConnection GetConnection();

    }
}