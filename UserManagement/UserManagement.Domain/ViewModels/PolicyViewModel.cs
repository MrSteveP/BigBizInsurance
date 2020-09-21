using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Mapper;
using UserManagement.Domain.Models;

namespace UserManagement.Domain.ViewModels
{
    public class PolicyViewModel
    {
        public int? Id { set; get; }

        [Required]
        public string Name { set; get; }

        public List<Guid> SelectedRoleIds { set; get; }
        public List<RoleViewModel> Roles { set; get; }

        public static implicit operator ApplicationPolicy(PolicyViewModel model)
        {
            return PolicyMapper.MapToModel(model);
        }

        public static implicit operator PolicyViewModel(ApplicationPolicy policy)
        {
            return PolicyMapper.MapToViewModel(policy);
        }
    }
}
