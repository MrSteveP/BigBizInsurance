using System;
using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Mapper;
using UserManagement.Domain.Models;

namespace UserManagement.Domain.ViewModels
{
    public class RoleViewModel
    {
        public Guid? Id { set; get; }

        [Required]
        public string Name { set; get; }

        public static implicit operator ApplicationRole(RoleViewModel model)
        {
            return RoleMapper.MapToModel(model);
        }

        public static implicit operator RoleViewModel(ApplicationRole role)
        {
            return RoleMapper.MapToViewModel(role);
        }
    }
}
