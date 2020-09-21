using System;
using System.Threading.Tasks;

namespace AuditTrailComponent.Services.Abstraction
{
    internal interface IErrorLogInternalService
    {
        Task Log(Exception ex);
    }
}
