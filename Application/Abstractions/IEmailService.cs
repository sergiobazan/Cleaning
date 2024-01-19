namespace Application.Abstractions;

public interface IEmailService
{
    Task SendEmail(string destination, string subject, string content);
}
