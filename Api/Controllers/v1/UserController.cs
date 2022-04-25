using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using Common;
using Common.Exceptions;
using Data.Contracts;
using ElmahCore;
using Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services;
using Services.IServices;
using Services.Services;
using WebFramework.Api;
using WebFramework.ApiFilter;

namespace Api.Controllers.v1
{
    [ApiVersion("1")]
    public class UserController : BaseController
    {
        private IUserRepository _userRepository;


        public ILogger<UserController> Logger { get; }
        public IJwtService JwtService { get; }
        public UserManager<User> UserManager { get; }
        public RoleManager<Role> RoleManager { get; }
        public SignInManager<User> SignInManager { get; }

        public UserController(IUserRepository userRepository, ILogger<UserController> logger, IJwtService jwtService,
            UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            _userRepository = userRepository;
            Logger = logger;
            JwtService = jwtService;
        }

        [HttpGet]
        [ApiResultFilter]
        [Authorize(Roles = "Admin")]
        public virtual async Task<ActionResult<List<UserDto>>> Get(CancellationToken cancellationToken)
        {
            //var username = HttpContext.User.Identity.GetUserName();
            //var username1 = HttpContext.User.Identity.Name;
            //var userid = HttpContext.User.Identity.GetUserId();
            //var useridInt = HttpContext.User.Identity.GetUserId<long>();
            //var role = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Role);
            //var phone = HttpContext.User.Identity.FindFirstValue(ClaimTypes.MobilePhone);

            var sel = await _userRepository.TableNoTracking.ToListAsync(cancellationToken);
            if (sel == null)
            {
                return NotFound();
            }
            return Ok(sel);
        }

        [HttpGet("{id:int}")]
        [ApiResultFilter]
        public virtual async Task<ActionResult<User>> Get(long id, CancellationToken cancellationToken)
        {
            var sel = await _userRepository.GetByIdAsync(cancellationToken, id);
            var sel2 = await UserManager.FindByIdAsync(id.ToString());

            if (sel == null)
                return NotFound();

            await _userRepository.UpdateSecurityStampAsync(sel, cancellationToken);

            return Ok(sel);
        }

        [HttpPost]
        [ApiResultFilter]
        [Route("CreateUser")]
        public virtual async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            #region Custom Errors Log For NLog & Elmah

            //Call NLog Method
            //Logger.LogError($"متد Creat با پارامترهای {JsonConvert.SerializeObject(userDto)} فراخوانی شده");

            //Call Elmah Method
            //HttpContext.RiseError(new Exception($"متد Creat با پارامترهای {JsonConvert.SerializeObject(userDto)} فراخوانی شده"));

            #endregion

            User user = new User()
            {
                Gender = userDto.Gender,
                Age = userDto.Age,
                Fullname = userDto.Fullname,
                RegisterDate = DateTime.Now,
                UserName = userDto.UserName,
                Email = userDto.Email
            };

            var sel = await UserManager.CreateAsync(user, userDto.PassWord);

            var sel2 = await RoleManager.CreateAsync(new Role() { Description = "Admin Des", Name = "Admin" });

            var sel3 = await UserManager.AddToRoleAsync(user, "Admin");


            await _userRepository.AddAsync(user, userDto.PassWord, cancellationToken);
            return Ok(userDto);
        }

        [HttpPut]
        [ApiResultFilter]
        public virtual async Task<ActionResult<User>> Update(long id, User user, CancellationToken cancellationToken)
        {
            var Updateuser = _userRepository.GetByIdAsync(cancellationToken, id);
            if (Updateuser == null)
                return NotFound();
            var newuser = new User()
            {
                Age = user.Age,
                Fullname = user.Fullname,
                Gender = user.Gender,
                UserName = user.UserName
            };
            await _userRepository.UpdateAsync(newuser, cancellationToken);
            return Ok(newuser);// new ApiResult(true, Common.Enums.ApiResultStatusCode.Success,"ویرایش کاربر با موفقیت انجام شد");
        }

        [HttpDelete]
        [ApiResultFilter]
        public virtual async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            //var user = await _userRepository.GetByIdAsync(cancellationToken, id);
            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();
            await _userRepository.DeleteAsync(user, cancellationToken);
            return Ok();// new ApiResult(true, Common.Enums.ApiResultStatusCode.Success,"حذف کاربر با موفقیت انجام شد");
        }

        /// <summary>
        /// This Method Generate Jwt Token
        /// </summary>
        /// <param name="token">Information Of Token Request</param>
        /// <param name="cancelationToken"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public virtual async Task<ActionResult> Token([FromForm]TokenRequest token, CancellationToken cancelationToken)
        {

            if (!token.grant_type.Equals("password", StringComparison.OrdinalIgnoreCase))
                throw new AppException(Common.Enums.ApiResultStatusCode.UnAuthorized, "دسترسی شما قابل پذیرش نیست!");


            //var user = await _userRepository.GetByUserAndPassAsync(username, password, cancelationToken);
            var user = await UserManager.FindByNameAsync(token.username);
            if (user == null)
                throw new BadRequestException("این نام کاربری موجود نیست");
            var isPasswordValid = await UserManager.CheckPasswordAsync(user,token.password);
            if (!isPasswordValid)
                throw new BadRequestException("این نام کاربری موجود نیست");

            var jwt = await JwtService.GenerateAsync(user);
            return new JsonResult(jwt);
        }
    }
}