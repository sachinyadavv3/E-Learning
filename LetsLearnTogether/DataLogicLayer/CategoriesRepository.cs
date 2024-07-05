using LetsLearnTogether.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace LetsLearnTogether.DataLogicLayer
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public ViewModel GetUserCategories(int userId)
        {
            ViewModel catItems = new ViewModel();
            using(var dbContext = new DBContext())
            {
                List<int> userCategoryId = getUserCategoriesId(userId);
                catItems.Categories = dbContext.Category.Where(a => userCategoryId.Contains(a.Id)).ToList();
                catItems.Items = dbContext.CategoryItem.Where(a => userCategoryId.Contains(a.CategoryId)).ToList();
            }
            return catItems;
        }

        public List<int> getUserCategoriesId(int userId)
        {
            List<int> list = new List<int>();
            using(var dbContext = new DBContext())
            {
                list = dbContext.UserCategory.Where(m => m.UserId == userId).Select(a => a.CategoryId).ToList();
            }
            return list;
        }

        public Models.Content ReditectToContent(int contentId, int categoryId, string contentTitle)
        {
            Models.Content content = new Models.Content();
            using(var dbContext = new DBContext())
            {
                content = dbContext.Content.Where(a => (a.CategoryId == categoryId && a.CategoryItemId == contentId)).FirstOrDefault();
            }
            return content;
        }

        public List<Category> GetAllCourses(int userId)
        {
            List<Category> categories = new List<Category>();
            List<int> enrolledList = getUserCategoriesId(userId);
            using(var dbContext = new DBContext())
            {
                categories = dbContext.Category.ToList();
            }

            foreach(var cat in categories)
            {
                if(!enrolledList.Contains(cat.Id))
                    cat.ThumbnailImagePath = "false";
            }
            return categories;
        }

        public void AddRemoveCategory(int[] CategoriesSelected, int userId)
        {
            using (var dbContext = new DBContext())
            {
                List<UserCategory> catuserList = dbContext.UserCategory.Where(a => a.UserId == userId).ToList();
                dbContext.UserCategory.RemoveRange(catuserList);

                if (CategoriesSelected != null)
                {
                    foreach (var catId in CategoriesSelected)
                    {
                        dbContext.UserCategory.Add(new UserCategory { UserId = userId, CategoryId = catId });
                    }
                }
                dbContext.SaveChanges();
            }
        }

        public List<Category> AdminGetAllCategory()
        {
            List<Category> categories = new List<Category>();
            using(var dbContext = new DBContext())
            {
                categories = dbContext.Category.ToList();
            }
            return categories;
        }

        public Boolean AdminDeleteCategory(int catId)
        {
            using(var dbContext = new DBContext())
            {
                var cat = dbContext.Category.Where(a => a.Id == catId).FirstOrDefault();
                var userCatList = dbContext.UserCategory.Where(a => a.CategoryId == catId).ToList();
                var catItemList = dbContext.CategoryItem.Where(a => a.CategoryId == catId).ToList();
                var contentList = dbContext.Content.Where(a => a.CategoryId == catId).ToList();

                if(contentList.Count() > 0)
                {
                    dbContext.Content.RemoveRange(contentList);
                }

                if(catItemList.Count() > 0)
                {
                    dbContext.CategoryItem.RemoveRange(catItemList);
                }
                if(userCatList.Count() > 0)
                {
                    dbContext.UserCategory.RemoveRange(userCatList);
                }
                if(cat != null)
                {
                    dbContext.Category.Remove(cat);
                }
                int isDeleted = dbContext.SaveChanges();
                if (isDeleted > 0)
                    return true;

            }
            return false;
        }

        public List<CategoryItem> AdminGetAllCategoryItem(int id)
        {
            List<CategoryItem> list = new List<CategoryItem>();
            using(var dbContext = new DBContext())
            {
                list = dbContext.CategoryItem.Where(a => a.CategoryId == id).ToList();
            }
            return list;
        }

        public Models.Content getContentEdit(int catId, int itemId)
        {
            var content = new Content();
            using(var dbContext = new DBContext())
            {
                content = dbContext.Content.Where(a => a.CategoryId == catId && a.CategoryItemId == itemId).FirstOrDefault();
            }
            return content;
        }

        public Boolean AdminAddCategory(string name, string description)
        {
            Category category = new Category();
            category.Title = name;
            category.Description = description;
            using(var dbContext = new DBContext())
            {
                dbContext.Category.Add(category);
                dbContext.SaveChanges();
            }
            return true;
        }

        public Boolean AdminAddContentToCat(int catId, String title, String videoLink, String htmlContent, String description)
        {
            using(var dbContext = new DBContext())
            {
                CategoryItem item = new CategoryItem();
                item.Title = title;
                item.Description = description;
                item.DateTimeItemAdded = DateTime.Now.ToString("MM-dd-YYYY");
                item.CategoryId = catId;
                dbContext.CategoryItem.Add(item);
                dbContext.SaveChanges();

                int itemId = item.Id;

                Content content = new Content();
                content.Title = title;
                content.HTMLContent = htmlContent;
                content.VideoLink = videoLink;
                content.CategoryId = catId;
                content.CategoryItemId = itemId;
                dbContext.Content.Add(content);

                int res = dbContext.SaveChanges();
                if (res > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}