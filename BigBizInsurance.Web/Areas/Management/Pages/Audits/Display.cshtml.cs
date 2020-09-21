using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using BigBizInsurance.Application;
using BigBizInsurance.Application.Services.Abstraction;
using BigBizInsurance.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using System.Threading.Tasks;
using AuditResources = BigBizInsurance.Localization.AuditResources.AuditResources;
using SharedResources = Common.Localization.Shared.Shared;

namespace BigBizInsurance.Web.Areas.Management.Pages.Audits
{
    public class DisplayModel : BasePageModel
    {
        public DisplayModel(IAuditTrailService auditTrailService)
        {
            Title = AuditResources.Display_Title;
            AuditTrailService = auditTrailService;
        }

        #region Dependencies
        public IAuditTrailService AuditTrailService { get; }

        #endregion

        #region Properties
        [BindProperty]
        public AuditTrailDto Input { set; get; } = new AuditTrailDto();

        #endregion

        #region Get
        public async Task<IActionResult> OnGet(int id)
        {
            var result = await AuditTrailService.GetById(id);

            if (result == null)
            {
                ShowMessage(CoreEnumerations.MessageTypes.danger, SharedResources.NoDataToDisplay);
                return RedirectToPage(NavConstants.ManagementAreas.Pages.AuditIndex, new { area = NavConstants.ManagementAreas.Area });
            }

            Input = result;
            return Page();
        }
        #endregion

        #region Post

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(NavConstants.ManagementAreas.Pages.AuditIndex, new { area = NavConstants.ManagementAreas.Area });
        }
        #endregion
    }
}
