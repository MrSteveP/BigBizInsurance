using System;
using System.Threading.Tasks;
using UserManagement.Application.Persistance.Abstraction;

namespace UserManagement.Application.Persistance
{
    public interface IUserManagementUnitOfWork //: IUnitOfWork
    {
        IPolicyRepository PolicyRepository { get; }
        IApplicationPolicyRoleRepository ApplicationPolicyRoleRepository { get; }
        int Save(Guid? userId = null, bool validateOnSaveEnabled = true);
      //  int Save();
      // Task<int> SaveAsync();
    }
}
