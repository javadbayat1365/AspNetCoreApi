using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WebFramework.ApiFilter;

namespace WebFramework.Api
{
    [Route("api/v{version:apiVersion}/[controller]")] //api/v2/[controller]
    [ApiController]
    //[AllowAnonymous]
    [ApiResultFilter]
    public class BaseController : ControllerBase
    {
        public bool UserIsAuthenticated => HttpContext.User.Identity.IsAuthenticated;
    }
}
