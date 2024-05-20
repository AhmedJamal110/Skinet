using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Skinet.Core.Services;
using System.Text;

namespace Skinet.API.Helper
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSec;

        public CachedAttribute(int timeToLiveInSec)
        {
            _timeToLiveInSec = timeToLiveInSec;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          var CacheServices = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheServices>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cacheResponse = await  CacheServices.GetCacheResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };

                context.Result = contentResult;
                return;
            }

             var ExcuitedEndPointContext =  await next();

            if (ExcuitedEndPointContext.Result is OkObjectResult okResult)
            {
                await CacheServices.CacheResponseAsync(cacheKey, cacheResponse,TimeSpan.FromSeconds(_timeToLiveInSec));
            }

        }
    
    
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path);

            foreach (var (key , value ) in request.Query)
            {
                keyBuilder.Append($"|{key}-{value}");

            }
            return keyBuilder.ToString();
        }
    }
}
