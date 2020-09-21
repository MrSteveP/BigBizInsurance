using AuditTrailComponent.Services.DTOs;
using System.Threading.Tasks;

namespace AuditTrailComponent.Services.Abstraction
{
    public interface IErrorLogPublicService
    {
        Task<ErrorLogFilterDto> Search(ErrorLogFilterDto model);
    }
}
