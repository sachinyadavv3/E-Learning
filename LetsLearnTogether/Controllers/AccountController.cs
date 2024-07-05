using LetsLearnTogether.BusinessLogicLayer;
using LetsLearnTogether.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LetsLearnTogether.Controllers
{
    public class AccountController : Controller
    {
        IAccount _account = new Account();
        // GET: Account
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["userData"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string name, string email, string phone, string password, string confirmPassword, string role)
        {
            if(role.IsNullOrWhiteSpace()) { role = "user"; }
            string msg = "password and confirm password does not match !";
            if (password.Equals(confirmPassword))
            {
                msg =  _account.Register(name, email, phone, password, role);
                if(msg.Equals("true"))
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
            }
            
            return Json(new { success = false, msg = msg });
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string role)
        {
            User data = _account.Login(email, password, role);
            if (data != null)
            {
                string userData = JsonConvert.SerializeObject(data);
                HttpCookie cookie = new HttpCookie(role);
                cookie.Value = userData;
                HttpContext.Response.Cookies.Add(cookie);
                //cookie.Expires.AddDays(1);
                if(role == "user")
                {
                    return Json(new { success = true, msg = data, redirectUrl = Url.Action("Index", "Home") });
                }else if(role == "admin")
                {
                    return Json(new { success = true, msg = data, redirectUrl = Url.Action("Index", "Admin") });
                }
                
            }
            return Json(new { success = false, msg = "wrong credentials" });
        }

        public ActionResult Logout()
        {
            HttpCookie cookie = Request.Cookies["user"];
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }
    }
}