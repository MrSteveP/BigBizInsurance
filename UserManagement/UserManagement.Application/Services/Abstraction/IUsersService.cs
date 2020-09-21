using CrossCutting.Core;
using System;
using System.Threading.Tasks;
using UserManagement.Domain.ViewModels;

namespace UserManagement.Application.Services.Abstraction
{
    public  interface IUsersService
    {
        Task<bool> IsValidUser(string username, string password);
        Task<ReturnResult<UserViewModel>> GetById(string userId);
        Task<ReturnResult<UserViewModel>> Add(ReturnResult<UserViewModel> model);
        Task<ReturnResult<UserViewModel>> Update(ReturnResult<UserViewModel> model);
        Task<ReturnResult<Guid>> Delete(ReturnResult<Guid> model);
        Task<ReturnResult<UserViewModel>> ChangePassword(ReturnResult<UserViewModel> model);
        Task<UserFilterViewModel> Search(UserFilterViewModel model);
        Task<bool> ChangeActivation(string userName, bool activate);
        Task<byte[]> LoadUserImage(string WebRootPath, string userName = "");
    }
}
