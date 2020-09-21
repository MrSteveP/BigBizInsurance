using System.Collections.Generic;

namespace UserManagement.Application.Authorization.Models
{
    public class SiteFolder
    {
        public string FolderName { set; get; }
        public string Path { get; set; }
        public string AreaName { get; set; }
        public string PolicyName { get; set; }
        public HashSet<SiteFolder> SiteFolders { get; set; } = new HashSet<SiteFolder>();
        public HashSet<SitePage>  SitePages { set; get; } = new HashSet<SitePage>();
    }
}
