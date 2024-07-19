namespace ContactForm.Domain.Users.Entities;

public sealed class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Address? Address { get; set; }
    public Company? Company { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public DateTime CreatedDate { get; set; }
}