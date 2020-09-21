using CrossCutting.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UserManagement.Application.Persistance;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;
using PolicyResources = UserManagement.Localization.PoliciesManagement.PoliciesManagement;

namespace UserManagement.Application.Services.Logic
{
    internal sealed class PolicyModification : IRequestLogic<PolicyViewModel>
    {
        public PolicyModification(IUserManagementUnitOfWork userManagementUnitOfWork,
            AuthorizationOptions _options, RoleManager<ApplicationRole> roleManager)
        {
            UserManagementUnitOfWork = userManagementUnitOfWork;
            Options = _options;
            RoleManager = roleManager;
        }

        public IUserManagementUnitOfWork UserManagementUnitOfWork { get; }
        public AuthorizationOptions Options { get; }
        public RoleManager<ApplicationRole> RoleManager { get; }

        public async Task<ReturnResult<PolicyViewModel>> IsValidAsync(ReturnResult<PolicyViewModel> model)
        {

            bool dbExists = await UserManagementUnitOfWork.PolicyRepository.AnyAsync(a => a.Id != model.Value.Id && a.Name == model.Value.Name);
            if (dbExists) model.AddErrorItem("", PolicyResources.msg_PolicyAlreadyExist);

            if (model.Value.SelectedRoleIds == null) model.AddErrorItem("", PolicyResources.msg_SelectOneRole);

            return model;
        }

        public async Task<ReturnResult<PolicyViewModel>> SaveAsync(ReturnResult<PolicyViewModel> model)
        {
            ApplicationPolicy policy = model.Value;

            var existedRoles = await UserManagementUnitOfWork.ApplicationPolicyRoleRepository.GetAsync(a => a.ApplicationPolicyId == model.Value.Id);
            UserManagementUnitOfWork.ApplicationPolicyRoleRepository.Delete(existedRoles);

            model.Value.SelectedRoleIds.ForEach(roleId =>
            {
                policy.ApplicationPolicyRoles.Add(new ApplicationPolicyRole
                {
                    ApplicationPolicy = policy,
                    ApplicationRoleId = roleId
                });

            });

            UserManagementUnitOfWork.PolicyRepository.Update(policy);
            UserManagementUnitOfWork.Save();

            AuthorizationPolicyCreation authorizationPolicyCreation = new AuthorizationPolicyCreation(Options, RoleManager);
            authorizationPolicyCreation.Create(model.Value.Name, model.Value.SelectedRoleIds);

            return model;
        }


    }
}
