using LetsLearnTogether.BusinessLogicLayer;
using LetsLearnTogether.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LetsLearnTogether.Controllers
{
    public class ContentController : Controller
    {
        private ICategories _categories = new Categories();
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["user"];
            if (cookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult ContentData(int contentId, int categoryId, String contentTitle)
        {
            Content content = _categories.ReditectToContent(contentId, categoryId, contentTitle);
            content.VideoLink = ExtractVideoId(content.VideoLink);
            return View(content);
        }

        static string ExtractVideoId(string url)
        {
            // Regular expression pattern to match the video ID in YouTube URL
            string pattern = @"(?:\?v=|\/embed\/|\/\d+\/|\/vi?\/|youtu\.be\/|\/e\/|watch\?v=|&v=|\/watch\?v=)([a-zA-Z0-9_-]{11})";

            // Match the pattern in the URL
            Match match = Regex.Match(url, pattern);

            // Extract the video ID from the matched group
            if (match.Success)
            {
                // Base URL for YouTube embed
                string baseUrl = "https://www.youtube.com/embed/";
                string videoId = match.Groups[1].Value;
                // Construct the URL with the video ID
                string embedeUrl = baseUrl + videoId + "?autoplay=1";
                return embedeUrl;
            }
            else
            {
                return url;
            }
        }
    }
}