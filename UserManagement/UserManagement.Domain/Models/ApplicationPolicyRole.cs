using CrossCutting.Persistance;
using System;

namespace UserManagement.Domain.Models
{
    public class ApplicationPolicyRole:IBaseModel
    {
        public int Id { set; get; }
        public int ApplicationPolicyId { set; get; }
        public Guid ApplicationRoleId { set; get; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public ApplicationPolicy ApplicationPolicy { set; get; }
        public ApplicationRole ApplicationRole { set; get; }
    }
}
