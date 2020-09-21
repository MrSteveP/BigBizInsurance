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
    public class CreateModel : BasePageModel
    {
        public CreateModel(IPolicyService policyService,IRolesService rolesService)
        {
            Title = PoliciesManagementResources.Create_Title;
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
        public void OnGet()
        {
            RolesList = RolesService.GetAll();

        }
        #endregion

        #region Post
        public async Task<IActionResult> OnPostSubmit()
        {
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

            ShowMessage(CoreEnumerations.MessageTypes.info, SharedResources.CreateSuccess);

            return RedirectToPage(UserManagementNavConstants.Pages.PolicyIndex, new { area = UserManagementNavConstants.Area});
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Policies/Index", new { area = "UserManagementAdmin" });
        }
        #endregion

    }
}
