using AutoMapper;
using Entities.Store;
using Marketer.Utilities.GenericMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace Api.Models
{
    public class StoreDto : BaseDto<StoreDto,Store,long> , IValidatableObject
    {
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string SellerName { get; set; }
        public string Plaque { get; set; }
        public short vahed { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public DateTime? RegisterDate { get; set; } = DateTime.Now;
        public string Lat { get; set; }
        public string Lng { get; set; }
        public long User_ID { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (Mobile.Length < 11)
                yield return new ValidationResult("موبایل کمتر از 11 کاراکتر است", new string[] { nameof(Mobile) });
        }
    }

    public class StoreSelectDto : BaseDto<StoreSelectDto, Store, long>
    {
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string SellerName { get; set; }
        public string Plaque { get; set; }
        public short vahed { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public long User_ID { get; set; }
        //[IgnoreMap]
        public string ShamsiRegisterDate { get; set; }

        public override void CustomMappings(IMappingExpression<Store, StoreSelectDto> mapping)
        {
            mapping.ForMember(
                dest => dest.ShamsiRegisterDate,
                config => config.MapFrom(src => src.RegisterDate.ToShamsiDateYMD()));
        }
    }


}
