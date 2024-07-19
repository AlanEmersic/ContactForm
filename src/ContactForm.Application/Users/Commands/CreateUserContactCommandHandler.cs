using ContactForm.Application.Users.DTO;
using ContactForm.Application.Users.Mappings;
using ContactForm.Application.Users.Services;
using ContactForm.Domain.Users.Entities;
using ContactForm.Domain.Users.Repositories;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace ContactForm.Application.Users.Commands;

internal sealed class CreateUserContactCommandHandler : IRequestHandler<CreateUserContactCommand, ErrorOr<Created>>
{
    private readonly IUserRepository userRepository;
    private readonly IHttpClientFactory clientFactory;
    private readonly IEmailService emailService;
    private readonly ILogger<CreateUserContactCommandHandler> logger;

    public CreateUserContactCommandHandler(IUserRepository userRepository, IHttpClientFactory clientFactory, IEmailService emailService, ILogger<CreateUserContactCommandHandler> logger)
    {
        this.userRepository = userRepository;
        this.clientFactory = clientFactory;
        this.logger = logger;
        this.emailService = emailService;
    }

    public async Task<ErrorOr<Created>> Handle(CreateUserContactCommand command, CancellationToken cancellationToken)
    {
        User? existingUser = await userRepository.GetByEmailAsync(command.Email);

        bool userContactAlreadyCreatedInLastMinute = existingUser is not null && (DateTime.UtcNow - existingUser.CreatedDate).TotalMinutes < 1;

        if (userContactAlreadyCreatedInLastMinute)
        {
            return Error.Validation(description: "Contact already submitted within the last minute.");
        }

        UserServiceDto? userServiceDto = await GetUserFromServiceAsync(command.Email, cancellationToken);

        try
        {
            User user = userServiceDto is not null ? command.MapToDomain(userServiceDto) : command.MapToDomain();

            await userRepository.AddAsync(user);

            //await emailService.SendEmailAsync(user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create user contact with email: {Email}.", command.Email);

            return Error.Failure(description: "Failed to create user contact.");
        }

        return Result.Created;
    }

    private async Task<UserServiceDto?> GetUserFromServiceAsync(string email, CancellationToken cancellationToken)
    {
        HttpClient client = clientFactory.CreateClient();

        string requestUri = $"http://jsonplaceholder.typicode.com/users?email={email}";

        try
        {
            List<UserServiceDto>? response = await client.GetFromJsonAsync<List<UserServiceDto>>(requestUri, cancellationToken);

            return response?.FirstOrDefault();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get user from service with email: {Email}.", email);

            return null;
        }
    }
}