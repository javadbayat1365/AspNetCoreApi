using AutoMapper;
using Entities;
using Marketer.Utilities.GenericMethods;
using System;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;
using WebFramework.CustomMapping;

namespace Api.Models
{
    public class CompanyDto:BaseDto<CompanyDto,Company,long>
    {
        [Required]
        [Display(Name ="نام شرکت")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "آدرس شرکت")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string ShamsiRegisterDate { get; set; }

        public override void CustomMappings(IMappingExpression<Company, CompanyDto> mapping)
        {
            mapping.ForMember(
                opt => opt.ShamsiRegisterDate,
                Conf => Conf.MapFrom(src => src.RegisterDate.ToShamsiDateYMD())
                );
        }
    }
}
