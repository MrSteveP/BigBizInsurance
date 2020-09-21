using CrossCutting.Core.Extensions;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;
namespace UserManagement.Domain.Mapper
{
    public static class UserMapper
    {
        public static ApplicationUser MapToModel(UserViewModel viewModel)
        {
            if (viewModel == null) return null;

            var user = new ApplicationUser(viewModel.UserName);
            user.Address = viewModel.Address;
            user.BirthDate = viewModel.BirthDateString.ToDateTime();
            user.Email = viewModel.Email;
            user.FirstName = viewModel.FirstName;
            user.GenderId = viewModel.GenderId;
            user.LastName = viewModel.LastName;
            user.MiddleName = viewModel.MiddleName;
            user.PhoneNumber = viewModel.PhoneNumber;
            user.UserName = viewModel.UserName;

            if (viewModel.Id.HasValue)
                user.Id = viewModel.Id.Value;

            return user;
        }

        public static UserViewModel MapToViewModel(ApplicationUser model)
        {
            if (model == null) return null;

            return new UserViewModel()
            {
                Id = model.Id,
                UserName = model.UserName,
                Address = model.Address,
                BirthDate = model.BirthDate,
                BirthDateString = model.BirthDate?.Date.DateToString(),
                Email = model.Email,
                FirstName = model.FirstName,
                GenderId = model.GenderId,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                PhoneNumber = model.PhoneNumber,
                Active = model.LockoutEnd == null
            };
        }
    }
}
