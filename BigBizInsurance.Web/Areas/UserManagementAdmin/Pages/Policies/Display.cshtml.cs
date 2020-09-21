using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using PoliciesManagementResources = UserManagement.Localization.PoliciesManagement.PoliciesManagement;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Policies
{
    public class DisplayModel : BasePageModel
    {
        public DisplayModel(IPolicyService policyService)
        {
            Title = PoliciesManagementResources.Display_Title;
            PolicyService = policyService;
        }

        #region Dependencies
        public IPolicyService PolicyService { get; }
        #endregion

        #region Properties
        [BindProperty]
        public PolicyViewModel Input { set; get; } = new PolicyViewModel();

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
            return Page();
        }
        #endregion

        #region Post

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Policies/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
