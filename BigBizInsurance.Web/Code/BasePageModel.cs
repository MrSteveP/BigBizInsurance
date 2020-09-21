using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CrossCutting.Core.CoreEnumerations;

namespace BigBizInsurance.Web.Code
{
    public class BasePageModel : PageModel
    {
        [ViewData]
        public string Title { set; get; }

        public MessageViewModel MessageInfo
        {
            get
            {
                return TempData.Get<MessageViewModel>(TempDataKeys.Message);
            }
        }

        public void ShowMessage(MessageTypes type, string message)
        {
            MessageViewModel messageInfo = new MessageViewModel
            {
                Message = message,
                Type = type
            };
            TempData.Put(TempDataKeys.Message, messageInfo);
        }


        #region Constructors
        public BasePageModel()
        {
        }

        #endregion
    }
}
