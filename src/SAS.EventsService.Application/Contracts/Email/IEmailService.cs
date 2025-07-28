using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Application.Contracts.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string subject, string body);
        Task SendBulkEmailsAsync(IEnumerable<string> recipient, string subject, string body);

    }
}
