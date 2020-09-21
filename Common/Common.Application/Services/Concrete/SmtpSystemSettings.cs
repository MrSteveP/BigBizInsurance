using Common.Application.Persistance;
using Common.Application.Services.Abstraction;
using Common.Domain.Module;

namespace Common.Application.Services.Concrete
{
    public sealed class SmtpSystemSettings : ISmtpSystemSettings
    {

        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public bool IsSmtpAuthenticated { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpEnableSSL { get; set; }

        public SmtpSystemSettings(ICommonUnitOfWork commonsUnitOfWork)
        {
            this.UnitOfWork = commonsUnitOfWork;
            this.LoadSettings();
        }


        private ICommonUnitOfWork UnitOfWork { get; }

        private void LoadSettings()
        {
            SmtpServer = this.UnitOfWork.SystemSettingRepository.GetValue<string>(
                  CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpServer.ToString(),
                  CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId);

            SmtpUserName = this.UnitOfWork.SystemSettingRepository.GetValue<string>(
                 CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpUserName.ToString(),
                 CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId);

            SmtpPassword = this.UnitOfWork.SystemSettingRepository.GetValue<string>(
                CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpPassword.ToString(),
                CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId);

            IsSmtpAuthenticated = this.UnitOfWork.SystemSettingRepository.GetValue<bool>(
               CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.IsSmtpAuthenticated.ToString(),
               CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId);

            SmtpPort = this.UnitOfWork.SystemSettingRepository.GetValue<int>(
              CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpPort.ToString(),
              CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId);

            SmtpEnableSSL = this.UnitOfWork.SystemSettingRepository.GetValue<bool>(
              CommonStaticValues.SystemSettingsData.SmtpApplication.KeysEnum.SmtpEnableSSL.ToString(),
              CommonStaticValues.SystemSettingsData.SmtpApplication.ApplicationId);

        }
    }
}
