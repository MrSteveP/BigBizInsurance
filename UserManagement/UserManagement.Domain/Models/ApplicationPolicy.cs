using CrossCutting.Persistance;
using System;
using System.Collections.Generic;

namespace UserManagement.Domain.Models
{
    public class ApplicationPolicy : IBaseModel
    {
        public ApplicationPolicy()
        {
            ApplicationPolicyRoles = new HashSet<ApplicationPolicyRole>();
        }
        public int Id { set; get; }
        public string Name { set; get; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public ICollection<ApplicationPolicyRole> ApplicationPolicyRoles { set; get; }

    }
}
