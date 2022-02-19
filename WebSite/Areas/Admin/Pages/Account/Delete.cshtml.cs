using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.Account
{
    public class DeleteModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;

        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }


        #endregion

        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";


        [BindProperty]
        public DeleteUserDTo userDto { get; set; }

        public void OnGet(long userId)
        {
            userDto = _userService.GetUserForDelete(userId);
        }

        
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var res = _userService.Delete(userDto);

                switch (res)
                {
                    case DeleteUserResult.NotFoundUser:
                        TempData[WarningMessage] = "کاربر مورد نظر یافت نشد";
                        return RedirectToPage("./Index");
                    case DeleteUserResult.DeleteSuccess:
                        TempData[SuccessMessage] = "کاربر مورد نظر با موفقیت حذف شد";
                        return RedirectToPage("./Index");
                }
            }

            return Page();
        }
    }
}
