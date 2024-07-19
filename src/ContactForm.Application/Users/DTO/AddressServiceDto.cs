namespace ContactForm.Application.Users.DTO;

public sealed record AddressServiceDto(
    string Street,
    string Suite,
    string City,
    string Zipcode,
    GeoServiceDto Geo);