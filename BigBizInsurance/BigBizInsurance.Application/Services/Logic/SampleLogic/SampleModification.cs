using CrossCutting.Core;
using BigBizInsurance.Application.Persistance;
using BigBizInsurance.Domain.Models;
using BigBizInsurance.Domain.ViewModels;
using BigBizInsurance.Localization.SampleResources;
using System;
using System.Threading.Tasks;

namespace BigBizInsurance.Application.Services.Logic.SampleLogic
{
    internal sealed class SampleModification : IRequestLogic<SampleViewModel>
    {
        public SampleModification(IBigBizInsuranceUnitOfWork newApplicationUnitOfWork)
        {
            BigBizInsuranceUnitOfWork = newApplicationUnitOfWork;
        }

        public IBigBizInsuranceUnitOfWork BigBizInsuranceUnitOfWork { get; }

        public async Task<ReturnResult<SampleViewModel>> IsValidAsync(ReturnResult<SampleViewModel> model)
        {

            bool dbExists = await BigBizInsuranceUnitOfWork.SampleRepository.AnyAsync(a => a.Id != model.Value.Id && a.Name == model.Value.Name && a.Active==true);
            if (dbExists) model.AddErrorItem("", SampleResources.msg_SampleAlreadyExist);

            return model;
        }

        public async Task<ReturnResult<SampleViewModel>> SaveAsync(ReturnResult<SampleViewModel> model)
        {
            SampleModel sample = model.Value;
           
            BigBizInsuranceUnitOfWork.SampleRepository.Update(sample);
            BigBizInsuranceUnitOfWork.Save();

            return model;
        }

  
    }
}
