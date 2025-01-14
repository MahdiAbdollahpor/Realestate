using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;

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
    }
}
