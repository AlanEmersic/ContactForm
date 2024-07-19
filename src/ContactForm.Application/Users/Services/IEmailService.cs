using ContactForm.Domain.Users.Entities;

namespace ContactForm.Application.Users.Services;

public interface IEmailService
{
    Task SendEmailAsync(User user);
}