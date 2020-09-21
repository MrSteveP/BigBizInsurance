using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using UsersManagementResources = UserManagement.Localization.UsersManagement.UsersManagement;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Users
{
    public class DisplayModel : BasePageModel
    {
        public DisplayModel(IUsersService usersService)
        {
            Title = UsersManagementResources.Display_Title;

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
            ModelState.Merge(result);

            if (!ModelState.IsValid)
            {
                ShowMessage(CoreEnumerations.MessageTypes.danger, ModelState.GetErrors(" , "));
                return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
            }
            Input = result.Value;
            return Page();
        }
        #endregion

        #region Post
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Users/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
