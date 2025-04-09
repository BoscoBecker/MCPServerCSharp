using System.ComponentModel;
using System.Data;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using MCPServer.Factory;
using MCPServer.Service;
using MCPServer.Tools;

namespace MCPServer;

internal abstract class Program
{
    private static async Task Main()
    {
        var builder = Host.CreateApplicationBuilder();
            builder.Configuration.AddJsonFile("appsettings.json");
            var configuration = builder.Configuration;
            builder.Services.AddSingleton<FbConnectionFactory>();        
            builder.Services.AddSingleton<FbQueries>();
            builder.Services
                .AddMcpServer()
                .WithStdioServerTransport()
                .WithTools();
        
        var app = builder.Build();
            ServiceLocator.SetProvider(app.Services);
        await app.RunAsync();
    }

    [McpToolType]
    public static class TablesFromDataBase
    {
        [McpTool, Description("Get data for table from name")]
        public static List<Dictionary<string, object>> ListTable(string tableName, string filter, int limitRecords=0)
        {
            var fbQueries = ServiceLocator.Instance?.GetRequiredService<FbQueries>()
                            ?? throw new InvalidOperationException("Service provider not initialized");
            return fbQueries.ListTable(tableName, filter, limitRecords);
        }

    }
}