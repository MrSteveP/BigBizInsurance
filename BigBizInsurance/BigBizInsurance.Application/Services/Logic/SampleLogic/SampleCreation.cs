using AuditTrailComponent.Services.Abstraction;
using CrossCutting.Core;
using Hangfire;
using BigBizInsurance.Application.Persistance;
using BigBizInsurance.Domain;
using BigBizInsurance.Domain.Models;
using BigBizInsurance.Domain.ViewModels;
using BigBizInsurance.Localization.SampleResources;
using System.Threading.Tasks;

namespace BigBizInsurance.Application.Services.Logic.SampleLogic
{
    internal sealed class SampleCreation : IRequestLogic<SampleViewModel>
    {
        public SampleCreation(IBigBizInsuranceUnitOfWork newApplicationUnitOfWork, IAuditTrailService auditTrailService)
        {
            BigBizInsuranceUnitOfWork = newApplicationUnitOfWork;
            AuditTrailService = auditTrailService;
        }

        public IBigBizInsuranceUnitOfWork BigBizInsuranceUnitOfWork { get; }
        public IAuditTrailService AuditTrailService { get; }

        public async Task<ReturnResult<SampleViewModel>> IsValidAsync(ReturnResult<SampleViewModel> model)
        {

            bool dbExists = await BigBizInsuranceUnitOfWork.SampleRepository.AnyAsync(a => a.Name == model.Value.Name && a.Active==true);
            if (dbExists) model.AddErrorItem("", SampleResources.msg_SampleAlreadyExist);

            return model;
        }

        public async Task<ReturnResult<SampleViewModel>> SaveAsync(ReturnResult<SampleViewModel> model)
        {
            SampleModel sample = model.Value;

            await BigBizInsuranceUnitOfWork.SampleRepository.AddAsync(sample);
             BigBizInsuranceUnitOfWork.Save();

            BackgroundJob.Enqueue(() => AuditTrailService.SaveCustomActionsAuditTrailAsync(BigBizInsuranceConstants.AuditActionsEnum.CreateSample.ToString(), $"New Sample has been created , sample name: { model.Value.Name}", BigBizInsuranceUnitOfWork.CurrentUserName));


            model.Value.Id = sample.Id;
            return model;
        }
    }
}
