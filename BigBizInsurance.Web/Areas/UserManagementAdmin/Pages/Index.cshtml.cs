using Microsoft.AspNetCore.Mvc;
using System;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.DTOs;
using BigBizInsurance.Web.Code;
using IndexResource = UserManagement.Localization.Index.Index;
using CrossCutting.Core.Extensions;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IDashboardService dashboardService)
        {
            Title = IndexResource.Index_Title;

            DashboardService = dashboardService;
        }
        #region Dependencies
        public IDashboardService DashboardService { get; }
        #endregion

        #region Binding Properties
        public DashboardCardsDto CardsModel { set; get; } = new DashboardCardsDto();
        #endregion

        #region Get
        public void OnGet()
        {

            CardsModel = DashboardService.GetDashboadCards();
        }

        public JsonResult OnGetDashboardData(string from, string to)
        {
            DateTime? FromDate = !string.IsNullOrEmpty(from) ?/* (DateTime?) Convert.ToDateTime(from)*/from.ToDateTime() : null;
            DateTime? ToDate = !string.IsNullOrEmpty(to) ? /*(DateTime?)Convert.ToDateTime(to)*/to.ToDateTime() : null;

            var resultList = DashboardService.GetUsersPerDayForDashboard(FromDate, ToDate);
            return new JsonResult(new { Result = true, data = resultList });
        }
        #endregion

    }
}
