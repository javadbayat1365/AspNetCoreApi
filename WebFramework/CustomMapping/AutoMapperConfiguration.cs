using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace WebFramework.CustomMapping
{
    public static class AutoMapperConfiguration
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Initialize(config =>
            {
                config.AddCustomMappingProfile();
            });

            //Compile mapping after configuration to boost map speed
            Mapper.Configuration.CompileMappings();
        }

        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config)
        {
            //GetEntryAssembly() = اسمبلی ورودی پروژه 
            config.AddCustomMappingProfile(Assembly.GetEntryAssembly());
        }
        /// <summary>
        /// از طریق Reflection
        /// تمام مپینگ ها شناسایی شده و انجام می شود
        /// </summary>
        /// <param name="config"></param>
        /// <param name="assemblies"></param>
        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

            var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
                type.GetInterfaces().Contains(typeof(IHaveCustomMapping)))
                .Select(type => (IHaveCustomMapping)Activator.CreateInstance(type));

            var profile = new CustomMappingProfile(list);

            config.AddProfile(profile);
        }
    }
}
