using Domain.Attributes;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductFeatures
{
    [Auditable]
    public class ProductFeature
    {
        #region properties

        public long ProductFeatureId { get; set; }

        public long ProductId { get; set; }

        public string FeatureTitle { get; set; }


        public string FeatureValue { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        #endregion
    }
}
