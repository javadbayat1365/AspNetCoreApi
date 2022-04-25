using Common.Enums;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebFramework.Api
{
   public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String Message { get; set; }

        public ApiResult(bool isSuccess,ApiResultStatusCode statusCode,string message = null)
        {
            this.IsSuccess = isSuccess;
            this.StatusCode = statusCode;
            this.Message = message ?? statusCode.ToDisplay(DisplayProperty.Name);
        }
        public ApiResult()
        {

        }
        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult(false, ApiResultStatusCode.BadRequest, message);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(true, ApiResultStatusCode.Success, result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(false, ApiResultStatusCode.NotFound);
        }
        #endregion

    }
    public class ApiResult<TData>:ApiResult
        where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }
        public ApiResult(bool isSuccess, ApiResultStatusCode apiResultStatus,TData Data,string message = null)
            :base(isSuccess, apiResultStatus, message)
        {
            this.Data = Data;
        }
        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, data);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            var sel = new ApiResult<TData>(true, ApiResultStatusCode.Success, (TData)result.Value);
            return sel;
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, (TData)result.Value, message);
        }

        public static implicit operator ApiResult<TData>(ContentResult result)
        {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success,null, result.Content);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, (TData)result.Value);
        }
        #endregion
    }
}
