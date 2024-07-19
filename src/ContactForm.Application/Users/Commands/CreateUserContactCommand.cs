using ErrorOr;
using MediatR;

namespace ContactForm.Application.Users.Commands;

public sealed record CreateUserContactCommand(string FirstName, string LastName, string Email) : IRequest<ErrorOr<Created>>;