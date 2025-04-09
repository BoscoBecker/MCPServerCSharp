# 🛠️ MCP Server with C# + Firebird

This project is an MCP (Modular Command Platform) server created in C#, with support for custom tools. One of the features included is the execution of dynamic queries in a **Firebird** database with filter and record limit.

---

## 📦 Technologies Used
- NPX 
- C# (.NET 9)
- MCP (Modular Command Platform)
- FirebirdSql.Data.FirebirdClient
- Dependency Injection via `Host.CreateApplicationBuilder`
- Configuration via `appsettings.json`

---

## ⚙️ Configuration
Install dependencies

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

![img_1.png](img_1.png)

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
![img.png](img.png)