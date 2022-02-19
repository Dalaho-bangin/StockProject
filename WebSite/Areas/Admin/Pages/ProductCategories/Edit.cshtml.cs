using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ProductCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.ProductCategories
{
    public class EditModel : BasePageModel
    {
        #region Constructor

        private readonly IProductCategoryService _productCategoryService;

        public EditModel(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }



        #endregion

        [BindProperty]
        public CreateProductCategoryDto ProductCategory { get; set; }

        public void OnGet(long productCategoryId)
        {
            ViewData["ProductCategories"] = _productCategoryService.GetAllProductCategories();
            ProductCategory = _productCategoryService.FindProductCategoryForEdit(productCategoryId);
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var res = _productCategoryService.Edit(ProductCategory);
                switch (res)
                {
                    case EditProductCategoryResult.EditSuccess:
                        TempData[SuccessMessage] = $"دسته بندی {ProductCategory.ProductCategoryName} با موفقیت ویرایش شد";
                        return RedirectToPage("./Index");
                    case EditProductCategoryResult.IsExistProductCategory:
                        TempData[WarningMessage] = "عنوان دسته بندی تکراری می باشد";
                        break;
                    case EditProductCategoryResult.NotFound:
                        TempData[ErrorMessage] = "دسته بندی مورد نظر یافت نشد";
                        break;
                  
                }
            }

            ViewData["ProductCategories"] = _productCategoryService.GetAllProductCategories();
            return Page();
        }
    }
}
