using Domain.Attributes;
using Domain.Products;


namespace Domain.ProductColors
{
    [Auditable]
    public class ProductColor
    {
        #region properties

        public long ProductColorId { get; set; }

        public long ProductId { get; set; }

        public string ColorCode { get; set; }

        public int Price { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }


        #endregion
    }
}
