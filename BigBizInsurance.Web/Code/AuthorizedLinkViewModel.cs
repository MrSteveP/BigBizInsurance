using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace BigBizInsurance.Web.Code
{
    public class AuthorizedLinkViewModel
    {
        public AuthorizedLinkViewModel(string area, string page,Dictionary<string,string> allRouteData, string linkName,
            string htmlIcon, string parentElementStart = "", string parentElementEnd = "", string cssClass = "")
        {
            Area = area;
            Page = page;
            AllRouteData = allRouteData;
            LinkName = linkName;
            HtmlIcon = htmlIcon;
            ParentElementStart = parentElementStart;
            ParentElementEnd = parentElementEnd;
            CssClass = cssClass;
        }
        public string Area { set; get; }
        public string Page { set; get; }
        public Dictionary<string, string> AllRouteData { get; }
        public string LinkName { set; get; }
        // [AllowHtml]
        public string HtmlIcon { set; get; }

        // [AllowHtml]
        public string ParentElementStart { set; get; }

        // [AllowHtml]
        public string ParentElementEnd { set; get; }
        public string CssClass { get; }
    }
}
