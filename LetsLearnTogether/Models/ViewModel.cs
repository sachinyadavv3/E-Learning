using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LetsLearnTogether.Models
{
    public class ViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<CategoryItem> Items { get; set; }
    }
}