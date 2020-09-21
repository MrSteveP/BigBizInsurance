using BigBizInsurance.Domain.Mapper;
using BigBizInsurance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Z.EntityFramework.Extensions;

namespace BigBizInsurance.Domain.ViewModels
{
  public  class SampleViewModel //:BaseTenantInfo
    {
      public int? Id { set; get; }
        public string Name { set; get; }
        public bool Active { set; get; }

        public static implicit operator SampleModel(SampleViewModel model)
        {
            return SampleMapper.MapToModel(model);
        }

        public static implicit operator SampleViewModel(SampleModel teacher)
        {
            return SampleMapper.MapToViewModel(teacher);
        }
    }
}
