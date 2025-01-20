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
            //bool isMobileExist = _identityService.IsPhoneNumberExist(id);
            //if (isMobileExist == false)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
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
    }
}
