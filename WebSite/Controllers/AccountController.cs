using Application.Users;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebSite.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Constructor

        private readonly ICaptchaValidator _captchaValidator;

        private readonly IUserService _userService;

        public AccountController(ICaptchaValidator captchaValidator, IUserService userService)
        {
            _captchaValidator = captchaValidator;
            _userService = userService;
        }


        #endregion


        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return PartialView("_Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            //if (!await _captchaValidator.IsCaptchaPassedAsync(dto.Captcha))
            //{
            //    TempData[ErrorMessage] = "کد کپچای شما تایید نشد";
            //    return View(dto);
            //}

            ModelState.Remove("Captcha");
            if (ModelState.IsValid)
            {
                var res = _userService.Register(dto);

                switch (res)
                {
                    case RegisterResult.SuccessRegister:
                        TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد";
                        TempData[InfoMessage] = "کد تایید تلفن همراه برای شما ارسال شد";
                        return RedirectToAction("Index");
                    case RegisterResult.ErrorRegister:
                        TempData[ErrorMessage] = "ثبت نام شما انجام نشد";       
                        break;
                    case RegisterResult.IsExistMobileNumber:
                        TempData[ErrorMessage] = "تلفن همراه وارد شده تکراری می باشد";
                        ModelState.AddModelError("Mobile", "تلفن همراه وارد شده تکراری می باشد");
                        break;
                }
            }
            return PartialView("_Register", dto);
        }

        #endregion


        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView("_Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Login(LoginDTO dTO)
        {

            //if (!await _captchaValidator.IsCaptchaPassedAsync(dto.Captcha))
            //{
            //    TempData[ErrorMessage] = "کد کپچای شما تایید نشد";
            //    return View(dto);
            //}

            ModelState.Remove("Captcha");

            if(ModelState.IsValid)
            {
                var res = _userService.Login(dTO);

                switch (res)
                {
                    case LoginResult.NotFound:
                        TempData[WarningMessage] = "حساب کاربری با این مشخصات یافت نشد";
                        break;

                    case LoginResult.NotActivated:
                        TempData[WarningMessage] = "حساب کاربری شما فعال نمی باشد";
                        break;

                    case LoginResult.Success:

                        var user =  _userService.GetUserByMobile(dTO.Mobile);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.Mobile),
                            new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString())
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = dTO.RememberMe
                        };

                        await HttpContext.SignInAsync(principal, properties);

                        TempData[SuccessMessage] = "عملیات ورود با موفقیت انجام شد";
                        return Redirect("/");
                }
            }

            return RedirectToAction("Login");
        }

        #endregion

    }
}
