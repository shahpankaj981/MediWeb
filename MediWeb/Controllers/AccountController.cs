using MediWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cook.Controllers
{
    public class AccountController : Controller
    {

        static string ReturnUrl = "#";

        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User userModel)
        {
            UserRepo userData = new UserRepo();

            User user = new User() { id = userModel.id, password = userModel.password };

            //get user details
            //check user credentials
            user = userData.getUserDetails(user);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(userModel.id, false);

                //var authTicket = new FormsAuthenticationTicket(1, user.email, DateTime.Now, DateTime.Now.AddMinutes(20), false, "user");
                var authTicket = new FormsAuthenticationTicket(1, user.id, DateTime.Now, DateTime.Now.AddMinutes(20), false, "user");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                //HttpCookie userCookie = new HttpCookie("username", userModel.username);
                //HttpContext.Response.Cookies.Add(userCookie);

                return RedirectToAction("viewSearch", "FullDetails", new { pid = user.id });

            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(userModel);
            }
        }

        //POST: /Account/Signup
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {   //return RedirectToAction("Index", "Home");
            return View();
        }

        //POST: /Account/Logout
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}