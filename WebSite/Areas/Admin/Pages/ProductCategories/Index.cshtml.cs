using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ProductCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.ProductCategories
{
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly IProductCategoryService _productCategoryService;

        public IndexModel(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }


        #endregion


        public IEnumerable<ProductCategoryDto> ProductCategory { get; set; }

        public void OnGet()
        {
            ProductCategory = _productCategoryService.ListProductCategories();
        }
    }
}
