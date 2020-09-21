using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigBizInsurance.Web.Code
{
    public class EmailService : IEmailService
    {
        public async Task<EmailServiceResultDto> SendAsync(string from, List<string> to, string subject, string body)
        {
            return new EmailServiceResultDto();
        }
    }
}
