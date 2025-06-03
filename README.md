# 🛠️ MCP Server with C# + Firebird

This project is an MCP ( Model Context Protocol ) server created in C#, with support for custom tools. One of the features included is the execution of dynamic queries in a **Firebird** database with filter and record limit.

---

## 📦 Technologies Used
- NPX 
- C# (.NET 9)
- MCP ( Model Context Protocol )
- FirebirdSql.Data.FirebirdClient
- Dependency Injection via `Host.CreateApplicationBuilder`
- Configuration via `appsettings.json`

---

## ⚙️ Configuration
Install dependencies by nuget
``` bash
dotnet add package ModelContextProtocol --prerelease
```

### 🔧 `appsettings.json` File

Create an `appsettings.json` file in the project root with the following content:

```json
{
"ConnectionStrings": {
"FirebirdDb": "Database=C:\\yourdata.fdb;User=SYSDBA;Password=masterkey;Dialect=3;Charset=NONE;" }
}
```
📝 Change the path of the .FDB file according to the location of your database.

📂 Recommended Project Structure
```
src/
├── MCPServer/
│ ├── Factory/
│ │ └── FbConnectionFactory.cs
│ ├── Tools/
│ │ └── FbQueries.cs
│ ├── Program.cs
│ └── appsettings.json

```
## 🧩 MCP Tool Available
### 🔍 ListTable
```csharp
[McpTool, Description("List all records from a Firebird table")]
public List<Dictionary<string, object>> ListTable(string tableName, string filter = "", int limitRecords = 0)
```
This tool returns records from a Firebird database table with:

🔎 Conditional filter via SQL

🔢 Limitation of number of records (using FIRST)

![{DF1F6239-E983-45A0-87E3-645291A6D2C7}](https://github.com/user-attachments/assets/f15110b0-28f4-412c-8280-062e7cd1cf85)

Example of use:
```json
{
  "tool": "ListTable",
  "args": {
    "tableName": "CUSTOMERS",
    "filter": "STATUS = 'ACTIVE'",
    "limitRecords": 10
  }
}
```
# Run MCP server

```bash
  npx @modelcontextprotocol/inspector dotnet run
```
![{58D6C0FD-B032-48FC-9F16-8769A9403E82}](https://github.com/user-attachments/assets/4a2b3fd4-9f91-46c2-99f3-e387fc8b42e7)
