using Common.Application.Persistance.Abstraction;
using Common.Domain.DTOs;
using Common.Domain.Module;
using CrossCutting.Core;
using CrossCutting.Persistance.Repositories;
using System.Collections.Generic;
using System.Linq;
using Common.Persistance;
using Z.EntityFramework.Plus;

namespace Common.Persistance.Concrete
{
    public sealed  class AttachmentTypeRepository : Repository<AttachmentType>, IAttachmentTypeRepository
    {
        public AttachmentTypeRepository(CommonDbContext context)
            : base(context)
        {
            Context = context;
        }

        public CommonDbContext Context { get; }

        public AttachmentTypeDto GetAttachmentTypeFromCache(int id)
        {
            return GetAll().FirstOrDefault(a => a.Id == id);
        }

        public List<AttachmentType> GetAll()
        {

            var setting = this.DbSet.FromCache(Caching.CachePolicy,
               Caching.Keys.AttachmentTypes);

            return setting.ToList();

        }
    }
}
