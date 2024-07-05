using LetsLearnTogether.BusinessLogicLayer;
using LetsLearnTogether.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsLearnTogether.Controllers
{
    public class HomeController : Controller
    {
        ICategories _categories = new Categories();
        public ActionResult Index()
        {
            ViewModel viewModel = new ViewModel();
            HttpCookie cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                string data = cookie.Value.ToString();
                User user = JsonConvert.DeserializeObject<User>(data);
                int userId = user.Id;
                viewModel = _categories.GetUserCategories(userId);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View(viewModel);
        }

        public ActionResult Courses()
        {
            List<Category> categories = new List<Category>();
            HttpCookie cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                string data = cookie.Value.ToString();
                User user = JsonConvert.DeserializeObject<User>(data);
                int userId = user.Id;
                categories = _categories.GetAllCourses(userId);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View(categories);
        }

        [HttpPost]
        public ActionResult AddRemoveCategory(int[] CategoriesSelected)
        {
            HttpCookie cookie = Request.Cookies["user"];
            if (cookie != null)
            {
                string data = cookie.Value.ToString();
                User user = JsonConvert.DeserializeObject<User>(data);
                int userId = user.Id;
                _categories.AddRemoveCategory(CategoriesSelected, userId);
            }
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}