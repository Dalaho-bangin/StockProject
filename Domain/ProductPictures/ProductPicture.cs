using Domain.Attributes;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductPictures
{
    [Auditable]
    public class ProductPicture
    {
        #region Properties

        public long ProductPictureId { get; set; }

        public long ProductId { get; set; }

        public string Picture { get; set; }

        public string PictureAlt { get; set; }

        public string PictureTitle { get; set; }

        #endregion

        #region Relations

        public Product Product { get; set; }

        #endregion
    }
}
