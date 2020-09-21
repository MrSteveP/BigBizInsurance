using Common.Application.Persistance.Abstraction;
using Common.Domain.Module;
using CrossCutting.Core;
using CrossCutting.Core.Globalization;
using CrossCutting.Persistance.Repositories;
using System.Collections.Generic;
using System.Linq;
using Common.Persistance;
using Z.EntityFramework.Plus;

namespace Common.Persistance.Concrete
{
    public sealed class SystemSettingRepository : Repository<SystemSetting>, ISystemSettingRepository
    {
        public SystemSettingRepository(CommonDbContext context)
            : base(context)
        {
            Context = context;
        }

        public CommonDbContext Context { get; }

        public T GetValue<T>(string key, string applicationId = null)
        {
            var setting = this.GetAll(applicationId).FirstOrDefault(e => e.Key == key && (e.ApplicationId == applicationId || applicationId == null));
            return setting == null ? default(T) : setting.Value.To<T>();
        }

        public List<SystemSetting> GetAll(string applicationId = null)
        {
          
               var setting = this.DbSet.FromCache(Caching.CachePolicy,
                  Caching.Keys.SystemSettings);

            if (!string.IsNullOrEmpty(applicationId))
            {
                return setting.Where(s => s.ApplicationId == applicationId).ToList();
            }

            return setting.ToList();
           
        }

        public void ClearCache()
        {
            QueryCacheManager.ExpireTag(Caching.Keys.SystemSettings);
        }
        public void UpdateSystemSetting(string key, string value, string applicationId = null, string desc = null)
        {
            var setting = this.DbSet.FirstOrDefault(
                c => c.Key == key && (c.ApplicationId == applicationId || applicationId == null));

            if (setting != null)
            {
                setting.Value = value;
                if (desc != null)
                {
                    if (CultureHelper.IsArabic)
                    {
                        setting.Desc_Ar = desc;
                    }
                    else
                    {
                        setting.Desc_En = desc;
                    }


                }
            }
        }

    }
}
