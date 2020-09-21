using CrossCutting.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BigBizInsurance.Application;
using BigBizInsurance.Application.Extensions;
using BigBizInsurance.Application.Services.Abstraction;
using BigBizInsurance.Application.Services.Concrete;
using BigBizInsurance.Domain.Models;
using BigBizInsurance.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;
using UserManagement.Localization.Users;
using TeachersResources = BigBizInsurance.Localization.SampleResources.SampleResources;
using SharedResources = Common.Localization.Shared.Shared;

namespace BigBizInsurance.Web.Areas.Management.Samples
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(ISampleService sampleService)
        {
            Title = TeachersResources.Create_Title;
            SampleService = sampleService;
        }

        #region Dependencies
        public ISampleService SampleService { get; }
        #endregion

        #region Properties
        [BindProperty]
        public SampleViewModel Input { set; get; } = new SampleViewModel();


        #endregion

        #region Get
        public async Task<IActionResult> OnGet()
        {

            return Page();
        }

        #endregion

        #region Post
        public async Task<IActionResult> OnPostSubmit()
        {
            if (!ModelState.IsValid)
                return Page();


            var result = await SampleService.Save(true, new ReturnResult<SampleViewModel>() { Value = Input });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
                return Page();


            ShowMessage(CoreEnumerations.MessageTypes.info, SharedResources.SaveSuccess);

            return RedirectToPage(NavConstants.ManagementAreas.Pages.SamplesIndex, new { area = NavConstants.ManagementAreas.Area });
        }

        public IActionResult OnPostBack()
        {
            return RedirectToPage(NavConstants.ManagementAreas.Pages.SamplesIndex, new { area = NavConstants.ManagementAreas.Area });
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(NavConstants.ManagementAreas.Pages.SamplesIndex, new { area = NavConstants.ManagementAreas.Area });
        }
        #endregion
    }
}
