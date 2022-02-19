using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UserPanel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Areas.User.Pages.Panel
{
    public class EditModel : BasePageModel
    {
        #region Constructor

        private readonly IUserPanelService _userPanelService;

        public EditModel(IUserPanelService userPanelService)
        {
            _userPanelService = userPanelService;
        }

        #endregion

        [BindProperty]

        public EditUserPanelDto UserPanel { get; set; }

        public async Task OnGet(long userId)
        {
            UserPanel =await _userPanelService.FindUserForEditPanel(userId);
        }

        public async Task<IActionResult>OnPost()
        {
            if(ModelState.IsValid)
            {
                var res =await _userPanelService.Edit(UserPanel);

                switch (res)
                {
                    case EditUserPanelResult.EditSuccess:
                        TempData[SuccessMessage] = "تغییرات شما با موفقیت ذخیره شد";
                        return RedirectToPage("./Index");
                    case EditUserPanelResult.NotFound:
                        TempData[ErrorMessage] = "تغییرات اعمال نشد";
                        break;
                    case EditUserPanelResult.AvatarHasNotImage:
                        TempData[WarningMessage] = "تصویر پروفایلی شما درست انتخاب نشده است";
                        break;
                }
            }
            return Page();
        }
    }
}
