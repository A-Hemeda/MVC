using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Core.Services.Contract;
using System.Text;

namespace Store.Attributes
{
    public class CacheAttribute : Attribute , IAsyncActionFilter
    {
        private readonly int _expireTime;

        public CacheAttribute(int expireTime) 
        {
            _expireTime = expireTime;
        }

        // this function for : is any data exixst
        // if cacheResponse is null or empty then [next from ActionExecutionDelegate will send the data from database]
        // next is the address of the GetAllProducts From ProductController
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var chacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cacheResponse = await chacheService.GetCacheKeyAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var excutedContext =  await next();

            if(excutedContext.Result is OkObjectResult response)
            {
                await chacheService.SetCacheKeyAsync(cacheKey,response,TimeSpan.FromMinutes(_expireTime));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var cacheKey = new StringBuilder();
            cacheKey.Append($"{request.Path}");

            foreach (var (key , value) in request.Query.OrderBy( x => x.Key))
            {
                cacheKey.Append($"|{key}-{value}");
            }
            return cacheKey.ToString();
        }
    }
}
