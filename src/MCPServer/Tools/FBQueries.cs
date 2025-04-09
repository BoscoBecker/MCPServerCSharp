using System.ComponentModel;
using System.Text.RegularExpressions;
using FirebirdSql.Data.FirebirdClient;
using MCPServer.Factory;
using ModelContextProtocol.Server;

namespace MCPServer.Tools;

[McpToolType]
public partial class FbQueries(FbConnectionFactory factory)
{
    [McpTool, Description("List all records from a Firebird table")]
    public List<Dictionary<string, object>> ListTable(string tableName, string filter="", int limitRecords=0)
    {
        var results = new List<Dictionary<string, object>>();
        if (string.IsNullOrWhiteSpace(tableName))
            throw new ArgumentException("Table name cannot be empty.");
        if (!MyRegex().IsMatch(tableName))
            throw new ArgumentException("Invalid table name."); 
        
        var limitClause = limitRecords > 0 ? $"FIRST {limitRecords}" : "";
        var sql = $"SELECT {limitClause} * FROM {tableName} WHERE 1=1";
        
        if (!string.IsNullOrWhiteSpace(filter))
        {
            sql += $" AND (@nome)"; 
        }        
        using var conn = factory.GetOpenConnection();
        using var cmd = new FbCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nome", filter);
        
        using var reader = cmd.ExecuteReader();
        
        {
            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                results.Add(row);
            }
        }

        return results;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9_]+$")]
    private static partial Regex MyRegex();
}