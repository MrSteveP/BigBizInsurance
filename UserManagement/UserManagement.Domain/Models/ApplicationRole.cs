using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace UserManagement.Domain.Models
{
    public  class ApplicationRole:IdentityRole<Guid>
    {
        public ApplicationRole()
        {
            ApplicationPolicyRoles = new HashSet<ApplicationPolicyRole>();
        }

        public ApplicationRole(string roleName) :base(roleName)
        {
            ApplicationPolicyRoles = new HashSet<ApplicationPolicyRole>();
        }

        public ICollection<ApplicationPolicyRole> ApplicationPolicyRoles { set; get; }

    }
}
