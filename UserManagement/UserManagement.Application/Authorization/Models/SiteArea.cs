using System.Collections.Generic;

namespace UserManagement.Application.Authorization.Models
{
    public class SiteArea
    {
        public string Name { get; set; }
        public string Policy { get; set; }
        public HashSet<SiteFolder> SiteFolders { get; set; }
        public HashSet<SitePage> SitePages { get; set; }

    }
}
