using Common.Domain.Module;
using CrossCutting.Persistance;
using System.Collections.Generic;

namespace Common.Application.Persistance.Abstraction
{
    public interface ISystemSettingRepository : IRepository<SystemSetting>
    {
        T GetValue<T>(string key, string applicationId = null);
        List<SystemSetting> GetAll(string applicationId = null);
        void ClearCache();
        void UpdateSystemSetting(string key, string value, string applicationId = null, string desc = null);
    }
}
