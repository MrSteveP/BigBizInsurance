using AuditTrailComponent.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuditTrailComponent.Services.Abstraction
{
    public interface IEmailService
    {
        Task<EmailServiceResultDto> SendAsync(string from, List<string> to, string subject, string body);
    }
}
