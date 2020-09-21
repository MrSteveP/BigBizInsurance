using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using PoliciesManagementResources = UserManagement.Localization.PoliciesManagement.PoliciesManagement;
using SharedResources = Common.Localization.Shared.Shared;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Policies
{
    public class EditModel : BasePageModel
    {
        public EditModel(IPolicyService policyService,IRolesService rolesService)
        {
            Title = PoliciesManagementResources.Edit_Title;

            PolicyService = policyService;
            RolesService = rolesService;
        }

        #region Dependencies
        public IPolicyService PolicyService { get; }
        public IRolesService RolesService { get; }

        #endregion

        #region Properties
        [BindProperty]
        public PolicyViewModel Input { set; get; } = new PolicyViewModel();
        public List<RoleViewModel> RolesList = new List<RoleViewModel>();

        #endregion

        #region Get
        public async Task<IActionResult> OnGet(int policyId)
        {
            var result = await PolicyService.GetById(policyId);
            ModelState.Merge(result);

            if (!ModelState.IsValid)
            {
                ShowMessage(CoreEnumerations.MessageTypes.danger, ModelState.GetErrors(" , "));

                return RedirectToPage("/Policies/Index", new { area = "UserManagementAdmin" });
            }
            Input = result.Value;
            RolesList = RolesService.GetAll();

            return Page();
        }
        #endregion

        #region Post
        public async Task<IActionResult> OnPostSubmit()
        {
            ModelState.Remove("Input.Name");

            if (!ModelState.IsValid)
            {
                RolesList = RolesService.GetAll();
                return Page();
            }

            var result = await PolicyService.Save(new ReturnResult<PolicyViewModel>() { Value = Input });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
            {
                RolesList = RolesService.GetAll();
                return Page();
            }

            ShowMessage(CoreEnumerations.MessageTypes.info, SharedResources.EditSuccess);

            return RedirectToPage(UserManagementNavConstants.Pages.PolicyIndex, new { area = UserManagementNavConstants.Area });
        }

        public async Task<IActionResult> OnPostRemove(int id)
        {
            ModelState.Remove("Input.Name");

            var result = await PolicyService.Delete(new ReturnResult<int> { Value = id });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
            {
                RolesList = RolesService.GetAll();
                return Page();
            }
            ShowMessage(CoreEnumerations.MessageTypes.info, SharedResources.DeleteSuccess);
            return RedirectToPage("/Policies/Index", new { area = "UserManagementAdmin" });

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Policies/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
