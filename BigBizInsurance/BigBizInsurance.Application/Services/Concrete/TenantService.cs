using BigBizInsurance.Application.Persistance;
using BigBizInsurance.Application.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBizInsurance.Application.Services.Concrete
{
    public sealed class TenantService : ITenantService
    {
        public TenantService(IBigBizInsuranceUnitOfWork newApplicationUnitOfWork)
        {
            BigBizInsuranceUnitOfWork = newApplicationUnitOfWork;
        }

        public IBigBizInsuranceUnitOfWork BigBizInsuranceUnitOfWork { get; }

      
    }
}
