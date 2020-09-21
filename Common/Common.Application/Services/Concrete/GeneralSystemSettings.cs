using Common.Application.Persistance;
using Common.Application.Services.Abstraction;
using Common.Domain.Module;

namespace Common.Application.Services.Concrete
{
    public sealed   class GeneralSystemSettings: IGeneralSystemSettings
    {
        public string DefaultCulture { get; set; }

        public GeneralSystemSettings(ICommonUnitOfWork commonsUnitOfWork)
        {
            this.UnitOfWork = commonsUnitOfWork;
            this.LoadSettings();
        }

        private ICommonUnitOfWork UnitOfWork { get; }

        private void LoadSettings()
        {
            DefaultCulture = this.UnitOfWork.SystemSettingRepository.GetValue<string>(
                  CommonStaticValues.SystemSettingsData.GeneralApplication.KeysEnum.DefaultCulture.ToString(),
                  CommonStaticValues.SystemSettingsData.GeneralApplication.ApplicationId);
        }
    }
}
