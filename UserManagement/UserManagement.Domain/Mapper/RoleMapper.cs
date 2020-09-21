using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;

namespace UserManagement.Domain.Mapper
{
    public static class RoleMapper
    {
        public static ApplicationRole MapToModel(RoleViewModel viewModel)
        {
            if (viewModel == null) return null;

            var role = new ApplicationRole(viewModel.Name);
            if (viewModel.Id.HasValue)
                role.Id = viewModel.Id.Value;

            return role;
        }

        public static RoleViewModel MapToViewModel(ApplicationRole model)
        {
            if (model == null) return null;

            return new RoleViewModel()
            {
                Id =model.Id,
                Name = model.Name,
            };
        }
    }
}
