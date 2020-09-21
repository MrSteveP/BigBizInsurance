using CrossCutting.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigBizInsurance.Domain.ViewModels
{
   public class SampleFilterViewModel : FilterVModel<SampleViewModel>
    {
        public string Keywork { set; get; }

    }
}
