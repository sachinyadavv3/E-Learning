using LetsLearnTogether.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace LetsLearnTogether.BusinessLogicLayer
{
    internal interface ICategories
    {
        ViewModel GetUserCategories(int userId);
        Models.Content ReditectToContent(int contentId, int categoryId, string contentTitle);
        List<Category> GetAllCourses(int userId);
        void AddRemoveCategory(int[] CategoriesSelected, int userId);
        List<Category> AdminGetAllCategory();
        Boolean AdminDeleteCategory(int catId);
        List<CategoryItem> AdminGetAllCategoryItem(int id);
        Models.Content getContentEdit(int catId, int itemId);
        Boolean AdminAddCategory(string name, string description);
        Boolean AdminAddContentToCat(int catId, String title, String videoLink, String htmlContent, String description);
    }
}
