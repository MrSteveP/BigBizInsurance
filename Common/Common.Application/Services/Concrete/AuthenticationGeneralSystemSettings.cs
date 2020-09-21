using Common.Application.Persistance;
using Common.Application.Services.Abstraction;
using Common.Domain.Module;

namespace Common.Application.Services.Concrete
{
    public sealed class AuthenticationGeneralSystemSettings : IAuthenticationGeneralSystemSettings
    {
        public bool AllowRememberMe { get; set; }
        public bool AllowForgotPassword { get; set; }
        public bool RequireConfirmedAccount { set; get; }

        public AuthenticationGeneralSystemSettings(ICommonUnitOfWork commonsUnitOfWork)
        {
            this.UnitOfWork = commonsUnitOfWork;
            this.LoadSettings();
        }

        private ICommonUnitOfWork UnitOfWork { get; }

        private void LoadSettings()
        {
            AllowRememberMe = this.UnitOfWork.SystemSettingRepository.GetValue<bool>(
                CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.AllowRememberMe.ToString(),
                CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.ApplicationId);

            AllowForgotPassword = this.UnitOfWork.SystemSettingRepository.GetValue<bool>(
               CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.AllowForgotPassword.ToString(),
               CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.ApplicationId);

            RequireConfirmedAccount = this.UnitOfWork.SystemSettingRepository.GetValue<bool>(
               CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.RequireConfirmedAccount.ToString(),
               CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.ApplicationId);
        }
    }
}
