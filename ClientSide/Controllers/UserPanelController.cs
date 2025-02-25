﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;

namespace ClientSide.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class UserPanelController : Controller
    {
        private readonly IIdentityService _identityService;
       

        public UserPanelController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public IActionResult Dashboard()
        {
            var userInfo = _identityService.GetUserInfoForUserPanel(User.Identity.Name);
            return View(userInfo);
        }

        [HttpPost]
        public IActionResult UpdateUserInfo(UserInfoForUserPanelViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool res = _identityService.UpdateUserInfoFromUserPanel(model);
                if (res)
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "UserPanel") });
                }
            }
            return Json(new { status = "error", message = "خطا در ویرایش کاربر" });
        }

    }
}
