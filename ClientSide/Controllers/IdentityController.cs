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


        [HttpPost]
        //[Route("Register")]
        public IActionResult RegisterByMobile(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.RegisterByPhoneNumber(model);
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

                    TempData["success"] = "ورود با موفقیت انجام شد";
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });

                }
                if (res == -100)
                {
                    return Json(new { success = false, message = "این شماره موبایل قبلا در سایت ثبت نام کرده است" });
                }
                
            }
            return Json(new { success = false, message = "کاربر گرامی متاسفانه ثبت نام موفقیت آمیز نبود  " });
        }

        


        

        [HttpPost]
        //[Route("Login")]
        public IActionResult LoginByMobile(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.GetUserStatusForLoginByPhoneNumber(model.PhoneNumber, model.Password);
                if (res == -100)
                {
                    
                    return Json(new { success = false, message = "حساب کاربری وجود ندارد" });
                }
                if (res == -50)
                {
                    
                    return Json(new { success = false, message = "کاربر گرامی رمز عبور شما اشتباه است" });
                }
                
                if (res == -200)
                {
                   
                    return Json(new { success = false, message = "حساب کاربری شما حذف شده است" });
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

                    

                    TempData["success"] = "ورود با موفقیت انجام شد";
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
                }
            }
            
            return Json(new { success = false, message = "کاربر گرامی متاسفانه ورود به حساب کاربری موفقیت آمیز نبود" });
        }

        

        [HttpPost]
        //[Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.GetPhoneNumberStatusForForgotPass(model.PhoneNumber);
                if (res == -100)
                {
                    return Json(new { success = false, message = "حساب کاربری یافت نشد" });
                }
                if (res == -50)
                {
                    return Json(new { success = false, message = "کاربر گرامی حساب شما مسدود است" });
                }
                if (res == -200)
                {
                    return Json(new { success = false, message = "حساب کاربری فعال نیست لطفا مجدد ثبت نام کنید" });
                }
                if (res == 1)
                {
                    //TempData["success"] = "کد تایید برای شما ارسال شد";
                    //return RedirectToAction("RsetPassword", new { id = model.PhoneNumber });
                    return Json(new { success = true, phoneNumber = model.PhoneNumber });
                }


            }
            return Json(new { success = false, message = "کاربر عزیز متاسفانه عملیات موفقیت آمیز نبود" });
        }

        


        [HttpPost]
        public IActionResult RsetPassword(RsetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.ResetPasswordByMobile(model);
                if (res == -100)
                {
                    return Json(new { success = false, message = "حساب کاربری یافت نشد" });
                }
                if (res == -50)
                {
                    return Json(new { success = false, message = "کد تایید منقضی شده است" });
                }
                if (res == -200)
                {
                    return Json(new { success = false, message = "کد تایید تطابق ندارد" });
                }
                if (res == 1)
                {
                    TempData["success"] = "رمز عبور شما با موفقیت تغییر یافت";
                    return Json(new { success = true, redirectUrl = Url.Action("LoginByMobile") });
                }
            }
            return Json(new { success = false, message = "کاربر عزیز متاسفانه عملیات موفقیت آمیز نبود" });
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
