using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Enums
{
   public enum BrandType
    {
        [Display(Name ="سالوت")]
        Salut = 9,
        [Display(Name = "بیژن")]
        Bijan = 10,
        [Display(Name = "دلپذیر")]
        Delpazir = 11,
    }
}
