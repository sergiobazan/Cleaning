using Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmail(string destination, string subject, string content)
    {
        _logger.LogInformation($"Email send to {destination} with subject: {subject} and content : {content}");
        return Task.CompletedTask;
    }
}
