using Common.Application.Persistance;
using Common.Application.Services.Abstraction;
using Common.Domain.Module;

namespace Common.Application.Services.Concrete
{
    public sealed class AttachmentSystemSettings : IAttachmentSystemSettings
    {
        public string AttachmentPath { get ; set; }
        public bool SaveFilesToDatabase { set; get; }

        public AttachmentSystemSettings(ICommonUnitOfWork commonsUnitOfWork)
        {
            this.UnitOfWork = commonsUnitOfWork;
            this.LoadSettings();
        }

        private ICommonUnitOfWork UnitOfWork { get; }

        private void LoadSettings()
        {
            AttachmentPath = this.UnitOfWork.SystemSettingRepository.GetValue<string>(
                CommonStaticValues.SystemSettingsData.AttachmentApplication.KeysEnum.AttachmentPath.ToString(),
                CommonStaticValues.SystemSettingsData.AttachmentApplication.ApplicationId);

            SaveFilesToDatabase = this.UnitOfWork.SystemSettingRepository.GetValue<bool>(
               CommonStaticValues.SystemSettingsData.AttachmentApplication.KeysEnum.SaveFilesToDatabase.ToString(),
               CommonStaticValues.SystemSettingsData.AttachmentApplication.ApplicationId);
        }
    }
}
