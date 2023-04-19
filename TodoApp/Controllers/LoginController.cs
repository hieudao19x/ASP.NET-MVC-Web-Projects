using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        readonly CustomMembershipProvider memberShipProvider = new CustomMembershipProvider();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,Password")] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.memberShipProvider.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Todoes");
                }
            }

            ViewBag.Message = "ログインが失敗しました。";
            return View(model);
        }

        public ActionResult SignOut ()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}