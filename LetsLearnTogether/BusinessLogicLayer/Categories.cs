using LetsLearnTogether.DataLogicLayer;
using LetsLearnTogether.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI.WebControls;

namespace LetsLearnTogether.BusinessLogicLayer
{
    public class Categories : ICategories
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public Categories()
        {
            _categoriesRepository = new CategoriesRepository();
        }
        public ViewModel GetUserCategories(int userId)
        {
            return _categoriesRepository.GetUserCategories(userId);
        }

        public Models.Content ReditectToContent(int contentId, int categoryId, string contentTitle)
        {
            return _categoriesRepository.ReditectToContent(contentId, categoryId, contentTitle);
        }

        public List<Category> GetAllCourses(int userId)
        {
            return _categoriesRepository.GetAllCourses(userId);
        }
        public void AddRemoveCategory(int[] CategoriesSelected, int userId)
        {
            _categoriesRepository.AddRemoveCategory(CategoriesSelected, userId);
        }

        public List<Category> AdminGetAllCategory()
        {
            return _categoriesRepository.AdminGetAllCategory();
        }

        public Boolean AdminDeleteCategory(int catId)
        {
            return _categoriesRepository.AdminDeleteCategory(catId);
        }

        public List<CategoryItem> AdminGetAllCategoryItem(int id)
        {
            return _categoriesRepository.AdminGetAllCategoryItem(id);
        }

        public Boolean AdminAddCategory(string name, string description)
        {
            return _categoriesRepository.AdminAddCategory(name, description);
        }

        public Boolean AdminAddContentToCat(int catId, String title, String videoLink, String htmlContent, String description)
        {
            return _categoriesRepository.AdminAddContentToCat(catId, title, videoLink, htmlContent, description);
        }
        public Models.Content getContentEdit(int catId, int itemId)
        {
            return _categoriesRepository.getContentEdit(catId, itemId);
        }


    }
}