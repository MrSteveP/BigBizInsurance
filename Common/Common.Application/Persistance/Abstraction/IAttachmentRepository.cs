using Common.Domain.Module;
using CrossCutting.Persistance;

namespace Common.Application.Persistance.Abstraction
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {
    }
}
