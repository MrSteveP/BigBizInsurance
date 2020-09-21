using CrossCutting.Persistance.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Application.Persistance.Abstraction;
using UserManagement.Domain.Models;

namespace UserManagement.Persistance.Concrete
{
  public sealed  class PolicyRepository : Repository<ApplicationPolicy>, IPolicyRepository
    {
        public PolicyRepository(ApplicationDbContext context)
            : base(context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }
    }
}
