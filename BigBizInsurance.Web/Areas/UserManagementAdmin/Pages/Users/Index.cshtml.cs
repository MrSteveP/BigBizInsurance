using CrossCutting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using UsersManagementResources = UserManagement.Localization.UsersManagement.UsersManagement;
using CrossCutting.Core;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Users
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IUsersService usersService)
        {
            Title = UsersManagementResources.Index_Title;

            UsersService = usersService;
        }
        #region Dependencies
        public IUsersService UsersService { get; }
        #endregion

        #region Get
        public void OnGet()
        {
        }
        #endregion

        #region Post
        public async Task<JsonResult> OnPostSearch(UserFilterViewModel model)
        {
            try
            {
                model = await UsersService.Search(model);
                return new JsonResult(new { Result = CommonConstants.JTableConstants.OK, Records = model.Items, TotalRecordCount = model.TotalCount });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = CommonConstants.JTableConstants.ERROR, ex.Message });
            }
        }

        public async Task<JsonResult> OnPostDelete(Guid id)
        {
            try
            {
                var result = await UsersService.Delete(new ReturnResult<Guid> { Value = id });
                ModelState.Merge(result);

                if (ModelState.IsValid)
                    return new JsonResult(new { Result = CommonConstants.JTableConstants.OK });
                else
                    return new JsonResult(new { Result = CommonConstants.JTableConstants.ERROR, Message = ModelState.GetErrors(" , ") });

            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = CommonConstants.JTableConstants.ERROR, Message = ex.Message });
            }
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
