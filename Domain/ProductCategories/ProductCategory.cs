using Domain.Attributes;
using Domain.ProductSelectedProductCategories;
using System.Collections.Generic;


namespace Domain.ProductCategories
{
    [Auditable]
    public class ProductCategory
    {
        #region Properties

        public long ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; }

        public string Slug { get; set; }

        public long? ParentProductCategoryId { get; set; }

        public string ParentProductCategoryName { get; set; }

        #endregion

        #region Relations

        public ICollection<ProductSelectedProductCategory> ProductSelectedProductCategories{ get; set; }

        public ICollection<ProductCategory> ParentProductCategories { get; set; }


        #endregion

    }
}
