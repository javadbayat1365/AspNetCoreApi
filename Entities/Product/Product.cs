using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Product
{
   public class Product:BaseEntity
    {
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public long BaseInfo_Category_ID { get; set; }
        public long BaseInfo_Brand_ID { get; set; }

        [ForeignKey(nameof(BaseInfo_Category_ID))]
        public BaseInfo CategoryBaseInfo { get; set; }

        [ForeignKey(nameof(BaseInfo_Brand_ID))]
        public BaseInfo BrandBaseInfo { get; set; }

        public ICollection<ProductImages> ProductImages { get; set; }
        public ICollection<ProductCompany> ProductCompanies { get; set; }
    }
}
