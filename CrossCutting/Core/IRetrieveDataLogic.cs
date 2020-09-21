using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Core
{
    public interface IRetrieveDataLogic<T>where T : class
    {
        Task<ReturnResult<T>> RetrieveDataAsync();
    }
}
