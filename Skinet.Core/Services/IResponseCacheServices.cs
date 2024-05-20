using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Core.Services
{
    public interface IResponseCacheServices
    {

        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeSpan);



        Task<string> GetCacheResponseAsync(string cashekey);
    }
}
