using Common.Application.Persistance;
using Common.Application.Services.Abstraction;
using Common.Domain.DTOs;
using CrossCutting.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Application.Services.Concrete
{
    public sealed class SystemSettingsService : ISystemSettingsService
    {
        ICommonUnitOfWork CommonUnitOfWork { get; }
        public SystemSettingsService(ICommonUnitOfWork _commonUnitOfWork)
        {
            CommonUnitOfWork = _commonUnitOfWork;
        }

        #region System settings properties
        IAuthenticationGeneralSystemSettings authenticationGeneralSystemSettings;
        public IAuthenticationGeneralSystemSettings AuthenticationGeneralSystemSettings
        {
            get
            {
                if (authenticationGeneralSystemSettings == null)
                    authenticationGeneralSystemSettings = new AuthenticationGeneralSystemSettings(CommonUnitOfWork);

                return authenticationGeneralSystemSettings;
            }
        }

        IAuthenticationThrottlingSystemSettings authenticationThrottlingSystemSettings;
        public IAuthenticationThrottlingSystemSettings AuthenticationThrottlingSystemSettings
        {
            get
            {
                if (authenticationThrottlingSystemSettings == null)
                    authenticationThrottlingSystemSettings = new AuthenticationThrottlingSystemSettings(CommonUnitOfWork);

                return authenticationThrottlingSystemSettings;
            }
        }

        IGeneralSystemSettings generalSystemSettings;
        public IGeneralSystemSettings GeneralSystemSettings
        {
            get
            {
                if (generalSystemSettings == null)
                    generalSystemSettings = new GeneralSystemSettings(CommonUnitOfWork);

                return generalSystemSettings;
            }
        }

        ISmtpSystemSettings smtpSystemSettings;
        public ISmtpSystemSettings SmtpSystemSettings
        {
            get
            {
                if (smtpSystemSettings == null)
                    smtpSystemSettings = new SmtpSystemSettings(CommonUnitOfWork);

                return smtpSystemSettings;
            }
        }

        IAttachmentSystemSettings attachmentSystemSettings;
        public IAttachmentSystemSettings AttachmentSystemSettings
        {
            get
            {
                if (attachmentSystemSettings == null)
                    attachmentSystemSettings = new AttachmentSystemSettings(CommonUnitOfWork);

                return attachmentSystemSettings;
            }
        }

        
        #endregion

        public async Task<ReturnResult<List<SystemSettingDto>>> Update(ReturnResult<List<SystemSettingDto>> model)
        {
            model.Value.ForEach(setting =>
            {
                CommonUnitOfWork.SystemSettingRepository.UpdateSystemSetting(setting.Key, setting.Value, setting.ApplicationId);
            });
             CommonUnitOfWork.Save();

            CommonUnitOfWork.SystemSettingRepository.ClearCache();
            return model;
        }
    }
}
