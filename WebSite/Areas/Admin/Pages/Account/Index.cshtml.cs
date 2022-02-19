
using System.Collections.Generic;
using Application.Permissions;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.Admin.Pages.Account
{
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public IndexModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }




        #endregion

        public List<UsersDTO> UsersDTO { get; set; }

        public  void OnGet()
        {           
            UsersDTO =  _userService.FilterUsers();
        }


    }
}
