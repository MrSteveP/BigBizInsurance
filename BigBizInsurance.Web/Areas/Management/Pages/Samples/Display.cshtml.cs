using Microsoft.AspNetCore.Mvc;
using BigBizInsurance.Application;
using BigBizInsurance.Application.Services.Abstraction;
using BigBizInsurance.Application.Services.Concrete;
using BigBizInsurance.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using System;
using System.Threading.Tasks;
using SampleResources = BigBizInsurance.Localization.SampleResources.SampleResources;
using SharedResources = Common.Localization.Shared.Shared;

namespace BigBizInsurance.Web.Areas.Management.Samples
{
    public class DisplayModel : BasePageModel
    {
        public DisplayModel(ISampleService sampleService)
        {
            Title = SampleResources.Create_Title;
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

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(NavConstants.ManagementAreas.Pages.SamplesIndex, new { area = NavConstants.ManagementAreas.Area });
        }
        #endregion
    }
}
