using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;
using System.Security.Claims;

namespace ClientSide.Controllers
{
    public class IdentityController : Controller
    {

        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult RegisterByMobile()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterByMobile(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.RegisterByPhoneNumber(model);
                if (res == 1)
                {
                    TempData["success"] = "ثبت نام با موفقیت انجام شد";
                    return RedirectToAction("ConfrimMobile", new { id = model.PhoneNumber });
                }
                if (res == -100)
                {
                    TempData["error"] = "این شماره موبایل قبلا در سایت ثبت نام کرده است";
                    return View(model);
                }
                if (res == -50)
                {
                    TempData["warning"] = "بعد 3 دقیقه دوباره تلاش کنید ";
                    return View(model);
                }
            }
            TempData["error"] = "کاربر گرامی متاسفانه ثبت نام موفقیت آمیز نبود  ";
            return View(model);
        }

        [HttpGet]
        [Route("ConfrimMobile/{id}")]
        public IActionResult ConfrimMobile(string id)
        {
            bool isMobileExist = _identityService.IsPhoneNumberExist(id);
            if (isMobileExist == false)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.PhoneNumber = id;
            return View();
        }

        [HttpPost]
        [Route("ConfrimMobile/{id}")]
        public IActionResult ConfrimMobile(ConfrimPhoneViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.ConfrimPhoneNumber(model.PhoneNumber, model.code);
                if (res == 1)
                {


                    int userId = _identityService.GetUserIdByPhoneNumber(model.PhoneNumber);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                        new Claim(ClaimTypes.Name,model.PhoneNumber)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(principal, properties);

                    TempData["success"] = "حساب کاربری شما با موفقیت تایید شد";
                    return RedirectToAction("Index", "Home");
                }
                if (res == -100)
                {
                    TempData["error"] = "کاربر یافت نشد";
                    return View();
                }
                if (res == -50)
                {
                    TempData["warning"] = " کد تایید منقضی شده ";
                    return View(model);
                }
                if (res == -200)
                {
                    TempData["warning"] = " کد تایید تطابق ندارد ";
                    return View();
                }
            }
            TempData["error"] = " خطا در تایید کد ";
            return View(model);
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult LoginByMobile()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginByMobile(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.GetUserStatusForLoginByPhoneNumber(model.PhoneNumber, model.Password);
                if (res == -100)
                {
                    TempData["error"] = "حساب کاربری وجود ندارد";
                    return View();
                }
                if (res == -50)
                {
                    TempData["error"] = "کاربر گرامی رمز عبور شما اشتباه است";
                    return View(model);
                }
                if (res == -150)
                {
                    TempData["warning"] = "  حساب کاربری شما فعال نیست لطفا مجدد ثبت نام کنید";
                    return View();
                }
                if (res == -200)
                {
                    TempData["error"] = "حساب کاربری شما حذف شده است";
                    return View();
                }
                else
                {
                    int userId = _identityService.GetUserIdByPhoneNumber(model.PhoneNumber);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                        new Claim(ClaimTypes.Name,model.PhoneNumber)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(principal, properties);

                    //if (returnUrl  != null)
                    //{
                    //    TempData["success"] = "ورود با موفقیت انجام شد";
                    //    return RedirectToAction("returnUrl");
                    //}

                    TempData["success"] = "ورود با موفقیت انجام شد";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["error"] = "کاربر گرامی متاسفانه ورود به حساب کاربری موفقیت آمیز نبود  ";
            return View(model);
        }

        [HttpGet]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.GetPhoneNumberStatusForForgotPass(model.PhoneNumber);
                if (res == -100)
                {
                    TempData["error"] = "حساب کاربری یافت نشد";
                    return View();
                }
                if (res == -50)
                {
                    TempData["error"] = "کاربر گرامی حساب شما مسدود است";
                    return View(model);
                }
                if (res == -200)
                {
                    TempData["error"] = "حساب کاربری فعال نیست لطفا مجدد ثبت نام کنید";
                    return View();
                }
                if (res == 1)
                {
                    TempData["success"] = "کد تایید برای شما ارسال شد";
                    return RedirectToAction("RsetPassword", new { id = model.PhoneNumber });
                }


            }
            TempData["error"] = "کاربر عزیز متاسفانه عملیات موفقیت آمیز نبود";
            return View(model);
        }

        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult RsetPassword(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.phoneNumber = id;

            return View();
        }


        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult RsetPassword(RsetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.ResetPasswordByMobile(model);
                if (res == -100)
                {
                    TempData["error"] = "حساب کاربری یافت نشد";
                    return View();
                }
                if (res == -50)
                {
                    TempData["error"] = "کد تایید منقضی شده است";
                    return View(model);
                }
                if (res == -200)
                {
                    TempData["error"] = "کد تایید تطابق ندارد";
                    return View(model);
                }
                if (res == 1)
                {
                    TempData["success"] = "رمز عبور شما با موفقیت تغییر یافت";
                    return RedirectToAction("LoginByMobile");
                }


            }
            TempData["error"] = "کاربر عزیز متاسفانه عملیات موفقیت آمیز نبود";
            return View(model);
        }

        [Route("SignOut")]
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["info"] = "کاربر گرامی شما از حساب کاربری خود خارج شدید";
            return RedirectToAction("Index", "Home");
        }

    }
}
