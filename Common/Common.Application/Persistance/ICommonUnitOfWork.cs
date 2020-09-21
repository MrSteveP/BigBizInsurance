using Common.Application.Persistance.Abstraction;
using CrossCutting.Persistance;

namespace Common.Application.Persistance
{
    public interface ICommonUnitOfWork: IUnitOfWork
    {
        ISystemSettingRepository SystemSettingRepository { get; }
        IAttachmentTypeRepository AttachmentTypeRepository { get; }
        IAttachmentRepository AttachmentRepository { get; }


    }
}
