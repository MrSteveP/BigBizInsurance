using System;
using System.Threading.Tasks;

namespace CrossCutting.Persistance
{
    public  interface IUnitOfWork
    {
        int Save(Guid? userId = null, bool validateOnSaveEnabled = true);

        //int Save();
        //Task<int> SaveAsync();
    }
}
