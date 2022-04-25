using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Enums
{
    public enum ApiResultStatusCode
    {
        [Display(Name ="عملیات موفق")]
        Success = 1,

        [Display(Name = "خطا سمت سرور")]
        ServerError = 2,

        [Display(Name = "پارامترها نا معتبر")]
        BadRequest = 3,

        [Display(Name = "پیدا نشد")]
        NotFound = 4,

        [Display(Name ="لیست خالی")]
        ListEmpty = 5,
        [Display(Name = "خطلایی در پردازش رخ داده")]
        LogicError = 6,
        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 7
    }
}
