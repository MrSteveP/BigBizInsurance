using AuditTrailComponent.Persistance.Repositories.Abstraction;
using AuditTrailComponent.Persistance.Repositories.Concrete;
using System;
using System.Threading.Tasks;

namespace AuditTrailComponent.Persistance.UnitOfWork
{
    internal class AuditUnitOfWork : IAuditUnitOfWork
    {

        public AuditUnitOfWork(AuditDbContext context)
        {
            this.Context = context;
        }

        ~AuditUnitOfWork()
        {
            Dispose(true);
        }

        private AuditDbContext Context { get; }

        INotificationRepository notificationRepository;
        public INotificationRepository NotificationRepository
        {
            get
            {
                if (notificationRepository == null)
                    notificationRepository = new NotificationRepository(Context);

                return notificationRepository;
            }
        }

        IActionUserGroupRepository actionUserGroupRepository;
        public IActionUserGroupRepository ActionUserGroupRepository
        {
            get
            {
                if (actionUserGroupRepository == null)
                    actionUserGroupRepository = new ActionUserGroupRepository(Context);

                return actionUserGroupRepository;
            }
        }

        IActionRepository actionRepository;
        public IActionRepository ActionRepository
        {
            get
            {
                if (actionRepository == null)
                    actionRepository = new ActionRepository(Context);

                return actionRepository;
            }
        }

        INotificationLogRepository notificationLogRepository;
        public INotificationLogRepository NotificationLogRepository
        {
            get
            {
                if (notificationLogRepository == null)
                    notificationLogRepository = new NotificationLogRepository(Context);

                return notificationLogRepository;
            }
        }

        IAuditTrailRepository auditTrailRepository;
        public IAuditTrailRepository AuditTrailRepository
        {
            get
            {
                if (auditTrailRepository == null)
                    auditTrailRepository = new AuditTrailRepository(Context);

                return auditTrailRepository;
            }
        }

        IErrorLogRepository errorLogRepository;
        public IErrorLogRepository ErrorLogRepository
        {
            get
            {
                if (errorLogRepository == null)
                    errorLogRepository = new ErrorLogRepository(Context);

                return errorLogRepository;
            }
        }

        public int Save()
        {
            return this.Context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await this.Context.SaveChangesAsync();
        }


        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
