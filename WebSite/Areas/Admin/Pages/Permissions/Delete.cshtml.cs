using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.Permissions
{
    public class DeleteModel : BasePageModel
    {
        #region Constructor

        private readonly IPermissionService _permissionservice;

        public DeleteModel(IPermissionService permissionservice)
        {
            _permissionservice = permissionservice;
        }


        #endregion



        [BindProperty]

        public DeleteRoleDto Role { get; set; }

        public void OnGet(long roleId)
        {
            Role = _permissionservice.GetRoleForDelete(roleId);
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                var res = _permissionservice.DeleteRole(Role);

                switch (res)
                {
                    case DeleteRoleResult.NotFound:
                        TempData[WarningMessage] = "نقش مورد نظر یافت نشد";
                        break;
                    case DeleteRoleResult.DeleteSuccess:
                        TempData[SuccessMessage] = "نقش مورد نظر باموفقیت حذف شد";
                        return RedirectToPage("./Index");
                   
                }

            }
            return Page();
        }
    }
}
