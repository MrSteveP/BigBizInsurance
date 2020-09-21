using CrossCutting.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BigBizInsurance.Application;
using BigBizInsurance.Application.Extensions;
using BigBizInsurance.Application.Services.Abstraction;
using BigBizInsurance.Domain.Models;
using BigBizInsurance.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;
using UserManagement.Localization.Users;
using SharedResources = Common.Localization.Shared.Shared;
using SampleResources = BigBizInsurance.Localization.SampleResources.SampleResources;

namespace BigBizInsurance.Web.Areas.Management.Samples
{
    public class EditModel : BasePageModel
    {
        public EditModel(ISampleService sampleService )
        {
            Title = SampleResources.Edit_Title;
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
        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
            {
                ShowMessage(CrossCutting.Core.CoreEnumerations.MessageTypes.danger, SharedResources.NoDataToDisplay);
                return Page();
            }

            await FillInput(id);

            return Page();
        }

      
        private async Task FillInput(int? id)
        {
            var model = await SampleService.GetById(id.Value);
            if (model.Value != null)
                Input = model.Value;
        }
        #endregion

        #region Post
        public async Task<IActionResult> OnPostSubmit()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await SampleService.Save(false, new ReturnResult<SampleViewModel>() { Value = Input });
            ModelState.Merge(result);

            if (!ModelState.IsValid)
                return Page();

            ShowMessage(CoreEnumerations.MessageTypes.info, SharedResources.SaveSuccess);
            return RedirectToPage(NavConstants.ManagementAreas.Pages.SamplesIndex,new{area = NavConstants.ManagementAreas.Area});
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
