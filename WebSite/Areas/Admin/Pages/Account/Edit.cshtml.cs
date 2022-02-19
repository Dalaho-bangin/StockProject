using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.Account
{
    public class EditModel : BasePageModel
    {
        #region Constructor

        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }


        #endregion

       

        [BindProperty]
        public EditUserDTo user { get; set; }

        public IFormFile AvatarFile { get; set; }

        public void OnGet(long userId)
        {
            user = _userService.GetUserForEdit(userId);
        }

        public IActionResult OnPost()
        {
            ModelState.Remove("Password");
            if(ModelState.IsValid)
            {
                var res = _userService.Edit(user, AvatarFile);

                switch (res)
                {
                    case EditUserResult.SuccessEdit:
                        TempData[SuccessMessage] = "کاربر مورد نظر با موفقیت ویرایش شد";

                        return RedirectToPage("./Index");
                   
                    case EditUserResult.IsExistMobileNumber:
                        TempData[WarningMessage] = "شماره تلفن وارد شده تکراری می باشد";
                        break;
                    case EditUserResult.NotFoundUser:
                        TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد";
                        break;
                    
                }
            }
            return Page();
        }
    }
}
