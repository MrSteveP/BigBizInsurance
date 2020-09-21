using CrossCutting.Core.Extensions;
using BigBizInsurance.Domain.Models;
using BigBizInsurance.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BigBizInsurance.Domain.Mapper
{
    public static class SampleMapper
    {
        public static SampleModel MapToModel(SampleViewModel viewModel)
        {
            if (viewModel == null) return null;

            var sample = new SampleModel();
            if (viewModel.Id.HasValue)
                sample.Id = viewModel.Id.Value;

            sample.Name = viewModel.Name;
            sample.Active = true;

            return sample;
        }


        public static SampleViewModel MapToViewModel(SampleModel model)
        {
            if (model == null) return null;

            return new SampleViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Active = model.Active
            };
        }
    }
}
