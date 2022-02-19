using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ProductCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.ProductCategories
{
    public class CreateModel : BasePageModel
    {
        #region Constructor

        private readonly IProductCategoryService _productCategoryService;

        public CreateModel(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }



        #endregion

        [BindProperty]
        public CreateProductCategoryDto ProductCategory { get; set; }

        public void OnGet()
        {
            ViewData["ProductCategories"] = _productCategoryService.GetAllProductCategories();
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var res = _productCategoryService.Create(ProductCategory);

                switch (res)
                {
                    case CreateProductCategoryResult.CreateSuccess:
                        TempData[SuccessMessage] = $"دسته محصول{ProductCategory.ProductCategoryName} با موفقیت اضافه شد ";
                        return RedirectToPage("./Index");
                    case CreateProductCategoryResult.IsExistProductCategory:
                        TempData[WarningMessage] = "دسته محصول تکراری می باشد";
                        break;
                    
                }
            }
            ViewData["ProductCategories"] = _productCategoryService.GetAllProductCategories();
            return Page();
        }
    }
}
