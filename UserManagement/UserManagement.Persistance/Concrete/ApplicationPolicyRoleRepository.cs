using CrossCutting.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Application.Persistance.Abstraction;
using UserManagement.Domain.Models;

namespace UserManagement.Persistance.Concrete
{
 public sealed   class ApplicationPolicyRoleRepository : Repository<ApplicationPolicyRole>, IApplicationPolicyRoleRepository
    {
        public ApplicationPolicyRoleRepository(ApplicationDbContext context)
            : base(context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }
    }
}
