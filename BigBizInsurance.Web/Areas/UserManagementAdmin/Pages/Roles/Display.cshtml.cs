using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using RolesManagementResources = UserManagement.Localization.RolesManagement.RolesManagement;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Roles
{
    public class DisplayModel : BasePageModel
    {
        public DisplayModel(IRolesService rolesService)
        {
            Title = RolesManagementResources.Display_Title;
            RolesService = rolesService;
        }

        #region Dependencies
        public IRolesService RolesService { get; }
        #endregion

        #region Properties
        [BindProperty]
        public RoleViewModel Input { set; get; } = new RoleViewModel();
        #endregion

        #region Get
        public async Task<IActionResult> OnGet(string roleId)
        {
            var result = await RolesService.GetById(roleId);
            ModelState.Merge(result);

            if (!ModelState.IsValid)
            {
                ShowMessage(CoreEnumerations.MessageTypes.danger, ModelState.GetErrors(" , "));

                return RedirectToPage("/Roles/Index", new { area = "UserManagementAdmin" });
            }
            Input = result.Value;
            return Page();
        }
        #endregion

        #region Post
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Roles/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
