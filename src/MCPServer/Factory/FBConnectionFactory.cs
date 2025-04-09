using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Configuration;

namespace MCPServer.Factory;

public class FbConnectionFactory
{
    private static string? _connectionString;
    private readonly IConfiguration _configuration;

    private static FbConnection GetConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("FirebirdDb");
        if (string.IsNullOrEmpty(_connectionString))
            throw new InvalidOperationException("Connection string not set. Use SetConnectionString(path) first.");

        var conn = new FbConnection(_connectionString);
        conn.Open();
        return conn;
    }
    public FbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public FbConnection GetOpenConnection()
    {
 
        return FbConnectionFactory.GetConnection(_configuration);
    }    
    
}
