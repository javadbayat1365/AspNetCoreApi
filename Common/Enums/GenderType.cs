using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Enums
{
   public enum GenderType
    {
        [Display(Name ="مرد")]
        Male = 1,
        [Display(Name = "زن")]
        Female = 2
    }
}
