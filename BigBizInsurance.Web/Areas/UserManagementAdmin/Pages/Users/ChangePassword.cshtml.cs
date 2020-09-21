using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using UsersManagementResources = UserManagement.Localization.UsersManagement.UsersManagement;
using UsersResources = UserManagement.Localization.Users.UsersResources;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Users
{
    public class ChangePasswordModel : BasePageModel
    {
        public ChangePasswordModel(IUsersService usersService)
        {
            Title = UsersManagementResources.ChangePassword_Title;

            UsersService = usersService;
        }

        #region Dependencies
        public IUsersService UsersService { get; }
        #endregion

        #region Properties
        [BindProperty]
        public UserViewModel Input { set; get; } = new UserViewModel();

        #endregion

        #region Get
        public async Task<IActionResult> OnGet(string userId)
        {

            var result = await UsersService.GetById(userId);
            if (!result.IsValid)
            {
                ShowMessage(CoreEnumerations.MessageTypes.danger, result.Errors
                    .Select(a => a.Value).Aggregate((a1, a2) => a1 + "," + a2).ToString());

                return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
            }
            Input = result.Value;

            return Page();
        }
        #endregion

        #region Post
        public async Task<IActionResult> OnPostSubmit()
        {
            ModelState.Remove("Input.UserName");
            ModelState.Remove("Input.FirstName");
            ModelState.Remove("Input.MiddleName");
            ModelState.Remove("Input.LastName");
            ModelState.Remove("Input.BirthDateString");
            ModelState.Remove("Input.Address");
            ModelState.Remove("Input.GenderId");
            ModelState.Remove("Input.PhoneNumber");
            ModelState.Remove("Input.Email");


            if (!ModelState.IsValid)
                return Page();

            var result = await UsersService.ChangePassword(new ReturnResult<UserViewModel>() { Value = Input });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
                return Page();

            ShowMessage(CoreEnumerations.MessageTypes.info, UsersResources.ChangePasswordSuccessfully);

            return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
        }


        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
