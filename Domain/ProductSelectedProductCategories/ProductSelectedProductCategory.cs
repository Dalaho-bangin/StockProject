using Domain.Attributes;
using Domain.ProductCategories;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductSelectedProductCategories
{
    [Auditable]
    public class ProductSelectedProductCategory
    {
        #region Properties

        public long ProductSelectedProductCategoryId { get; set; }

        public long ProductId { get; set; }

        public long ProductCategoryId { get; set; }

        #endregion

        #region Relations

        public Product Product { get; set; }

        public ProductCategory ProductCategory { get; set; }

        #endregion
    }
}
