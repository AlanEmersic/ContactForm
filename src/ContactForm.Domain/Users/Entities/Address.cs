namespace ContactForm.Domain.Users.Entities;

public sealed class Address
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Street { get; set; } = null!;
    public string Suite { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Zipcode { get; set; } = null!;
    public Geo Geo { get; set; } = null!;
}