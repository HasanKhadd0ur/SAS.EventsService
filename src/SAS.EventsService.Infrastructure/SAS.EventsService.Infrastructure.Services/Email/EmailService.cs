using SAS.EventsService.Application.Contracts.Email;

namespace SAS.EventsService.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        public Task SendBulkEmailsAsync(IEnumerable<string> recipient, string subject, string body)
        {
            Console.WriteLine("a messages sended " + "|| body is " + body + "|| subject is" + subject);
            return Task.CompletedTask;

        }

        public Task SendEmailAsync(string recipient, string subject, string body)
        {
            Console.WriteLine("a message sended to " + recipient + "|| body is " + body + "|| subject is" + subject);
            return Task.CompletedTask;

        }
    }
}
