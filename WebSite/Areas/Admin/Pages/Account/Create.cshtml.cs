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
    public class CreateModel : BasePageModel
    {
        #region Constructor

        private readonly IUserService _userService;

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }


        #endregion

       

        [BindProperty]
        public AddUserDTo user { get; set; }

        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var res = _userService.AddUser(user);

                switch (res)
                {
                    case AddUserResult.SuccessRegister:
                        TempData[SuccessMessage] = "کاربر مورد نظر با موفقیت اضافه شد";
                        return RedirectToPage("./Index");


                    case AddUserResult.IsExistMobileNumber:
                        TempData[WarningMessage] = "شمار تلفن کاربر تکراری می باشد";
                        break;
                    
                }
            }
            return Page();
        }
    }
}
