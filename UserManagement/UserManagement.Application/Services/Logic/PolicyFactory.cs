using CrossCutting.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Persistance;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;

namespace UserManagement.Application.Services.Logic
{
    internal static class PolicyFactory
    {
        public static IRequestLogic<PolicyViewModel> CreateInstance(int? requestId,
            IUserManagementUnitOfWork unitOfWork, AuthorizationOptions _options, RoleManager<ApplicationRole> roleManager)
        {
            if (requestId == null)
                return new PolicyCreation(unitOfWork, _options, roleManager);
            else
                return new PolicyModification(unitOfWork, _options, roleManager);

        }
    }
}
