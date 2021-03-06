﻿using System.Web.Mvc;
using Azimuth.Models;
using Azimuth.Services.Interfaces;

namespace Azimuth.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly IUserService _userService;

        public UserProfileController(ISettingsService settingsService, IUserService userService)
        {
            _settingsService = settingsService;
            _userService = userService;
        }
        //
        // GET: /UserProfile/   
        public ActionResult Index(long? id)
        {
            if (Request.IsAuthenticated)
            {
                var settings = _settingsService.GetUserSettings(id);
                return View(settings);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult _FollowAction(long? userId, string following)
        {
            if (userId == null) return null;
            var user = following == "Follow" ? _userService.FollowPerson((long)userId) : _userService.UnfollowPerson((long)userId);
            return PartialView(UserModel.From(user));
        }
    }
}