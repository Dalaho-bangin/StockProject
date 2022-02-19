using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ProductCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.ProductCategories
{
    public class DeleteModel : BasePageModel
    {
        #region Constructor

        private readonly IProductCategoryService _productCategoryService;

        public DeleteModel(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        #endregion

        [BindProperty]
        public DeleteProductCategory ProductCategory { get; set; }

        public void OnGet(long productCategoryId)
        {
            ProductCategory = _productCategoryService.FindProductCategoryForDelete(productCategoryId);
        }

        public IActionResult OnPost()
        {
            var res = _productCategoryService.Delete(ProductCategory);
            switch (res)
            {
                case DeleteProductCategoryResult.DeleteSuccess:
                    TempData[SuccessMessage] = $"دسته ی محصول{ProductCategory.ProductCategoryName} با موفقیت حذف شد";
                    return RedirectToPage("./index");
                case DeleteProductCategoryResult.NotFound:
                    TempData[WarningMessage] = "دسته ی محصول مورد نظر یافت نشد";
                    break;
                
            }

            return Page();
        }
    }
}
