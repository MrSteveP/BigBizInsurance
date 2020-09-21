using Microsoft.Extensions.DependencyInjection;
using System;

namespace UserManagement.Application.Authorization
{
    public interface IAuthorizePolicies
    {
        IDisposable AddAuthorizationPolicies(IServiceCollection services);
    }
}