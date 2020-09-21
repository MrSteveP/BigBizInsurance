using Common.Application.Persistance;
using Common.Application.Services.Abstraction;
using Common.Domain.Module;

namespace Common.Application.Services.Concrete
{
    public sealed class AuthenticationThrottlingSystemSettings : IAuthenticationThrottlingSystemSettings
    {
        public bool AllowThrottleAuthentication { get; set; }
        public int LockoutTime { get; set; }
        public int MaximumNumberOfAttempts { get; set; }

        public AuthenticationThrottlingSystemSettings(ICommonUnitOfWork commonsUnitOfWork)
        {
            this.UnitOfWork = commonsUnitOfWork;
            this.LoadSettings();
        }

        private ICommonUnitOfWork UnitOfWork { get; }

        private void LoadSettings()
        {
            AllowThrottleAuthentication = this.UnitOfWork.SystemSettingRepository.GetValue<bool>(
                CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.AllowThrottleAuthentication.ToString(),
                CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.ApplicationId);

            LockoutTime = this.UnitOfWork.SystemSettingRepository.GetValue<int>(
               CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.LockoutTime.ToString(),
               CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.ApplicationId);

            MaximumNumberOfAttempts = this.UnitOfWork.SystemSettingRepository.GetValue<int>(
                  CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.MaximumNumberOfAttempts.ToString(),
                  CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.ApplicationId);
        }

    }
}
