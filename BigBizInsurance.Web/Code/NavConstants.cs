using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBizInsurance.Web.Code
{
    public class NavConstants
    {
        public const string Area = "";
        public class Pages
        {
            public const string Index = "/Index";
        }
            public static class ManagementAreas
        {
            public const string Area = "Management";
            public class Pages
            {
                public const string Index = "/Index";

                public const string SamplesIndex = "/Samples/Index";
                public const string SamplesCreate = "/Samples/Create";
                public const string SamplesEdit = "/Samples/Edit";
                public const string SamplesDisplay = "/Samples/Display";

                public const string AuditIndex = "/Audits/Index";
                public const string AuditDisplay = "/Audits/Display";


            }
        }

    }
}
