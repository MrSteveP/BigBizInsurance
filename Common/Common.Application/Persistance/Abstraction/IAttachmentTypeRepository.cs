using Common.Domain.DTOs;
using Common.Domain.Module;
using CrossCutting.Persistance;

namespace Common.Application.Persistance.Abstraction
{
    public interface IAttachmentTypeRepository : IRepository<AttachmentType>
    {
        AttachmentTypeDto GetAttachmentTypeFromCache(int id);
    }
}
