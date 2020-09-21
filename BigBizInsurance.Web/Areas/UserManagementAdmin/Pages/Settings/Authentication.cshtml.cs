using Common.Application.Services.Abstraction;
using Common.Domain.DTOs;
using Common.Domain.Module;
using CrossCutting.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using MessagesResources = Common.Localization.Messages.Messages;
using SettingsManagementResources = UserManagement.Localization.SettingsManagement.SettingsManagement;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Settings
{
    public class AuthenticationModel : BasePageModel
    {
        public AuthenticationModel(IOptions<IdentityOptions> option,ISystemSettingsService systemSettingsService)
        {
            Title = SettingsManagementResources.Authentication_Title;

            Option = option;
            SystemSettingsService = systemSettingsService;
        }

        #region Dependencies
        public IOptions<IdentityOptions> Option { get; }
        public ISystemSettingsService SystemSettingsService { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        #endregion

        #region Properties
        [BindProperty]
        public AuthenticationViewModel Input { set; get; } = new AuthenticationViewModel();
        #endregion

        #region Get
        public void OnGet()
        {
            FillData();
        }

        private void FillData()
        {
            var AuthenticationGeneralSystemSettings = SystemSettingsService.AuthenticationGeneralSystemSettings;
            var AuthenticationThrottlingSystemSettings = SystemSettingsService.AuthenticationThrottlingSystemSettings;

            Input.AllowForgotPassword = AuthenticationGeneralSystemSettings.AllowForgotPassword;
            Input.AllowRememberMe = AuthenticationGeneralSystemSettings.AllowRememberMe;
            Input.RequireConfirmedAccount = AuthenticationGeneralSystemSettings.RequireConfirmedAccount;

            Input.AllowThrottleAuthentication = AuthenticationThrottlingSystemSettings.AllowThrottleAuthentication;
            Input.MaximumNumberOfAttempts = AuthenticationThrottlingSystemSettings.MaximumNumberOfAttempts;
            Input.LockoutTime = AuthenticationThrottlingSystemSettings.LockoutTime;
        }
        #endregion

        #region Post
        #region Authentication General
        public async Task<IActionResult> OnPostSaveAuthenticationGeneral()
        {
            if (!ModelState.IsValid)
                return Page();

            ReturnResult<List<SystemSettingDto>> model = new ReturnResult<List<SystemSettingDto>>();
            model.Value = CollectAuthenticationGeneralSettings();

            var result = await SystemSettingsService.Update(model);
            ModelState.Merge(result);

            if (!ModelState.IsValid)
                return Page();

            ApplySignInOptions();
            ShowMessage(CoreEnumerations.MessageTypes.info, MessagesResources.SavedSuccessfully);
            FillData();
            return Page();
        }

        private List<SystemSettingDto> CollectAuthenticationGeneralSettings()
        {
            List<SystemSettingDto> settingsList = new List<SystemSettingDto>();
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.AllowForgotPassword.ToString(),
                Value = Input.AllowForgotPassword.ToString()
            });
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.AllowRememberMe.ToString(),
                Value = Input.AllowRememberMe.ToString()
            });
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.RequireConfirmedAccount.ToString(),
                Value = Input.RequireConfirmedAccount.ToString()
            });

            return settingsList;
        }

        private void ApplySignInOptions()
        {
             Option.Value.SignIn.RequireConfirmedAccount = Input.RequireConfirmedAccount;
        }
        #endregion 

        #region Authentication Throttling
        public async Task<IActionResult> OnPostSaveAuthenticationThrottling()
        {
            if (!ModelState.IsValid)
                return Page();

            ReturnResult<List<SystemSettingDto>> model = new ReturnResult<List<SystemSettingDto>>();
            model.Value = CollectAuthenticationThrottlingSettings();

            var result = await SystemSettingsService.Update(model);
            ModelState.Merge(result);

            if (!ModelState.IsValid)
                return Page();

            ApplyLockOutOptions();
            ShowMessage(CoreEnumerations.MessageTypes.info, MessagesResources.SavedSuccessfully);
            FillData();
            return Page();
        }

        private void ApplyLockOutOptions()
        {
            if (Input.AllowThrottleAuthentication)
            {
                Option.Value.Lockout = new LockoutOptions
                {
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Input.LockoutTime),
                    MaxFailedAccessAttempts = Input.MaximumNumberOfAttempts
                };
            }
            else
            {
                Option.Value.Lockout = null;
            }
        }

        private List<SystemSettingDto> CollectAuthenticationThrottlingSettings()
        {
            List<SystemSettingDto> settingsList = new List<SystemSettingDto>();
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.AllowThrottleAuthentication.ToString(),
                Value = Input.AllowThrottleAuthentication.ToString()
            });
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.LockoutTime.ToString(),
                Value = Input.LockoutTime.ToString()
            });
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.MaximumNumberOfAttempts.ToString(),
                Value = Input.MaximumNumberOfAttempts.ToString()
            });
            return settingsList;
        }
        #endregion

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Index", new { area = "UserManagementAdmin" });
        }
        #endregion

    }
}
