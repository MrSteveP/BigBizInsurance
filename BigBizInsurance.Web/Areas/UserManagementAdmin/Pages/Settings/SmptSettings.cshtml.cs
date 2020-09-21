using Common.Application.Services.Abstraction;
using Common.Domain.DTOs;
using Common.Domain.Module;
using CrossCutting.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.ViewModels;
using BigBizInsurance.Web.Code;
using SettingResources = UserManagement.Localization.SettingsManagement.SettingsManagement;
using SharedResources = Common.Localization.Shared.Shared;

namespace BigBizInsurance.Web.Areas.UserManagementAdmin.Pages.Settings
{
    public class SmptSettingsModel : BasePageModel
    {
        public SmptSettingsModel(ISystemSettingsService systemSettingsService)
        {
            Title = SettingResources.SMTPSetting_Title;

            SystemSettingsService = systemSettingsService;
        }

        #region Dependencies
        public ISystemSettingsService SystemSettingsService { get; }
        #endregion

        #region Properties
        [BindProperty]
        public SMTPViewModel Input { set; get; } = new SMTPViewModel();
        #endregion

        #region Get
        public void OnGet()
        {
            FillData();
        }

        private void FillData()
        {
            var SmtpSystemSettings = SystemSettingsService.SmtpSystemSettings;

            Input.IsSmtpAuthenticated = SmtpSystemSettings.IsSmtpAuthenticated;
            Input.SmtpEnableSSL = SmtpSystemSettings.SmtpEnableSSL;
            Input.SmtpPassword = SmtpSystemSettings.SmtpPassword;
            Input.SmtpPort = SmtpSystemSettings.SmtpPort;
            Input.SmtpServer = SmtpSystemSettings.SmtpServer;
            Input.SmtpUserName = SmtpSystemSettings.SmtpUserName;
        }
        #endregion

        #region Post
        public async Task<IActionResult> OnPostSubmit()
        {
            if (!ModelState.IsValid)
                return Page();

            ReturnResult<List<SystemSettingDto>> model = new ReturnResult<List<SystemSettingDto>>();
            model.Value = CollectSmtpSettings();

            var result = await SystemSettingsService.Update(model);
            ModelState.Merge(result);

            if (!ModelState.IsValid)
                return Page();

            ShowMessage(CoreEnumerations.MessageTypes.info, SharedResources.SaveSuccess);
            FillData();
            return Page();
        }

        private List<SystemSettingDto> CollectSmtpSettings()
        {
            List<SystemSettingDto> settingsList = new List<SystemSettingDto>();
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.IsSmtpAuthenticated.ToString(),
                Value = Input.IsSmtpAuthenticated.ToString()
            });
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpEnableSSL.ToString(),
                Value = Input.SmtpEnableSSL.ToString()
            });
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpPassword.ToString(),
                Value = Input.SmtpPassword?.ToString()
            });
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpPort.ToString(),
                Value = Input.SmtpPort?.ToString()
            });
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpServer.ToString(),
                Value = Input.SmtpServer?.ToString()
            });
            settingsList.Add(new SystemSettingDto
            {
                ApplicationId = CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId,
                Key = CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpUserName.ToString(),
                Value = Input.SmtpUserName?.ToString()
            });
            return settingsList;
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage("/Index", new { area = "UserManagementAdmin" });
        }

        #endregion
    }
}
