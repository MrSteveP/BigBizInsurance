using System.Collections.Generic;

namespace UserManagement.Application.Authorization.Models
{
    public class SitePolicy
    {
        public string Name { get; set; }
        public HashSet<string> Roles { get; set; }
    }
}
