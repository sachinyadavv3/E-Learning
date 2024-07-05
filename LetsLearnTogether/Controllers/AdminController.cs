using LetsLearnTogether.BusinessLogicLayer;
using LetsLearnTogether.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LetsLearnTogether.Controllers
{
    public class AdminController : Controller
    {
        private ICategories _categories = new Categories();
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["admin"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<Category> catList = _categories.AdminGetAllCategory();
            return View(catList);
        }

        public ActionResult DeleteCategory(String id)
        {
            Boolean isDeleted = _categories.AdminDeleteCategory(int.Parse(id));
            if (isDeleted)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult Category(String id, String catName)
        {
            HttpCookie cookie = Request.Cookies["admin"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.CategoryId = id;
            ViewBag.CategoryName = catName;
            List<CategoryItem> categoryItemList = _categories.AdminGetAllCategoryItem(int.Parse(id));   
            return View(categoryItemList);
        }

        public ActionResult CategoryItem(int catId, int itemId)
        {
            HttpCookie cookie = Request.Cookies["admin"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.CategoryId = catId;
            ViewBag.ItemId = itemId;
            Content content = _categories.getContentEdit(catId, itemId);
            return View(content);
        }

        public ActionResult AddCategory(String name, String description)
        {
            Boolean isAdded = _categories.AdminAddCategory(name, description);
            if(isAdded)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult AddContent(int catId, String title, String videoLink, String htmlContent, String description)
        {
            if (validateUrl(videoLink))
            {
                Boolean isAdded = _categories.AdminAddContentToCat(catId, title, videoLink, htmlContent, description);
                if(isAdded )
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "something went wrong" });
            }
            return Json(new { success = false, message = "Please Enter Valid YouTube URL" });
        }

        public Boolean validateUrl(String url)
        {
            try
            {
                HttpWebRequest request =  (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}