using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using Entities.Store;
using WebFramework.CustomMapping;

namespace Api.CustomMapping
{
    public class StoreCustomMapping : IHaveCustomMapping
    {
        public void CreateMappings(Profile profile)
        {
            profile.CreateMap<Store, StoreDto>().ReverseMap()
                .ForMember(f => f.user, opt => opt.Ignore());//این خط باعث می شود که هنگام مپ شدن استور دی تی ا به استور برای User چیزی مپ نشود
        }
    }
}
