using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using RolesManagementResources = UserManagement.Localization.RolesManagement.RolesManagement;
using SharedResource = Common.Localization.Shared.Shared;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Roles
{
    public class EditModel : BasePageModel
    {
        public EditModel(IRolesService rolesService)
        {
            Title = RolesManagementResources.Edit_Title;
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
            var result =await RolesService.GetById(roleId);
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
        public async Task<IActionResult> OnPostSubmit()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await RolesService.Update(new ReturnResult<RoleViewModel>() { Value = Input });
            ModelState.Merge(result);
            
            if (!ModelState.IsValid)
                return Page();

            ShowMessage(CoreEnumerations.MessageTypes.info, SharedResource.EditSuccess);

            return RedirectToPage("/Roles/Index", new { area = "UserManagementAdmin" });
        }

        public async Task<IActionResult> OnPostRemove(Guid id)
        {
            ModelState.Remove("Input.Name");

            var result = await RolesService.Delete(new ReturnResult<Guid> { Value = id });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
                return Page();

            ShowMessage(CoreEnumerations.MessageTypes.info, SharedResource.DeleteSuccess);
            return RedirectToPage("/Roles/Index", new { area = "UserManagementAdmin" });

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Roles/Index", new { area = "UserManagementAdmin" });
        }

        #endregion
    }
}
