namespace ContactForm.Infrastructure.Persistence.Users.Queries;

internal static class UserQueries
{
    public const string InsertUser = @"
        INSERT INTO Users (FirstName, LastName, Email, Phone, Website, CreatedDate)
        VALUES (@FirstName, @LastName, @Email, @Phone, @Website, @CreatedDate);
        SELECT CAST(SCOPE_IDENTITY() as int);";

    public const string InsertAddress = @"
        INSERT INTO Addresses (UserId, Street, Suite, City, ZipCode)
        VALUES (@UserId, @Street, @Suite, @City, @ZipCode);
        SELECT CAST(SCOPE_IDENTITY() as int);";

    public const string InsertGeo = @"
        INSERT INTO Geos (AddressId, Latitude, Longitude)
        VALUES (@AddressId, @Latitude, @Longitude);
        SELECT CAST(SCOPE_IDENTITY() as int);";

    public const string InsertCompany = @"
        INSERT INTO Companies (UserId, Name, CatchPhrase, Bs)
        VALUES (@UserId, @Name, @CatchPhrase, @Bs);
        SELECT CAST(SCOPE_IDENTITY() as int);";

    public const string GetUserByEmail = @"
        SELECT TOP 1
            [user].Id, [user].FirstName, [user].LastName, [user].Email, [user].Phone, [user].Website, [user].CreatedDate,
            address.Street, address.Suite, address.City, address.ZipCode,
            company.Name, company.CatchPhrase, company.Bs,
            geo.Latitude, geo.Longitude
        FROM Users [user]
        LEFT JOIN Addresses address ON [user].Id = address.UserId
        LEFT JOIN Companies company ON [user].Id = company.UserId
        LEFT JOIN Geos geo ON address.Id = geo.AddressId
        WHERE [user].Email = @Email
        ORDER BY [user].CreatedDate DESC;";
}