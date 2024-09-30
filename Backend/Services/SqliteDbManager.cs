using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace Backend.Services;

public class SqliteDbManager : IDbManager
{
    public DbConnection GetConnection()
    {
        try
        {
            var connection = new SqliteConnection("Data Source=Resources\\citystatecountry.db;Mode=ReadWrite");
            return connection;
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
