using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Common.Settings;
using Data;
using Data.Contracts;
using Data.Repositories;
using Entities.Common;
using Microsoft.Extensions.DependencyInjection;
using Services.IServices;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        public static IServiceProvider BuildAutofacServiceProvicer(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            //این خط تمام سرویس هایی که به 
            //services in startup.cs
            //اضافه شده را بصورت اتوماتیک به Autofac اضافه میکنه
            containerBuilder.Populate(services);
            containerBuilder.AddServices();
            //Regiser services to autofac containerBuilder
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            //container.Register(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //container.AddScoped<IUserRepository, UserRepository>();
            //container.Register<IJwtService, JwtService>();


            //بجای کدهای بالا در Startup.cs از کد های پایین استفاده میشه در Autofac
            //containerBuilder.RegisterType<JwtService>().As<IJwtService>().InstancePerLifetimeScope();
            //containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            #region Add Automaticly Register Classes In IOC that Inherit Of IScopeDependency,ITrancientDependency,ISingletonDependendy
                var CommonAssembly = typeof(JwtSettings).Assembly;
                var EntitiesAssembly = typeof(IEntity).Assembly;
                var DataAssembly = typeof(ApplicationDbContext).Assembly;
                var ServicesAssembly = typeof(JwtService).Assembly;
                //کلاسهایی که از
                //IAsMarkScopeDependency
                //ارثبری کنند بصورت اتوماتیک تزریق وابستگی روی انها اعمال می شود
                containerBuilder.RegisterAssemblyTypes(CommonAssembly, EntitiesAssembly, DataAssembly, ServicesAssembly)
                        .AssignableTo<IAsMarkScopeDependency>()
                        .AsImplementedInterfaces()
                        .InstancePerLifetimeScope();
                //کلاسهایی که از
                //IAsMarkTransientDependency
                //ارثبری کنند بصورت اتوماتیک تزریق وابستگی روی انها اعمال می شود
                containerBuilder.RegisterAssemblyTypes(CommonAssembly, EntitiesAssembly, DataAssembly, ServicesAssembly)
                        .AssignableTo<IAsMarkTransientDependency>()
                        .AsImplementedInterfaces()
                        .InstancePerDependency();
                //کلاسهایی که از
                //IAsMarkSingletonDependency
                //ارثبری کنند بصورت اتوماتیک تزریق وابستگی روی انها اعمال می شود
                containerBuilder.RegisterAssemblyTypes(CommonAssembly, EntitiesAssembly, DataAssembly, ServicesAssembly)
                        .AssignableTo<IAsMarkSingletonDependency>()
                        .AsImplementedInterfaces()
                        .SingleInstance();
            #endregion


        }
    }
}
