using CrossCutting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using PoliciesManagementResources = UserManagement.Localization.PoliciesManagement.PoliciesManagement;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Policies
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IPolicyService policyService)
        {
            Title = PoliciesManagementResources.Index_Title;

            PolicyService = policyService;
        }

        #region Dependencies
        public IPolicyService PolicyService { get; }
        #endregion

        #region Get
        public void OnGet()
        {
        }
        #endregion

        #region Post
        public async Task<JsonResult> OnPostSearch(PolicyFilterViewModel model)
        {
            try
            {
                model = await PolicyService.Search(model);
                return new JsonResult(new { Result = CommonConstants.JTableConstants.OK, Records = model.Items, TotalRecordCount = model.TotalCount });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = CommonConstants.JTableConstants.ERROR, ex.Message });
            }
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Index", new { area = "UserManagementAdmin" });
        }
        #endregion

    }
}
