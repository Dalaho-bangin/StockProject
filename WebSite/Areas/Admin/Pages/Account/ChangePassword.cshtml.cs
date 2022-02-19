using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.Account
{
    public class ChangePasswordModel : BasePageModel
    {
        #region Constructor

        private readonly IUserService _userService;

        public ChangePasswordModel(IUserService userService)
        {
            _userService = userService;
        }


        #endregion

        

        [BindProperty]
        public ChangePasswordUserDto userDTo { get; set; }

        public void OnGet(long userId)
        {
            userDTo = _userService.GetUserForChangePassword(userId);
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var res = _userService.ChangedPassword(userDTo);

                switch (res)
                {
                    case ChangePasswordUserResult.NotFoundUser:
                        TempData[WarningMessage] = "کاربر مورد نظر یافت نشد";
                        return RedirectToPage("./Index");
                    case ChangePasswordUserResult.SuccessChangedPassword:
                        TempData[SuccessMessage] = "کلمه عبور کاربر مورد نظر با موفقیت تغییر کرد";
                        return RedirectToPage("./Index");
                }
            }

            return Page();
        }
    }
}
