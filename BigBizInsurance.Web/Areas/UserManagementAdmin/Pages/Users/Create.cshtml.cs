using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using UsersManagementResources = UserManagement.Localization.UsersManagement.UsersManagement;
using UsersResources = UserManagement.Localization.Users.UsersResources;
using UserManagement.Domain.Models;
using Microsoft.AspNetCore.Http.Extensions;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Users
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(IUsersService usersService, IRolesService rolesService)
        {
            Title = UsersManagementResources.Create_Title;

            UsersService = usersService;
            RolesService = rolesService;
        }


        #region Dependencies
        public IUsersService UsersService { get; }
        public IRolesService RolesService { get; }
        #endregion

        #region Binding Properties
        [BindProperty]
        public UserViewModel Input { set; get; } = new UserViewModel();

        public List<RoleViewModel> RolesList { set; get; } = new List<RoleViewModel>();

        [BindProperty]
        public string ReturnUrl { set; get; } = string.Empty;

        [BindProperty]
        public string RoleName { set; get; } = string.Empty;
        #endregion

        #region Get
        public void OnGet(string returnUrl = "", string roleName = "")
        {
            if (!string.IsNullOrEmpty(returnUrl))
                ReturnUrl = returnUrl;

            if (!string.IsNullOrEmpty(roleName))
                RoleName = roleName;

            LoadRoles(RoleName);
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


            var result = await UsersService.Add(new ReturnResult<UserViewModel>() { Value = Input });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
            {
                LoadRoles(RoleName);
                return Page();
            }


            if (string.IsNullOrEmpty(ReturnUrl))
            {
                ShowMessage(CoreEnumerations.MessageTypes.info, UsersResources.UserCreatedSuccessfully);
                return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
            }
            else
            {
                ShowMessage(CoreEnumerations.MessageTypes.info, UsersResources.UserCreatedSuccessfullyStepOne);
                return Redirect($"{ReturnUrl}/{result.Value.Id}");
            }

        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
