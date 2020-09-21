using Common.Domain.DTOs;
using CrossCutting.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Application.Services.Abstraction
{
    public interface ISystemSettingsService
    {
        #region System settings properties
        IGeneralSystemSettings GeneralSystemSettings { get; }
        ISmtpSystemSettings SmtpSystemSettings { get; }
        IAuthenticationGeneralSystemSettings AuthenticationGeneralSystemSettings { get; }
        IAuthenticationThrottlingSystemSettings AuthenticationThrottlingSystemSettings { get; }
        IAttachmentSystemSettings AttachmentSystemSettings { get; }
        #endregion

        Task<ReturnResult<List<SystemSettingDto>>> Update(ReturnResult<List<SystemSettingDto>> model);
    }
}
