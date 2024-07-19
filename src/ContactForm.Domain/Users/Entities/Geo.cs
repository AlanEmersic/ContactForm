namespace ContactForm.Domain.Users.Entities;

public sealed class Geo
{
    public int Id { get; set; }
    public int AddressId { get; set; }
    public string Latitude { get; set; } = null!;
    public string Longitude { get; set; } = null!;
}