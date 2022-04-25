using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Common
{
   public class BaseInfo:BaseEntity
    {
        public string Name { get; set; }
        public long? ParentBaseInfo_ID { get; set; }

        [ForeignKey(nameof(ParentBaseInfo_ID))]
        public BaseInfo ParentBaseInfo { get; set; }
        public ICollection<BaseInfo> ChildBaseInfos { get; set; }
        public ICollection<Location.Location> Locations { get; set; }
       // public ICollection<Product.Product> CategoryBaseInfos { get; set; }
       // public ICollection<Product.Product> BrandBaseInfos { get; set; }
    }
}
