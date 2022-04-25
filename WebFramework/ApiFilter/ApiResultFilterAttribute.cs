using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebFramework.Api;

namespace WebFramework.ApiFilter
{
   public class ApiResultFilterAttribute:ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            
            if (context.Result is OkObjectResult okObjectResult)
            {
                var apiResult = new ApiResult<object>(true, Common.Enums.ApiResultStatusCode.Success, okObjectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is OkResult objectResult)
            {
                var apiResult = new ApiResult(true, Common.Enums.ApiResultStatusCode.Success);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                var apiResult = new ApiResult(false, Common.Enums.ApiResultStatusCode.BadRequest);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is BadRequestObjectResult requestObjectResult)
            {
                var apiResult = new ApiResult<object>(false, Common.Enums.ApiResultStatusCode.BadRequest, requestObjectResult.Value);
                string message = requestObjectResult.Value.ToString();
                if (requestObjectResult.Value is SerializableError errors)
                {
                    var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                    message = string.Join(" | ", errorMessages);
                }
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                var apiResult = new ApiResult(false, Common.Enums.ApiResultStatusCode.NotFound);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                var apiResult = new ApiResult<object>(false, Common.Enums.ApiResultStatusCode.NotFound,notFoundObjectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new ApiResult(true, Common.Enums.ApiResultStatusCode.Success);
                context.Result = new JsonResult(apiResult);
            }
            
            else if (context.Result is ObjectResult result && result.StatusCode == null && !(result.Value is ApiResult))
            {
                var apiResult = new ApiResult<object>(true, Common.Enums.ApiResultStatusCode.Success,result.Value);
                context.Result = new JsonResult(apiResult);
            }
            base.OnResultExecuting(context);
        }
    }
}
