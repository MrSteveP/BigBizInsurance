using CrossCutting.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using BigBizInsurance.Application.Persistance.Abstraction;
using BigBizInsurance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BigBizInsurance.Persistance.Concrete
{
    public sealed class SampleRepository : Repository<SampleModel>, ISampleRepository
    {
        public SampleRepository(BigBizInsuranceDbContext context)
            : base(context)
        {
            Context = context;
        }

        public BigBizInsuranceDbContext Context { get; }


    }
}
