using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using CrossCutting;
using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BigBizInsurance.Application.Services.Abstraction;
using BigBizInsurance.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuditResources = BigBizInsurance.Localization.AuditResources.AuditResources;

namespace BigBizInsurance.Web.Areas.Management.Pages.Audits
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IAuditTrailService auditTrailService)
        {
            Title = AuditResources.Index_Title;
            AuditTrailService = auditTrailService;
        }


        #region Dependencies
        #endregion

        #region Properties
        public IAuditTrailService AuditTrailService { get; }

        #endregion
        #region Get
        public void OnGet()
        {
        }

   
        #endregion

        #region Post
        public async Task<JsonResult> OnPostSearch(AuditTrailFilterDto model)
        {
            try
            {
                model = await AuditTrailService.Search(model);
                return new JsonResult(new { Result = CommonConstants.JTableConstants.OK, Records = model.Items, TotalRecordCount = model.TotalCount });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = CommonConstants.JTableConstants.ERROR, ex.Message });
            }
        }
      
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Index", new { area = "" });
        }
        #endregion
    }
}
