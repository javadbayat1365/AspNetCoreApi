using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Enums
{
    public enum StatusInvoiceType
    {
        [Display(Name ="ثبت موقت")]
        Temporary = 1,
        [Display(Name = "ثبت نهایی")]
        Registred = 2,
        [Display(Name = "تایید شده")]
        Confirmed = 3,
        [Display(Name = "برگشت خورده")]
        Rejected = 4,
        [Display(Name = "حذف شده")]
        Deleted = 5,

    }
}
