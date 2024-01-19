using Application.Abstractions;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    public Task SendEmail(string destination, string subject, string content)
    {
        return Task.CompletedTask;
    }
}
