using ContactForm.Domain.Users.Entities;
using ContactForm.Domain.Users.Repositories;
using ContactForm.Infrastructure.Persistence.Database;
using ContactForm.Infrastructure.Persistence.Users.Queries;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace ContactForm.Infrastructure.Persistence.Users.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory connectionFactory;
    private readonly ILogger<UserRepository> logger;

    public UserRepository(IDbConnectionFactory connectionFactory, ILogger<UserRepository> logger)
    {
        this.connectionFactory = connectionFactory;
        this.logger = logger;
    }

    public async Task AddAsync(User user)
    {
        using IDbConnection connection = await connectionFactory.CreateConnectionAsync();
        using IDbTransaction transaction = connection.BeginTransaction();

        try
        {
            user.Id = await connection.QuerySingleAsync<int>(UserQueries.InsertUser, user, transaction);

            if (user.Address is not null)
            {
                user.Address.UserId = user.Id;

                user.Address.Id = await connection.QuerySingleAsync<int>(UserQueries.InsertAddress, user.Address, transaction);

                if (user.Address.Geo is not null)
                {
                    user.Address.Geo.AddressId = user.Address.Id;

                    user.Address.Geo.Id = await connection.QuerySingleAsync<int>(UserQueries.InsertGeo, user.Address.Geo, transaction);
                }
            }

            if (user.Company is not null)
            {
                user.Company.UserId = user.Id;

                user.Company.Id = await connection.QuerySingleAsync<int>(UserQueries.InsertCompany, user.Company, transaction);
            }

            transaction.Commit();
            logger.LogInformation("User with email: {Email} has been successfully added.", user.Email);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            logger.LogError(ex, "Failed to insert user.");
            throw;
        }
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using IDbConnection connection = await connectionFactory.CreateConnectionAsync();

        try
        {
            IEnumerable<User> users = await connection.QueryAsync<User, Address, Company, Geo, User>(UserQueries.GetUserByEmail, (user, address, company, geo) =>
            {
                if (address is not null)
                {
                    address.Geo = geo;
                    user.Address = address;
                }

                user.Company = company;

                return user;
            }, new { Email = email }, splitOn: "Street,Name,Latitude");

            return users.FirstOrDefault();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get user.");
            throw;
        }
    }
}