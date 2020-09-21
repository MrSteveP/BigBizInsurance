using AuditTrailComponent.Services.Abstraction;
using CrossCutting.Core;
using BigBizInsurance.Application.Persistance;
using BigBizInsurance.Domain.ViewModels;

namespace BigBizInsurance.Application.Services.Logic.SampleLogic
{
    internal static class SampleFactory
    {
        public static IRequestLogic<SampleViewModel> CreateInstance(bool Add, IBigBizInsuranceUnitOfWork unitOfWork
            , IAuditTrailService auditTrailService)
        {
            if (Add)
                return new SampleCreation(unitOfWork, auditTrailService);
            else
                return new SampleModification(unitOfWork);

        }
    }
}
