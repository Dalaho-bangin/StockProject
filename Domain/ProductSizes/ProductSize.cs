using Domain.Attributes;
using Domain.Products;
using System.Collections.Generic;


namespace Domain.ProductSizes
{
    [Auditable]
    public class ProductSize
    {
        #region Properties


        public long ProductSizeId { get; set; }

        public long ProductId { get; set; }

        public ICollection<ProductSizeType> ProductSizeTypes { get; set; }

        #endregion

        #region Relations

        public Product Product { get; set; }

        #endregion
    }

    public enum ProductSizeType
    {
        S,
        M,
        L,
        XL,
        XXL
    }
}
