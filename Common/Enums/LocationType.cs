using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Enums
{
    public enum LocationType
    {
        [Display(Name ="کشور")]
        Country = 1,
        [Display(Name = "استان")]
        Province = 2,
        [Display(Name = "شهر")]
        City = 3,
        [Display(Name = "منطقه")]
        Area = 4,
        [Display(Name = "محله")]
        Neighbourhood = 5
    }
}
