using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    internal class RedisCacheAttribute(int durationinSecond = 120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
            //next ---> set Vali=ue --> go to end point
            string cacheKey = GenerateKey(context.HttpContext.Request);
            var result = await cacheService.GetCacheValueAsync(cacheKey);
            if (result != null) 
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "Application/json",
                    StatusCode = (int)HttpStatusCode.OK
                };
                return;
            }
            var resultContext = await next.Invoke();
            if (resultContext.Result is OkObjectResult objectResult)
            {
                await cacheService.SetCacheValueAsync(cacheKey, objectResult, TimeSpan.FromSeconds(durationinSecond));
            }
        }

        private string GenerateKey(HttpRequest request)
        {
            //A
            //H
            //M
            var key = new StringBuilder();
            //Request path api/Product
            //Request query ==>Query String
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
