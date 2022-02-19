
using Application.Permissions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace WebSite.Areas.Admin.Pages.Permissions
{
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly IPermissionService _permissionservice;

        public IndexModel(IPermissionService permissionservice)
        {
            _permissionservice = permissionservice;
        }


        #endregion


        public IEnumerable<RoleDto> Roles { get; set; }

        public void OnGet()
        {
            Roles = _permissionservice.FilterRole();
        }
    }
}
