using Common.Application.Persistance.Abstraction;
using Common.Domain.Module;
using CrossCutting.Persistance.Repositories;
using Common.Persistance;

namespace Common.Persistance.Concrete
{
    public sealed  class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(CommonDbContext context)
            : base(context)
        {
            Context = context;
        }

        public CommonDbContext Context { get; }
    }
}
