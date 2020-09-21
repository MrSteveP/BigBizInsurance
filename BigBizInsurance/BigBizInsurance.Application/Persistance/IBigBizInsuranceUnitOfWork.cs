using CrossCutting.Persistance;
using BigBizInsurance.Application.Persistance.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigBizInsurance.Application.Persistance
{
   public interface IBigBizInsuranceUnitOfWork : IUnitOfWork
    {
        string CurrentUserName { get; }

        ISampleRepository SampleRepository { get; }

    }
}
