using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using UsersManagementResources = UserManagement.Localization.UsersManagement.UsersManagement;
using UsersResources = UserManagement.Localization.Users.UsersResources;
using UserManagement.Domain.Models;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Users
{
    public class EditModel : BasePageModel
    {
        public EditModel(IUsersService usersService, IRolesService rolesService)
        {
            Title = UsersManagementResources.Edit_Title;

            UsersService = usersService;
            RolesService = rolesService;
        }

        #region Dependencies
        public IUsersService UsersService { get; }
        public IRolesService RolesService { get; }
        #endregion

        #region Bind Properties
        [BindProperty]
        public UserViewModel Input { set; get; } = new UserViewModel();

        public List<RoleViewModel> RolesList = new List<RoleViewModel>();

        [BindProperty]
        public string ReturnUrl { set; get; } = string.Empty;

        [BindProperty]
        public string RoleName { set; get; } = string.Empty;
        #endregion

        #region Get
        public async Task<IActionResult> OnGet(string userId,string returnUrl = "", string roleName = "")
        {

            var result = await UsersService.GetById(userId);
            ModelState.Merge(result);

            if (!ModelState.IsValid)
            {
                ShowMessage(CoreEnumerations.MessageTypes.danger, ModelState.GetErrors(" , "));
                return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
            }

            if (!string.IsNullOrEmpty(returnUrl))
                ReturnUrl = returnUrl;

            if (!string.IsNullOrEmpty(roleName))
                RoleName = roleName;

            Input = result.Value;
            LoadRoles(RoleName);

            return Page();
        }

        private void LoadRoles(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                RolesList = RolesService.GetAll();
        }
        #endregion

        #region Post
        public async Task<IActionResult> OnPostSubmit()
        {
            ModelState.Remove("Input.Password");
            ModelState.Remove("Input.ConfirmPassword");

            if (!ModelState.IsValid)
            {
                LoadRoles(RoleName);
                return Page();
            }

            if (Input.Roles.Contains(Enumerations.RolesEnum.Admins.ToString()) && !User.IsInRole(Enumerations.RolesEnum.Admins.ToString()))
            {
                ModelState.AddModelError("", UsersResources.msg_YouMustBeAdminToCreateAdmins);
                return Page();
            }

            var result = await UsersService.Update(new ReturnResult<UserViewModel>() { Value = Input });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
            {
                LoadRoles(RoleName);
                return Page();
            }


            if (string.IsNullOrEmpty(ReturnUrl))
            {
                ShowMessage(CoreEnumerations.MessageTypes.info, UsersResources.UpdateUserSuccessfully);
                return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
            }
            else
            {
                ShowMessage(CoreEnumerations.MessageTypes.info, UsersResources.UserEditedSuccessfullyStepOne);
                return Redirect($"{ReturnUrl}/{result.Value.Id}");
            }
        }

        //public async Task<IActionResult> OnPostRemove(Guid id)
        //{
        //    ModelState.Remove("Input.Password");
        //    ModelState.Remove("Input.ConfirmPassword");

        //    var result = await UsersService.Delete(new ReturnResult<Guid> { Value = id });
        //    ModelState.Merge(result);

        //    if (!ModelState.IsValid)
        //        return Page();

        //    ShowMessage(CoreEnumerations.MessageTypes.info, UsersResources.DeleteUserSuccess);
        //    return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });

        //}
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
