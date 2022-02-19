using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.Permissions
{
    public class EditModel : BasePageModel
    {
        #region Constructor

        private readonly IPermissionService _permissionservice;

        public EditModel(IPermissionService permissionservice)
        {
            _permissionservice = permissionservice;
        }


        #endregion




        [BindProperty]

        public EditRoleDto Role { get; set; }

        public void OnGet(long roleId)
        {
            Role = _permissionservice.GetRoleForEdit(roleId);
            ViewData["Permission"] = _permissionservice.GetAllPermission();
            ViewData["SelectedPermission"] = _permissionservice.GetPermissionsRole(roleId);
        }

        public IActionResult OnPost(List<long> SelectedPermission)
        {
            if(ModelState.IsValid)
            {
                var res = _permissionservice.EditRole(Role, SelectedPermission);

                switch (res)
                {
                    case EditRoleResult.SuccessEdit:
                        TempData[SuccessMessage] = "نقش مورد نظر با موفقیت ویرایش شد";
                        return RedirectToPage("./Index");
                    case EditRoleResult.IsExistRoleTitle:
                        TempData[WarningMessage] = "نقش تکراری می باشد";
                        break;
                    case EditRoleResult.NotFound:
                        TempData[ErrorMessage] = "نقش مورد نظر یافت نشد";
                        break;
                    
                }
            }
            ViewData["Permission"] = _permissionservice.GetAllPermission();
            ViewData["SelectedPermission"] = _permissionservice.GetPermissionsRole(Role.RoleId);
            return Page();
        }
    }
}
