using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserPanel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebSite.PresentationExtensions;

namespace WebSite.Areas.User.Pages.Panel
{
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly IUserPanelService _userPanelService;

        public IndexModel(IUserPanelService userPanelService)
        {
            _userPanelService = userPanelService;
        }

        #endregion


        [BindProperty]

        public UserPanelDto UserPanel { get; set; }

        public async Task OnGet()
        {
            UserPanel =await _userPanelService.Panel(User.GetUserId());
        }
    }
}
