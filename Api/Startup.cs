using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Settings;
using Data;
using Data.Contracts;
using Data.Repositories;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Entities.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.IServices;
using Services.Services;
using Swashbuckle.AspNetCore.Swagger;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.MiddleWares;
using WebFramework.Swagger;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly SiteSettings _siteSettings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            //AutoMapper Add Mapping Once In Startup.cs
            AutoMapperConfiguration.InitializeAutoMapper();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //تزریق وابستگی اپ ستینگ برای استفاده در لایه کنترولر
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddDbContext(Configuration);

            services.AddCustomIdentity(_siteSettings.IdentitySettings);

            services.AddMinimalMvc();

            #region Elmah-Setting

                //برای لاگ کردن الماه کافیه خط پایین رو آنکامنت کنیم
                //و از متد روبرو استفاده شود => HttpContext.RiseError 
                //برای جاهایی که دستی میخواهیم لاگ بگیریم

                services.AddCustomElmah(Configuration, _siteSettings);
            #endregion

            //اضافه کردن جی دبلیو تی به ریکوست های پروژه
            services.AddJwtAuthentication(_siteSettings.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddSwagger();

            #region AutoFac ServiceProvider(IOC Provider) //از این به بعد اگر کلاسی برای تزریق وابستگی درخواست کنیم از طریق Autofac برای ما ساخته شده و ارسال میشود و و تزریق وابستگی دات نت کور استفاده نمیکنیم
            return services.BuildAutofacServiceProvicer();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.CustomeExceptionHandler();

            app.UseHsts(env);

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseSwaggerAndUI();
           
            #region Elmah-Settings
            //برای لاگ کردن الماه کافیه خط پایین رو آنکامنت کنیم
            //app.UseElmah();
            #endregion

            app.UseMvc();
        }
    }
}
