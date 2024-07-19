using Microsoft.Data.SqlClient;
using System.Data;

namespace ContactForm.Infrastructure.Persistence.Database;

internal sealed class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        SqlConnection connection = new(connectionString);

        await connection.OpenAsync();

        return connection;
    }
}