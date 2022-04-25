using Common;
using Common.Enums;
using Common.Exceptions;
using Common.Settings;
using Common.Utilities;
using Data;
using Data.Contracts;
using Entities.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace WebFramework.Configuration
{
   public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services,JwtSettings JwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(JwtSettings.SecretKey);
                var encriptKey = Encoding.UTF8.GetBytes(JwtSettings.EncriptKey);
                var validationParameters = new TokenValidationParameters
                {
                    //تلورانس زمانی برای انقضا توکن و شروع کار با توکن
                    ClockSkew = TimeSpan.FromMinutes(JwtSettings.ClockSkew),// default: 5 min
                    //آیا توکن های ارسالی حتما بیاد امضا داشته باشند؟
                    RequireSignedTokens = true,
                    //آیا امضا توکن مورد اعتبارسنجی قرار بگیر؟
                    ValidateIssuerSigningKey = true,
                    //این خط اعتبار سنجی امضا توکن رو انجام میده
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),
                    //اعتبارسنجی زمانی توکن بررسی شود؟
                    RequireExpirationTime = true,
                    ValidateLifetime = true,


                    ValidateAudience = true, //default : false
                    ValidAudience = JwtSettings.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = JwtSettings.Issure,

                    TokenDecryptionKey = new SymmetricSecurityKey(encriptKey)
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                //به ازای یک سری رویداد در بر روی توکن یکسری کار انجام میدهیم
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        //logger.LogError("Authentication failed.", context.Exception);

                        if (context.Exception != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity.Claims?.Any() != true)
                            context.Fail("This token has no claims.");

                        var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                        if (!securityStamp.HasValue())
                            context.Fail("This token has no secuirty stamp");

                        //Find user and token from database and perform your custom validation
                        var userId = claimsIdentity.GetUserId<long>();
                        var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

                        //if (user.SecurityStamp != Guid.Parse(securityStamp).ToString())
                        //    context.Fail("Token secuirty stamp is not valid.");

                        var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                        if (validatedUser == null)
                            context.Fail("Token secuirty stamp is not valid.");

                        if (!user.IsActive)
                            context.Fail("User is not active.");

                        await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
                    },
                    OnChallenge = context =>
                    {
                        //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);

                        if (context.AuthenticateFailure != null)
                            throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                        throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);

                        //return Task.CompletedTask;
                    }
                };
            });
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MyApiConnectionString"));
            });
        }

        public static void AddMinimalMvc(this IServiceCollection services)
        {
            //https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
            services.AddMvcCore(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            })
            .AddApiExplorer() // مسیریابی API - For Example Swager
            .AddAuthorization() // احراز هویت
            .AddFormatterMappings() // mapping result format in API
            .AddDataAnnotations()
            .AddJsonFormatters(/*options =>
            {
                options.Formatting = Newtonsoft.Json.Formatting.Indented;
                options.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }*/)
            //.AddCors()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvcCore(options =>
            {
                //باعث میشه تمام اکشن ها در تمام کنترلرها نیاز به اعتبارسنجی داشته باشند به غیر از
                //[Authorize(Roles = "...")] Or [AllowAnonymous]
                options.Filters.Add(new AuthorizeFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public static void AddCustomElmah(this IServiceCollection services,IConfiguration configuration,SiteSettings siteSettings)
        {
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.Path = siteSettings.ElmahPath;
                options.ConnectionString = configuration.GetConnectionString("LoggingConnectionString");
            });
        }

        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(option => {
                option.AssumeDefaultVersionWhenUnspecified = true; // Default = false
                option.DefaultApiVersion = new ApiVersion(1,0); //v1.0 = v1 / Default = 1
                option.ReportApiVersions = true; //در جواب هر درخواست درمورد ورژن ها اطلاعات میدهد که مثلا ورژن یک منصوخ شده و از ورژن 2 باید استفاده کرد
                //option.ApiVersionReader = new QueryStringApiVersionReader("api-version"); //api/store?api-version=1
                //option.ApiVersionReader = new UrlSegmentApiVersionReader(); // Default , //api/v1/store
                //option.ApiVersionReader = new HeaderApiVersionReader(new[] { "api-version" }); // in header => api-verison : 1
                //option.ApiVersionReader = new MediaTypeApiVersionReader();
                //option.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"), new HeaderApiVersionReader(new[] { "api-version" }), new UrlSegmentApiVersionReader()); // Or

                
            });
        }
    }
}
