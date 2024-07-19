using ContactForm.Domain.Users.Entities;

namespace ContactForm.Domain.Users.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}