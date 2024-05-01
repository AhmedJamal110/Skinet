using Skinet.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Repository
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IGenericRepository<T> Repository<T>() where T : BaseEntity;


        Task<int> Complete();
        
    }
}
