using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Product
{
   public class ProductCompany:BaseEntity
    {
        public long Company_ID { get; set; }
        public long Product_ID { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "مبلغ واحد برای مغازه دار")]
        [Description("مبلغ واحد برای مغازه دار")]
        [Required(ErrorMessage = "مبلغ واحد مغازه دار را حتما وارد کنید")]
        public decimal StorePriceUnit { get; set; }

        [Display(Name = "مبلغ واحد برای مشتری")]
        [Description("مبلغ واحد برای مشتری")]
        [Required(ErrorMessage = "مبلغ واحد مشتری را حتما وارد کنید")]
        public decimal CustomerPriceUnit { get; set; }


        [Display(Name = "مبلغ کل برای مغازه دار")]
        [Description("مبلغ کل برای مغازه دار")]
        [Required(ErrorMessage = "مبلغ واحد کل را حتما وارد کنید")]
        public decimal StoreTotalPrice { get; set; }


        [Display(Name = "مبلغ کل برای مشتری")]
        [Description("مبلغ کل برای مشتری")]
        [Required(ErrorMessage = "مبلغ واحد مشتری را حتما وارد کنید")]
        public decimal CustomerTotalPrice { get; set; }


        [Display(Name = "توضیحات مهم و اضافی")]
        public string Description { get; set; }

        [Display(Name = "میزان پورسانت بازاریاب به درصد")]
        public short Commission { get; set; }

        [ForeignKey(nameof(Product_ID))]
        public Product Product { get; set; }
        [ForeignKey(nameof(Company_ID))]
        public Company Company { get; set; }
        public ICollection<Invoice> Invoices{ get; set; }

    }
}
