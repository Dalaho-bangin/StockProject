using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Permissions;
using Domain.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.Permissions
{
    public class CreateModel : BasePageModel
    {
        #region Constructor

        private readonly IPermissionService _permissionservice;

        public CreateModel(IPermissionService permissionservice)
        {
            _permissionservice = permissionservice;
        }


        #endregion


      




        [BindProperty]
        public CreateRoleDto Role { get; set; }


        public void OnGet()
        {
            ViewData["Permission"] = _permissionservice.GetAllPermission();
        }

        public IActionResult OnPost(List<long> SelectedPermission)
        {
            if(ModelState.IsValid)
            {
                var res = _permissionservice.Create(Role, SelectedPermission);

                switch (res)
                {
                    case CreateRoleResult.SuccessCreate:
                        TempData[SuccessMessage] = "نقش با موفقیت ایجاد شد";

                        return RedirectToPage("./Index");
                    case CreateRoleResult.IsExistRoleTitle:
                        TempData[WarningMessage] = "نقش تکراری می باشد";
                        break;
                    default:
                        break;
                }
            }

            return Page();
        }
    }
}
