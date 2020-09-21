using Common.Application.Persistance;
using Common.Application.Persistance.Abstraction;
using Common.Persistance.Concrete;
using CrossCutting.Persistance.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Common.Persistance
{
    public class CommonUnitOfWork : UnitOfWork, ICommonUnitOfWork
    {

        public CommonUnitOfWork(CommonDbContext context, IHttpContextAccessor httpContextAccessor)
             : base(context, httpContextAccessor)
        {
            this.Context = context;
            HttpContextAccessor = httpContextAccessor;
        }

        ~CommonUnitOfWork()
        {
            Dispose(true);
        }

        private CommonDbContext Context { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        ISystemSettingRepository systemSettingRepository;
        public ISystemSettingRepository SystemSettingRepository
        {
            get
            {
                if (systemSettingRepository == null)
                    systemSettingRepository = new SystemSettingRepository(Context);

                return systemSettingRepository;
            }
        }

        IAttachmentTypeRepository attachmentTypeRepository;
        public IAttachmentTypeRepository AttachmentTypeRepository
        {
            get
            {
                if (attachmentTypeRepository == null)
                    attachmentTypeRepository = new AttachmentTypeRepository(Context);

                return attachmentTypeRepository;
            }
        }

        IAttachmentRepository attachmentRepository;
        public IAttachmentRepository AttachmentRepository
        {
            get
            {
                if (attachmentRepository == null)
                    attachmentRepository = new AttachmentRepository(Context);

                return attachmentRepository;
            }
        }

        public new int Save(Guid? userId = null, bool validateOnSaveEnabled = true)
        {
            //TODO : add audit trails code
            return base.Save(userId, validateOnSaveEnabled);
        }

        //public async new Task<int> SaveAsync()
        //{
        //    //TODO : add audit trails code
        //    return await base.SaveAsync();
        //}

    }
}
