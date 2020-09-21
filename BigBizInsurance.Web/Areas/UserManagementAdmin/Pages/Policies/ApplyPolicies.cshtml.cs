using CrossCutting.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application;
using UserManagement.Application.Authorization;
using UserManagement.Application.Authorization.Models;
using UserManagement.Application.Services.Abstraction;
using BigBizInsurance.Web.Code;
using PoliciesManagementResources = UserManagement.Localization.PoliciesManagement.PoliciesManagement;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Policies
{
    public class ApplyPoliciesModel : BasePageModel
    {
        public ApplyPoliciesModel(IPolicyService policyService, IWebHostEnvironment webHostEnvironment)
        {
            Title = PoliciesManagementResources.ApplyPolicy_Title;
            PolicyService = policyService;
            WebHostEnvironment = webHostEnvironment;

        }

        #region Dependencies
        public IPolicyService PolicyService { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        #endregion

        #region Properties
        [BindProperty]
        public List<SitePage> Input { set; get; } = new List<SitePage>();

        public List<SelectListItem> PoliciesItems { set; get; } = new List<SelectListItem>();

        #endregion

        #region Get
        public async Task OnGet()
        {
            Input = PolicyService.GetAllSitePages(WebHostEnvironment.ContentRootPath);
            await LoadPoliciesItems();

        }

        private async Task LoadPoliciesItems()
        {
            PoliciesItems = (await PolicyService.GetAll())
                .Select(a => new SelectListItem { Value = a.Name.ToString(), Text = a.Name })
                .ToList();

            PoliciesItems.Insert(0, new SelectListItem
            { Value = AuthorizationConfig.AnonymousPolicy, Text = AuthorizationConfig.AnonymousPolicy });
        }
        #endregion

        #region Post
        public async Task<IActionResult> OnPostSubmit()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await PolicyService.SaveApplyPolicyChanges(WebHostEnvironment.ContentRootPath, new ReturnResult<List<SitePage>>() { Value = Input });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
                return Page();

            ShowMessage(CoreEnumerations.MessageTypes.info, PoliciesManagementResources.msg_ApplyPolicy_Saved);

            return RedirectToPage(UserManagementNavConstants.Pages.ApplyPoliciesMessage, new { area = UserManagementNavConstants.Area });
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Policies/Index", new { area = "UserManagementAdmin" });
        }
        #endregion
    }
}
