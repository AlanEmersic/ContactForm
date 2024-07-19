using ContactForm.Application.Users.Commands;
using ContactForm.Application.Users.DTO;
using ContactForm.Domain.Users.Entities;

namespace ContactForm.Application.Users.Mappings;

public static class UserMappingExtensions
{
    public static User MapToDomain(this CreateUserContactCommand command, UserServiceDto userServiceDto)
    {
        return new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Phone = userServiceDto.Phone,
            Website = userServiceDto.Website,
            Address = new Address
            {
                City = userServiceDto.Address.City,
                Street = userServiceDto.Address.Street,
                Suite = userServiceDto.Address.Suite,
                Zipcode = userServiceDto.Address.Zipcode,
                Geo = new Geo
                {
                    Latitude = userServiceDto.Address.Geo.Lat,
                    Longitude = userServiceDto.Address.Geo.Lng
                }
            },
            Company = new Company
            {
                Name = userServiceDto.Company.Name,
                CatchPhrase = userServiceDto.Company.CatchPhrase,
                Bs = userServiceDto.Company.Bs
            },
            CreatedDate = DateTime.UtcNow
        };
    }

    public static User MapToDomain(this CreateUserContactCommand command)
    {
        return new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            CreatedDate = DateTime.UtcNow
        };
    }
}