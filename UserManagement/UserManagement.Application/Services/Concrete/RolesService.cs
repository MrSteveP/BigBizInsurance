using CrossCutting.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Persistance;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.Mapper;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;
using UsersResources = UserManagement.Localization.Users.UsersResources;
using RoleManagementResources = UserManagement.Localization.RolesManagement.RolesManagement;

namespace UserManagement.Application.Services.Concrete
{
    public sealed class RolesService : IRolesService
    {
        public RolesService(RoleManager<ApplicationRole> roleManager, IUserManagementUnitOfWork userManagementUnitOfWork)
        {
            RoleManager = roleManager;
            UserManagementUnitOfWork = userManagementUnitOfWork;
        }

        public RoleManager<ApplicationRole> RoleManager { get; }
        public IUserManagementUnitOfWork UserManagementUnitOfWork { get; }

        public async Task<ReturnResult<RoleViewModel>> GetById(string roleId)
        {
            ReturnResult<RoleViewModel> result = new ReturnResult<RoleViewModel>();

            var role = await RoleManager.FindByIdAsync(roleId);
            result.Value = role;

            if (role == null)
                result.AddErrorItem("", UsersResources.RoleNotFound);

            return result;

        }

        public List<RoleViewModel> GetAll()
        {
            return RoleManager.Roles.Select(role => RoleMapper.MapToViewModel(role)).ToList();
        }

        public async Task<ReturnResult<RoleViewModel>> Add(ReturnResult<RoleViewModel> model)
        {
            var result = await RoleManager.CreateAsync(model.Value);

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error =>
                {
                    model.AddErrorItem("", $"{error.Code} : {error.Description}");
                });
            }

            return model;
        }

        public async Task<ReturnResult<RoleViewModel>> Update(ReturnResult<RoleViewModel> model)
        {
            var role = await RoleManager.FindByIdAsync(model.Value.Id.ToString());
            role.Name = model.Value.Name;

            var result = await RoleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error =>
                {
                    model.AddErrorItem("", $"{error.Code} : {error.Description}");
                });
            }

            return model;
        }

        public async Task<ReturnResult<Guid>> Delete(ReturnResult<Guid> model)
        {
            var role = await RoleManager.FindByIdAsync(model.Value.ToString());

            model = await CanDeleteRole(model);
            if (model.IsValid)
            {
                var result = await RoleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(error =>
                    {
                        model.AddErrorItem("", $"{error.Code} : {error.Description}");
                    });
                }
            }

            return model;
        }

        private async Task<ReturnResult<Guid>> CanDeleteRole(ReturnResult<Guid> model)
        {
            var relatedToPolicy = await UserManagementUnitOfWork.ApplicationPolicyRoleRepository.AnyAsync(a => a.ApplicationRoleId == model.Value);
            if (relatedToPolicy)
                model.AddErrorItem("", RoleManagementResources.msg_RoleIsRelatedToPolicy);

            return model;
        }
        public async Task<RoleFilterViewModel> Search(RoleFilterViewModel model)
        {
            var query = RoleManager.Roles;

            if (!string.IsNullOrEmpty(model.Name))
                query = query.Where(m => m.Name.Contains(model.Name.ToLower()));

            List<RoleViewModel> items = new List<RoleViewModel>();

            model.TotalCount = query.Count();
            if (model.jtPageSize > 0)
                items = await query.Skip((int)(model.PageNumber * model.jtPageSize)).Take((int)model.jtPageSize)
                    .Select(role => RoleMapper.MapToViewModel(role)).ToListAsync();
            else
                items = await query.Select(role => RoleMapper.MapToViewModel(role)).ToListAsync();

            model.Items = items;

            return model;
        }
    }
}
