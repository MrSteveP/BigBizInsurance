using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using RolesManagementResources = UserManagement.Localization.RolesManagement.RolesManagement;
using SharedResources = Common.Localization.Shared.Shared;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Roles
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(IRolesService rolesService)
        {
            Title = RolesManagementResources.Create_Title;
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
        public void OnGet()
        {
        }
        #endregion

        #region Post
        public async Task<IActionResult> OnPostSubmit()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await RolesService.Add(new ReturnResult<RoleViewModel>() { Value = Input });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
                return Page();

            ShowMessage(CoreEnumerations.MessageTypes.info, SharedResources.CreateSuccess);

            return RedirectToPage("/Roles/Index", new { area = "UserManagementAdmin" });
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Roles/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
