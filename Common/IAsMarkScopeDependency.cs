using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// این کلاس ها پیاده سازی ندارند و فقط برای ارثبری ازشون استفاده میشه تا بصورت اتوماتیک کلاس هایی که از این ها ارثبری کردن رو شناسایی کنیم
/// و بصورت خودکار عمل IOC توسط Autofac رو پیاده سازی کنیم
/// </summary>
namespace Common
{
   public interface IAsMarkScopeDependency
    {
    }
   public interface IAsMarkTransientDependency
    {
    }
   public interface IAsMarkSingletonDependency
    {
    }
}
