using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;

namespace UserManagement.Domain.Mapper
{
    public static   class PolicyMapper
    {
        public static ApplicationPolicy MapToModel(PolicyViewModel viewModel)
        {
            if (viewModel == null) return null;

            var policy = new ApplicationPolicy();
            policy.Name = viewModel.Name?.Replace(" ","");
            if (viewModel.Id.HasValue)
                policy.Id = viewModel.Id.Value;

            return policy;
        }

        public static PolicyViewModel MapToViewModel(ApplicationPolicy model)
        {
            if (model == null) return null;

            return new PolicyViewModel()
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}
