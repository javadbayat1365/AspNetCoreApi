using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using Entities.User;
using WebFramework.CustomMapping;

namespace Api.CustomMapping
{
    public class UserCustomMapping : IHaveCustomMapping
    {
        public void CreateMappings(Profile profile)
        {
            profile.CreateMap<User, UserDto>().ReverseMap()
                .ForMember(f => f.invoices, opt => opt.Ignore())
                .ForMember(f => f.stores, opt => opt.Ignore());
        }
    }
}
