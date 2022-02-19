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
    public class AddUserRoleModel : BasePageModel
    {
        #region Constructor

        private readonly IPermissionService _permissionservice;
        private readonly IUserService _userService;

        public AddUserRoleModel(IPermissionService permissionservice, IUserService userService)
        {
            _permissionservice = permissionservice;
            _userService = userService;
        }




        #endregion

       

        [BindProperty]
        public AddUserRolesDTo AddUserRoles { get; set; }

        public void OnGet(long userId)
        {

            ViewData["userId"] = userId;

            ViewData["Roles"] = _permissionservice.FilterRole();
        }

        public IActionResult OnPost()
        {
            if(AddUserRoles.RolesId !=null)
            {
                var res = _userService.AddUserRole(AddUserRoles);

                switch (res)
                {
                    case AddUserRoleResult.Success:
                        TempData[SuccessMessage] = "نقش های کاربر با موفقیت اضافه شد";
                        return RedirectToPage("./Index");
                    case AddUserRoleResult.NotFound:
                        TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد";
                        break;
                    
                }
            }
            ViewData["userId"] = AddUserRoles.UserId;
            ViewData["Roles"] = _permissionservice.FilterRole();
            return Page();
        }
    }
}
