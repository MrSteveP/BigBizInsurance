using CrossCutting.Core;
using BigBizInsurance.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigBizInsurance.Application.Services.Abstraction
{
    public interface ISampleService
    {
        Task<ReturnResult<SampleViewModel>> GetById(int sampleId);
        Task<ReturnResult<SampleViewModel>> Save(bool add, ReturnResult<SampleViewModel> model);
        Task<SampleFilterViewModel> Search(SampleFilterViewModel model);
        Task<ReturnResult<int>> Delete(ReturnResult<int> model);
    }
}
