using Entities.Common;
using Entities.Product;
using System;
using System.Collections.Generic;

namespace Entities
{
   public class Company:BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public DateTime RegisterDate { get; set; }

        public ICollection<ProductCompany> ProductCompanies { get; set; }

    }
}
