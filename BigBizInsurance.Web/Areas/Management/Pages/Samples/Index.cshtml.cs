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
using UserManagement.Application.Services.Abstraction;
using SampleResources = BigBizInsurance.Localization.SampleResources.SampleResources;

namespace BigBizInsurance.Web.Areas.Management.Teachers
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(ISampleService sampleService )
        {
            Title = SampleResources.Index_Title;

            SampleService = sampleService;
        }

        #region Dependencies
        public ISampleService SampleService { get; }
        #endregion

        #region Properties

        #endregion
        #region Get
        public void OnGet()
        {
        }
        #endregion

        #region Post
        public async Task<JsonResult> OnPostSearch(SampleFilterViewModel model)
        {
            try
            {
                model = await SampleService.Search(model);
                return new JsonResult(new { Result = CommonConstants.JTableConstants.OK, Records = model.Items, TotalRecordCount = model.TotalCount });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Result = CommonConstants.JTableConstants.ERROR, ex.Message });
            }
        }

        public async Task<JsonResult> OnPostDelete(int id)
        {
            try
            {
                var result = await SampleService.Delete(new ReturnResult<int> { Value = id });

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
            return RedirectToPage("/Index", new { area = "" });
        }
        #endregion
    }
}
