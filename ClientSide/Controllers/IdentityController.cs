﻿using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

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
    }
}
