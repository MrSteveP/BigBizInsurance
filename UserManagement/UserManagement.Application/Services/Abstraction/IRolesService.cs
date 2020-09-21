using CrossCutting.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.ViewModels;

namespace UserManagement.Application.Services.Abstraction
{
    public interface IRolesService
    {
        Task<ReturnResult<RoleViewModel>> GetById(string roleId);
        List<RoleViewModel> GetAll();
        Task<ReturnResult<RoleViewModel>> Add(ReturnResult<RoleViewModel> model);
        Task<ReturnResult<RoleViewModel>> Update(ReturnResult<RoleViewModel> model);
        Task<ReturnResult<Guid>> Delete(ReturnResult<Guid> model);
        Task<RoleFilterViewModel> Search(RoleFilterViewModel model);
    }
}
