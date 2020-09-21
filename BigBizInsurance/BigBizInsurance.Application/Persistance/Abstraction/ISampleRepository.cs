using CrossCutting.Persistance;
using BigBizInsurance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BigBizInsurance.Application.Persistance.Abstraction
{
    public interface ISampleRepository : IRepository<SampleModel>
    {
    }
}
