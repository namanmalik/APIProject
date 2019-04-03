using APIPROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int ProductQty { get; set; }

        public float ProductPrice { get; set; }

        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }

      public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        public int ProductCategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}