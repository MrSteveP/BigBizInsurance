using AuditTrailComponent.Services.Abstraction;
using CrossCutting.Core;
using Microsoft.EntityFrameworkCore;
using BigBizInsurance.Application.Persistance;
using BigBizInsurance.Application.Services.Abstraction;
using BigBizInsurance.Application.Services.Logic.SampleLogic;
using BigBizInsurance.Domain.Mapper;
using BigBizInsurance.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedResources = Common.Localization.Shared.Shared;

namespace BigBizInsurance.Application.Services.Concrete
{
    public sealed class SampleService : ISampleService
    {
        public SampleService(IBigBizInsuranceUnitOfWork newApplicationUnitOfWork,
            IAuditTrailService auditTrailService)
        {
            BigBizInsuranceUnitOfWork = newApplicationUnitOfWork;
            AuditTrailService = auditTrailService;
        }

        public IBigBizInsuranceUnitOfWork BigBizInsuranceUnitOfWork { get; }
        public IAuditTrailService AuditTrailService { get; }

        public async Task<ReturnResult<SampleViewModel>> GetById(int sampleId)
        {
            ReturnResult<SampleViewModel> result = new ReturnResult<SampleViewModel>();

            var sample =await BigBizInsuranceUnitOfWork.SampleRepository.GetByIdAsync(sampleId);

            result.Value = sample;

            if (sample == null)
                result.AddErrorItem("", SharedResources.NoDataToDisplay);

            return result;

        }
     
        public async Task<List<SampleViewModel>> GetAll()
        {
            return BigBizInsuranceUnitOfWork.SampleRepository.GetQuery()
                     .Select(sample => SampleMapper.MapToViewModel(sample)).ToList();
        }

  
        public async Task<ReturnResult<SampleViewModel>> Save(bool add, ReturnResult<SampleViewModel> model)
        {

            var requestlogic = SampleFactory.CreateInstance(add, BigBizInsuranceUnitOfWork, AuditTrailService);

            model = await requestlogic.IsValidAsync(model);
            if (!model.IsValid)
                return model;

            return await requestlogic.SaveAsync(model);
        }

     
        public async Task<SampleFilterViewModel> Search(SampleFilterViewModel model)
        {
            var query = BigBizInsuranceUnitOfWork.SampleRepository.GetQuery();

            if (!string.IsNullOrEmpty(model.Keywork))
                query = query.Where(m => m.Name.Contains(model.Keywork.ToLower()));

            
            List<SampleViewModel> items = new List<SampleViewModel>();

            model.TotalCount = query.Count();
            if (model.jtPageSize > 0)
                items = await query.Skip((int)(model.PageNumber * model.jtPageSize)).Take((int)model.jtPageSize)
                    .Select(teacher => SampleMapper.MapToViewModel(teacher)).ToListAsync();
            else
                items = await query.Select(teacher => SampleMapper.MapToViewModel(teacher)).ToListAsync();

            model.Items = items;

            return model;
        }

        public async Task<ReturnResult<int>> Delete(ReturnResult<int> model)
        {
            var sample = await BigBizInsuranceUnitOfWork.SampleRepository.GetByIdAsync(model.Value);
            sample.Active = false;
            BigBizInsuranceUnitOfWork.SampleRepository.Update(sample);
            int affRows = BigBizInsuranceUnitOfWork.Save();

            if (affRows == 0)
                model.AddErrorItem("", SharedResources.DeleteFail);

            return model;
        }
    }
}
