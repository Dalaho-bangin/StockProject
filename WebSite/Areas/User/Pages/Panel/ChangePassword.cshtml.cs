using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserPanel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.User.Pages.Panel
{
    public class ChangePasswordModel : BasePageModel
    {
        #region Constructor

        private readonly IUserPanelService _userPanelService;

        public ChangePasswordModel(IUserPanelService userPanelService)
        {
            _userPanelService = userPanelService;
        }

        #endregion

        [BindProperty]

        public ChangePasswordDto UserPanel { get; set; }

        public async Task OnGet(long userId)
        {
            UserPanel =await _userPanelService.FindUserForChangePassword(userId);
        }

        public async Task<IActionResult>OnPost()
        {
            if(ModelState.IsValid)
            {
                var res =await _userPanelService.ChangePassword(UserPanel);

                switch (res)
                {
                    case ChangePassworResult.ChangedSuccess:
                        TempData[SuccessMessage] = "کلمه عبور با موفقیت تغییر کرد";
                        return RedirectToPage("./Index");
                    case ChangePassworResult.NotFound:
                        TempData[ErrorMessage] = "تغییر کلمه عبور با خطا مواجه شد";
                        break;
                    case ChangePassworResult.InvalidPassword:
                        TempData[WarningMessage] = "کلمه عبور شما صحیح نمی باشد";
                        break;
                }

            }
            return Page();
        }
    }
}
