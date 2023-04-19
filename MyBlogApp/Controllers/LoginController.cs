using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBlogApp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly CustomMembershipProvider customMembershipProvider = new CustomMembershipProvider();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Username, Password")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.customMembershipProvider.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Articles");
                }
            }

            return View(model);
        }

        // GET: Login/SignOut
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Articles");
        }

    }
}