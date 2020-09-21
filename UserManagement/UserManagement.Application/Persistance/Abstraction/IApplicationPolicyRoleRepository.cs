using CrossCutting.Persistance;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Persistance.Abstraction
{
    public  interface IApplicationPolicyRoleRepository : IRepository<ApplicationPolicyRole>
    {
    }
}
