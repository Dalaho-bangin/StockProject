using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Permissions;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.Account
{
    public class EditUserRoleModel : BasePageModel
    {

        #region Constructor

        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public EditUserRoleModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }




        #endregion

    

        [BindProperty]
        public AddUserRolesDTo AddUserRoles { get; set; }

        public void OnGet(long userId)
        {
            ViewData["userId"] = userId;

            ViewData["Roles"] = _permissionService.FilterRole();
            ViewData["SelectedRoles"] = _userService.SelectedRoles(userId);
        }

        public IActionResult OnPost()
        {
            var res = _userService.EditUserRoles(AddUserRoles);

            switch (res)
            {
                case EditUserRoleResult.EditSuccess:
                    TempData[SuccessMessage] = "نقش کاربر با موفقیت ویرایش شد";
                    return RedirectToPage("./Index");
                case EditUserRoleResult.NotFound:
                    TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد";
                    break;
             
            }

            ViewData["userId"] = AddUserRoles.UserId;
            ViewData["Roles"] = _permissionService.FilterRole();
            ViewData["SelectedRoles"] = _userService.SelectedRoles(AddUserRoles.UserId);
            return Page();
        }
    }
}
