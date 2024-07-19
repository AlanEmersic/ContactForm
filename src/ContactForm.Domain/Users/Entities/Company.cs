namespace ContactForm.Domain.Users.Entities;

public sealed class Company
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string CatchPhrase { get; set; } = null!;
    public string Bs { get; set; } = null!;
}