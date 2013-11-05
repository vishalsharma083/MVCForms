using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCForms.Models;

namespace MVCForms.Controllers
{
    public class UserController : Controller
    {
        readonly MVCFormsAspNetUserEntities _MVCFormsUserDb = new MVCFormsAspNetUserEntities();

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Index()
        {
            var allUsers = _MVCFormsUserDb.aspnet_Users;
            var viewModel = new List<UserAllocationsViewModel>();
            foreach (var user in allUsers) //Don't convert this to a Linq expression; the profile call reacts badly
            {
                var profile = UserProfile.GetUserProfile(user.UserName);
                viewModel.Add(new UserAllocationsViewModel
                                  {
                                      UserId = user.UserId,
                                      UserName = user.UserName,
                                      AvailableSubmissions = profile.AvailableSubmissions,
                                      LastSubmission = profile.LastSubmission
                                  });
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var user = _MVCFormsUserDb.aspnet_Users.Single(u => u.UserId == id);
            var profile = UserProfile.GetUserProfile(user.UserName);
            var viewModel = new UserAllocationsViewModel
                                {
                                    UserId = id,
                                    UserName = user.UserName,
                                    AvailableSubmissions = profile.AvailableSubmissions,
                                    LastSubmission = profile.LastSubmission
                                };
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(UserAllocationsViewModel viewModel)
        {
            var profile = UserProfile.GetUserProfile(viewModel.UserName);
            profile.AvailableSubmissions = viewModel.AvailableSubmissions;
            profile.Save();
            return RedirectToAction("Index", "User");
        }

    }
}
