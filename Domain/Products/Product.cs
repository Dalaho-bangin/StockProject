using Domain.Attributes;
using Domain.ProductColors;
using Domain.ProductFeatures;
using Domain.ProductPictures;
using Domain.ProductSelectedProductCategories;
using Domain.ProductSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
    [Auditable]
    public class Product
    {
        #region Properties

        public long ProductId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public string Picture { get; set; }

        public string PictureAlt { get; set; }

        public string PictureTitle { get; set; }

        public string Keywords { get; set; }

        public string MetaDescription { get; set; }

        public string Slug { get; set; }

        #endregion

        #region Relations

        public ICollection<ProductPicture> ProductPictures { get; set; }

        public ICollection<ProductColor> ProductColors { get; set; }

        public ICollection<ProductSize> ProductSizes { get; set; }

        public ICollection<ProductFeature> ProductFeatures { get; set; }

        public ICollection<ProductSelectedProductCategory> ProductSelectedProductCategories { get; set; }

        #endregion

    }
}
