using ContactForm.Application.Users.Services;
using ContactForm.Domain.Users.Entities;
using System.Net;
using System.Net.Mail;

namespace ContactForm.Infrastructure.Persistence.Users.Services;

internal sealed class EmailService : IEmailService
{
    public async Task SendEmailAsync(User user)
    {
        const string email = "email";
        const string password = "password";
        const string host = "smtp-mail.outlook.com";
        const int port = 587;
        const string subject = "Contact Form Submission";

        SmtpClient client = new(host, port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(email, password)
        };

        string body = GetEmailBody(user);

        await client.SendMailAsync(email, user.Email, subject, body);
    }

    private static string GetEmailBody(User user)
    {
        string body = $@"
            <h2>Thank you for submitting the contact form.</h2>
            <p><strong>First Name:</strong> {user.FirstName}</p>
            <p><strong>Last Name:</strong> {user.LastName}</p>
            <p><strong>Email:</strong> {user.Email}</p>";

        if (user.Address is not null)
        {
            body += $@"
                <p><strong>Address:</strong> {user.Address}</p>
                <p><strong>Website:</strong> {user.Website}</p>
                <p><strong>Phone:</strong> {user.Phone}</p>";
        }

        return body;
    }
}