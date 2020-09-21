using CrossCutting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using RolesManagementResources = UserManagement.Localization.RolesManagement.RolesManagement;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Roles
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IRolesService rolesService)
        {
            Title = RolesManagementResources.Index_Title;

            RolesService = rolesService;
        }
        #region Dependencies
        public IRolesService RolesService { get; }
        #endregion

        #region Get
        public void OnGet()
        {
        }
        #endregion

        #region Post
        public async Task<JsonResult> OnPostSearch(RoleFilterViewModel model)
        {
            try
            {
                model = await RolesService.Search(model);
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
