using System.Collections.Generic;

namespace UserManagement.Domain.DTOs
{
    public  class PolicyPagesDto
    {
        public string PolicyName { set; get; }
        public List<string> Pages { set; get; }
    }
}
