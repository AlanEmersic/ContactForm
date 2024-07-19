namespace ContactForm.Application.Users.DTO;

public sealed record UserServiceDto(
    int Id,
    string Name,
    string Username,
    string Email,
    AddressServiceDto Address,
    string Phone,
    string Website,
    CompanyServiceDto Company
);