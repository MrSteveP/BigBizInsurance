using System.Threading.Tasks;

namespace CrossCutting.Core
{
    public interface IRequestLogic<T> where T : class
    {
        Task<ReturnResult<T>> IsValidAsync(ReturnResult<T> model);
        Task<ReturnResult<T>> SaveAsync(ReturnResult<T> model);
    }
}
