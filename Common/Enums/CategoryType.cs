using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Enums
{
   public enum CategoryType
    {
        [Display(Name ="خوراک")]
        Food = 7,
        [Display(Name = "پوشاک")]
        Clothing = 8,

    }
}
