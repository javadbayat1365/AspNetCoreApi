using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Controllers.v1;
using Api.Models;
using Data.Contracts;
using Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.IServices;
using WebFramework.Api;

namespace Api.Controllers.v2
{
    [ApiVersion("2")]
    public class UserController : v1.UserController
    {
        public UserController(
            IUserRepository userRepository, 
            ILogger<v1.UserController> logger, 
            IJwtService jwtService, 
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, 
            SignInManager<User> signInManager) 
            : base(userRepository, logger, jwtService, userManager, roleManager, signInManager)
        {
        }

        #region This Override Is For Confilict Swagger Not DotNetCore

        public override Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            return base.Create(userDto, cancellationToken);
        }

        public override Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }

        public override Task<ActionResult<List<UserDto>>> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }

        public override Task<ActionResult<User>> Get(long id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }

        public override Task<ActionResult<User>> Update(long id, User user, CancellationToken cancellationToken)
        {
            return base.Update(id, user, cancellationToken);
        }

        public override Task<ActionResult> Token([FromForm] TokenRequest token, CancellationToken cancelationToken)
        {
            return base.Token(token, cancelationToken);
        }
        #endregion
    }
}