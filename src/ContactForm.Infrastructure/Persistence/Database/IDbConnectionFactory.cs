using System.Data;

namespace ContactForm.Infrastructure.Persistence.Database;

internal interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}