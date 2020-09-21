using Common.Application.Services.Abstraction;
using Common.Domain.Module;
using CrossCutting.Core;
using CrossCutting.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Domain.Mapper;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;
using UsersResources = UserManagement.Localization.Users.UsersResources;

namespace UserManagement.Application.Services.Concrete
{
    public sealed class UsersService : IUsersService
    {
        public UsersService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAttachmentService attachmentService,
            IHttpContextAccessor httpContextAccessor)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            AttachmentService = attachmentService;
            HttpContextAccessor = httpContextAccessor;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }
        public IAttachmentService AttachmentService { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        public async Task<ReturnResult<UserViewModel>> GetById(string userId)
        {
            ReturnResult<UserViewModel> result = new ReturnResult<UserViewModel>();

            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                result.AddErrorItem("", UsersResources.UserNotFound);
                return result;
            }

            result.Value = user;
            result.Value.Roles = (await UserManager.GetRolesAsync(user)).ToList();

            return result;

        }

        public async Task<ReturnResult<UserViewModel>> Add(ReturnResult<UserViewModel> model)
        {
            var result = await UserManager.CreateAsync(model.Value, model.Value.Password);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByNameAsync(model.Value.UserName);
                user.EmailConfirmed = true;
                await UserManager.UpdateAsync(user);

                await UserManager.AddToRolesAsync(user, model.Value.Roles);
                await AddOrUpdateAttachment(model, user);
                await UserManager.UpdateAsync(user);

                model.Value.Id = user.Id;
            }
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    model.AddErrorItem("", $"{error.Code} : {error.Description}");
                });
            }

            return model;
        }

        private async Task AddOrUpdateAttachment(ReturnResult<UserViewModel> model, ApplicationUser user)
        {
            var attachmentResult = await AttachmentService.AddOrUpdateAttachment(model.Value.File,
                  (int)CommonEnumerations.AttachmentTypes.AvatarImage,
                  HttpContextAccessor.HttpContext, user?.AttachmentId);

            if (attachmentResult.IsValid && attachmentResult.Value != null)
                user.AttachmentId = attachmentResult.Value.Id;

        }

        public async Task<ReturnResult<UserViewModel>> Update(ReturnResult<UserViewModel> model)
        {
            var user = await UserManager.FindByIdAsync(model.Value.Id.ToString());
            user.Address = model.Value.Address;
            user.BirthDate = model.Value.BirthDateString.ToDateTime(); //Convert.ToDateTime(model.Value.BirthDateString,new CultureInfo("en-US"));
            user.Email = model.Value.Email;
            user.FirstName = model.Value.FirstName;
            user.GenderId = model.Value.GenderId;
            user.LastName = model.Value.LastName;
            user.MiddleName = model.Value.MiddleName;
            user.PhoneNumber = model.Value.PhoneNumber;
            user.Address = model.Value.Address;

            if (model.Value.Active)
                await ActivateUser(user);
            else
                await DeactivateUser(user);

            var currentRoles = await UserManager.GetRolesAsync(user);
            await UserManager.RemoveFromRolesAsync(user, currentRoles);
            await UserManager.AddToRolesAsync(user, model.Value.Roles);

            await AddOrUpdateAttachment(model, user);
            var result = await UserManager.UpdateAsync(user);

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
            var user = await UserManager.FindByIdAsync(model.Value.ToString());
            var result = await UserManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error =>
                {
                    model.AddErrorItem("", $"{error.Code} : {error.Description}");
                });
            }

            return model;
        }

        public async Task<ReturnResult<UserViewModel>> ChangePassword(ReturnResult<UserViewModel> model)
        {
            var user = await UserManager.FindByIdAsync(model.Value.Id.ToString());

            await this.UserManager.RemovePasswordAsync(user);
            var addpasswordResult = await this.UserManager.AddPasswordAsync(user, model.Value.Password);

            if (!addpasswordResult.Succeeded)
                model.AddErrorItem("", UsersResources.ChangePasswordFailed);

            return model;
        }
        public async Task<UserFilterViewModel> Search(UserFilterViewModel model)
        {
            var query = UserManager.Users;

            if (!string.IsNullOrEmpty(model.Name))
                query = query.Where(m => (m.FirstName + " " + m.MiddleName + " " + m.LastName).ToLower().Contains(model.Name.ToLower()));

            if (!string.IsNullOrEmpty(model.UserName))
                query = query.Where(m => m.UserName.ToLower().Contains(model.UserName.ToLower()));

            if (!string.IsNullOrEmpty(model.Email))
                query = query.Where(m => m.Email.ToLower().Contains(model.Email.ToLower()));

            if (!string.IsNullOrEmpty(model.PhoneNumber))
                query = query.Where(m => m.PhoneNumber.ToLower().Contains(model.PhoneNumber.ToLower()));



            List<UserViewModel> items = new List<UserViewModel>();

            model.TotalCount = query.Count();
            if (model.jtPageSize > 0)
                items = await query.Skip((int)(model.PageNumber * model.jtPageSize)).Take((int)model.jtPageSize)
                    .Select(role => UserMapper.MapToViewModel(role)).ToListAsync();
            else
                items = await query.Select(role => UserMapper.MapToViewModel(role)).ToListAsync();

            model.Items = items;

            return model;
        }



        public async Task<bool> ChangeActivation(string userName, bool activate)
        {
            var user = await UserManager.FindByNameAsync(userName);

            if (activate)
                return await ActivateUser(user);
            else
                return await DeactivateUser(user);
        }


        public async Task<bool> ActivateUser(ApplicationUser user)
        {
            await UserManager.SetLockoutEndDateAsync(user, null);
            return await this.UserManager.IsLockedOutAsync(user) == false;
        }


        private async Task<bool> DeactivateUser(ApplicationUser user)
        {
            await UserManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.UtcNow.AddYears(500)));
            return await this.UserManager.IsLockedOutAsync(user) == true;
        }

        public async Task<byte[]> LoadUserImage(string WebRootPath, string userName = "")
        {
            try
            {

                var user = await UserManager.FindByNameAsync(userName);
                string imagePath = "";
                if (user != null && user.AttachmentId != null)
                    imagePath = AttachmentService.GetFilePath(user.AttachmentId.Value);
                else
                    imagePath = WebRootPath + @"\images\user.png";
                return System.IO.File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> IsValidUser(string username, string password)
        {
            try
            {
                var user = await UserManager.FindByNameAsync(username);
                var result = await SignInManager.CheckPasswordSignInAsync(user, password, false);

                return result.Succeeded;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }


}
