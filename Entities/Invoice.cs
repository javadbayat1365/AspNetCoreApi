using Common.Enums;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Invoice : BaseEntity
    {
        public long User_ID { get; set; }
        public long ProductCompany_ID { get; set; }
        public long Store_ID { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Count { get; set; }
        public StatusInvoiceType Status{get;set;}

        [ForeignKey(nameof(User_ID))]
        public User.User User { get; set; }


        [ForeignKey(nameof(ProductCompany_ID))]
        public Product.ProductCompany  ProductCompany{ get; set; }

        [ForeignKey(nameof(Store_ID))]
        public Store.Store Store { get; set; }
    }
}
