using Common.Domain.DTOs;
using Common.Domain.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedResources = Common.Localization.Shared.Shared;

namespace BigBizInsurance.Web.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        #region Bind Properties
        [BindProperty]
        public FullPageMessage FullPageMessage { get; set; }
        #endregion

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


        public ErrorModel()
        {
        }

        public void OnGet(string errorCode = "")
        {
            if (errorCode ==CommonEnumerations.ErrorTypesEnum.PageNotFound.ToString() || errorCode == "404")
            {
                FullPageMessage = new FullPageMessage
                {
                    ErrorCode="404",
                    Message = SharedResources.Error404,
                    Icon = MessageIcon.Exclamation,
                    BackLinkText = SharedResources.HomePage,
                    BackLinkUrl = "/",
                    PageTitle = SharedResources.Error404,
                    Description = string.Empty
                };
            }
            else if (errorCode == CommonEnumerations.ErrorTypesEnum.NotAuthorized.ToString())
            {
                FullPageMessage = new FullPageMessage
                {
                    ErrorCode = "403",
                    Message = SharedResources.Error403,
                    Icon = MessageIcon.Security,
                    BackLinkText = SharedResources.HomePage,
                    BackLinkUrl = "/",
                    PageTitle = SharedResources.Error403,
                    Description = string.Empty
                };
            }
            else
            {
                FullPageMessage = new FullPageMessage
                {
                    ErrorCode = "400",
                    Message = SharedResources.UnexpectedError,
                    Icon = MessageIcon.Error,
                    BackLinkText = SharedResources.HomePage,
                    BackLinkUrl = "/",
                    PageTitle = SharedResources.UnexpectedError,
                    Description = string.Empty
                };
            }
        }

    }
}
